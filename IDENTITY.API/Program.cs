using FluentValidation;
using FluentValidation.AspNetCore;
using IDENTITY.BLL.Configurations;
using IDENTITY.BLL.DTO.Requests;
using IDENTITY.BLL.Factories;
using IDENTITY.BLL.Factories.Interfaces;
using IDENTITY.BLL.Mapping;
using IDENTITY.BLL.Services;
using IDENTITY.BLL.Services.Interfaces;
using IDENTITY.BLL.Validation;
using IDENTITY.DAL.Data;
using IDENTITY.DAL.Entities;
using IDENTITY.DAL.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Text;

namespace IDENTITY.API;

public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure logging first
        ConfigureLogging(builder);

        // Configure services
        ConfigureServices(builder);

        var app = builder.Build();

        // Configure pipeline
        await ConfigurePipelineAsync(app);

        app.Run();
    }

    private static void ConfigureLogging(WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, config) =>
        {
            config.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                  .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                  .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                  .Enrich.FromLogContext()
                  .Enrich.WithProperty("Application", "IdentityAPI");

            if (builder.Environment.IsDevelopment())
            {
                config.MinimumLevel.Debug()
                      .WriteTo.Console()
                      .WriteTo.Debug();
            }
            else
            {
                config.MinimumLevel.Information()
                      .WriteTo.Console()
                      .WriteTo.File(
                          path: builder.Configuration["Logging:File:Path"] ?? "logs/identity-api-.txt",
                          rollingInterval: RollingInterval.Day,
                          retainedFileCountLimit: 30,
                          fileSizeLimitBytes: 10_000_000,
                          rollOnFileSizeLimit: true);
            }
        });
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        // Controllers and validation
        ConfigureControllersAndValidation(builder);

        // Database
        ConfigureDatabase(builder);

        // Identity
        ConfigureIdentity(builder);

        // Authentication & Authorization
        ConfigureAuthentication(builder);

        // Business Logic Services
        ConfigureBusinessServices(builder);

        // API Documentation
        ConfigureSwagger(builder);

        // CORS
        ConfigureCors(builder);

        // Health Checks
        ConfigureHealthChecks(builder);

        // Other services
        builder.Services.AddEndpointsApiExplorer();
    }

    private static void ConfigureControllersAndValidation(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(options =>
        {
            // Global filters can be added here
            options.SuppressAsyncSuffixInActionNames = false;
        });

        // FluentValidation
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Specific validators
        builder.Services.AddScoped<IValidator<UserSignInRequest>, UserSignInRequestValidator>();
        builder.Services.AddScoped<IValidator<UserSignUpRequest>, UserSingUpRequestValidator>();
    }

    private static void ConfigureDatabase(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDBContext>(options =>
        {
            var connectionString = GetConnectionString(builder.Configuration);

            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                sqlOptions.CommandTimeout(60);
                sqlOptions.MigrationsAssembly("IDENTITY.DAL");
            });

            if (builder.Environment.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });
    }

    private static string GetConnectionString(IConfiguration configuration)
    {
        if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST")
                ?? throw new InvalidOperationException("DB_HOST environment variable is required in container mode");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME")
                ?? throw new InvalidOperationException("DB_NAME environment variable is required in container mode");
            var dbUser = Environment.GetEnvironmentVariable("DB_USER")
                ?? throw new InvalidOperationException("DB_USER environment variable is required in container mode");
            var dbPass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD")
                ?? throw new InvalidOperationException("DB_SA_PASSWORD environment variable is required in container mode");

            return $"Data Source={dbHost};User ID={dbUser};Password={dbPass};Initial Catalog={dbName};Encrypt=True;Trust Server Certificate=True;Connection Timeout=30;Command Timeout=60;";
        }

        return configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("DefaultConnection string is required when not running in container");
    }

    private static void ConfigureIdentity(WebApplicationBuilder builder)
    {
        builder.Services.AddIdentityCore<User>(options =>
        {
            // Password requirements
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;

            // User requirements
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" +
                "ÀÁÂÃ¥ÄÅªÆÇÈ²¯ÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÜÞßàáâã´äåºæçè³¿éêëìíîïðñòóôõö÷øùüþÿ" +
                "0123456789!@.,/ ";

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // SignIn settings
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        })
        .AddRoles<Role>()
        .AddUserManager<UserManager<User>>()
        .AddSignInManager<SignInManager<User>>()
        .AddRoleManager<RoleManager<Role>>()
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<AppDBContext>();
    }

    private static void ConfigureAuthentication(WebApplicationBuilder builder)
    {
        var jwtKey = builder.Configuration["JwtSecurityKey"]
            ?? throw new InvalidOperationException("JwtSecurityKey is required");

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddCookie("Identity.Application", options =>
        {
            options.Cookie.Name = "Bearer";
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.ExpireTimeSpan = TimeSpan.FromHours(24);
            options.SlidingExpiration = true;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ClockSkew = TimeSpan.FromMinutes(5) // Reduced from 1 day
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    if (string.IsNullOrEmpty(context.Token))
                    {
                        context.Token = context.Request.Cookies["Bearer"];
                    }
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception is SecurityTokenExpiredException)
                    {
                        context.Response.Headers.Add("Token-Expired", "true");
                    }
                    return Task.CompletedTask;
                }
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            // Define custom policies here if needed
            options.AddPolicy("RequireAdminRole", policy =>
                policy.RequireRole("Admin"));
            options.AddPolicy("RequireUserRole", policy =>
                policy.RequireRole("User", "Admin"));
        });
    }

    private static void ConfigureBusinessServices(WebApplicationBuilder builder)
    {
        // Configurations
        builder.Services.AddSingleton<JwtTokenConfiguration>();
        builder.Services.AddSingleton<ClientAppConfiguration>();
        builder.Services.AddSingleton<GoogleClientConfiguration>();
        builder.Services.AddSingleton<EmailSenderConfiguration>();

        // AutoMapper
        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

        // Business Services
        builder.Services.AddScoped<IJwtSecurityTokenFactory, JwtSecurityTokenFactory>();
        builder.Services.AddScoped<EmailSender>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IIdentityService, IdentityService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IRoleService, RoleService>();
    }

    private static void ConfigureSwagger(WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Identity API",
                Version = "v1",
                Description = "Authentication and authorization service for Service Station system"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter JWT Bearer token"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            // Include XML comments if available
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }
        });
    }

    private static void ConfigureCors(WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            if (builder.Environment.IsDevelopment())
            {
                options.AddPolicy("Open", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            }
            else
            {
                options.AddPolicy("Production", policy =>
                {
                    policy.WithOrigins(
                            builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()
                            ?? ["https://yourdomain.com"])
                          .AllowCredentials()
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .WithExposedHeaders("Token-Expired");
                });
            }

            // Default policy for API Gateway
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:2060", "http://api_gateway:8083")
                      .AllowCredentials()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });
    }

    private static void ConfigureHealthChecks(WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy("Identity API is running"))
            .AddSqlServer(GetConnectionString(builder.Configuration));
    }

    private static async Task ConfigurePipelineAsync(WebApplication app)
    {
        // Global exception handling
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        // Security headers
        app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");

            await next();
        });

        // Development tools
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API V1");
                options.RoutePrefix = string.Empty;
                options.DocumentTitle = "Identity API - Service Station";
            });
        }

        // Initialize database and seed data
        await InitializeDatabaseAsync(app);

        // Request pipeline
        // app.UseHttpsRedirection(); // Commented out for container deployment

        // CORS
        if (app.Environment.IsDevelopment())
        {
            app.UseCors("Open");
        }
        else
        {
            app.UseCors("Production");
        }

        // Authentication & Authorization
        app.UseAuthentication();
        app.UseAuthorization();

        // Health checks
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                var response = new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.ToDictionary(
                        kvp => kvp.Key,
                        kvp => new
                        {
                            status = kvp.Value.Status.ToString(),
                            description = kvp.Value.Description,
                            duration = kvp.Value.Duration.TotalMilliseconds
                        }
                    ),
                    totalDuration = report.TotalDuration.TotalMilliseconds
                };
                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
            }
        });

        // Quick health check
        app.MapGet("/ping", () => Results.Ok(new { status = "healthy", service = "identity-api", timestamp = DateTime.UtcNow }));

        // Controllers
        app.MapControllers();
    }

    private static async Task InitializeDatabaseAsync(WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            logger.LogInformation("Starting Identity database initialization...");

            var context = scope.ServiceProvider.GetRequiredService<AppDBContext>();

            // Apply migrations
            await context.Database.MigrateAsync();
            logger.LogInformation("Database migrations applied successfully");

            // Seed data
            await Seed.Initialize(scope.ServiceProvider);
            logger.LogInformation("Database seeding completed successfully");

            logger.LogInformation("Identity database initialization completed");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to initialize Identity database");

            if (app.Environment.IsDevelopment())
            {
                throw; // Re-throw in development for debugging
            }

            logger.LogWarning("Identity API starting without complete database initialization");
        }
    }
}