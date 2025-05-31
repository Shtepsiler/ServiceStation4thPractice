using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PARTS.BLL;
using PARTS.BLL.Services;
using PARTS.DAL;
using PARTS.DAL.Data;
using PARTS.DAL.Seeders;
using ServiceCenterPayment;
using System.Reflection;
using System.Text;

namespace PARTS.API;

public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure logging
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();

        // Configure services
        ConfigureServices(builder);

        var app = builder.Build();

        // Configure pipeline
        await ConfigurePipelineAsync(app);

        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        // API Services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // Swagger
        ConfigureSwagger(builder.Services);

        // Database
        ConfigureDatabase(builder);

        // Caching
        ConfigureRedisCache(builder);

        // Authentication & Authorization
        ConfigureAuthentication(builder);

        // Business Logic & Data Access
        builder.Services.AddPartsDal();
        builder.Services.AddPartsBll();

        // Blockchain Services
        ConfigureBlockchainServices(builder);

        // Health Checks
        ConfigureHealthChecks(builder);

        // CORS (if needed)
        ConfigureCors(builder);
    }

    private static void ConfigureSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Parts API",
                Version = "v1",
                Description = "Auto parts management API"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
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

    private static void ConfigureDatabase(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<PartsDBContext>(options =>
        {
            var connectionString = GetConnectionString(builder.Configuration);

            options.UseSqlServer(connectionString, sqlOptions =>
            {
                //sqlOptions.EnableRetryOnFailure(
                //    maxRetryCount: 3,
                //    maxRetryDelay: TimeSpan.FromSeconds(30),
                //    errorNumbersToAdd: null);
                //sqlOptions.CommandTimeout(60);
            });

            // Only enable sensitive data logging in development
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

    private static void ConfigureRedisCache(WebApplicationBuilder builder)
    {
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            var redisConfiguration = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"
                ? Environment.GetEnvironmentVariable("REDIS")
                : builder.Configuration.GetValue<string>("Redis");

            if (string.IsNullOrEmpty(redisConfiguration))
            {
                throw new InvalidOperationException("Redis configuration is required");
            }

            options.Configuration = redisConfiguration;
            options.InstanceName = "ServiceStationParts";
        });
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
                ClockSkew = TimeSpan.FromDays(1), // Reduced from 1 day
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    // Try to get token from cookie if not in header
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

        builder.Services.AddAuthorization();
    }

    private static void ConfigureBlockchainServices(WebApplicationBuilder builder)
    {
        builder.Services.Configure<Web3Config>(builder.Configuration.GetSection("Blockchain"));
        builder.Services.AddWeb3Context();
        builder.Services.AddSingleton<IServiceCenterPaymentServiceFactory, ServiceCenterPaymentServiceFactory>();
        builder.Services.AddHostedService<EventProcessingService>();
    }

    private static void ConfigureHealthChecks(WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks().AddSqlServer(GetConnectionString(builder.Configuration))
            .AddRedis(sp => sp.GetRequiredService<IConfiguration>().GetValue<string>("Redis"), "redis");
    }

    private static void ConfigureCors(WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                }
                else
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    // Configure for production
                    //policy.WithOrigins("https://yourdomain.com")
                    //      .AllowCredentials()
                    //      .AllowAnyMethod()
                    //      .AllowAnyHeader();
                }
            });
        });
    }

    private static async Task ConfigurePipelineAsync(WebApplication app)
    {
        // Initialize database and seed data
        await InitializeDatabaseAsync(app);

        // Configure request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parts API V1");
                c.RoutePrefix = string.Empty; // Serve Swagger UI at root
            });
        }
        else
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

        // Enable CORS if configured
        app.UseCors();

        // Authentication & Authorization
        app.UseAuthentication();
        app.UseAuthorization();

        // Health checks
        app.MapHealthChecks("/health");

        // API endpoints
        app.MapControllers();
    }

    private static async Task InitializeDatabaseAsync(WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            logger.LogInformation("Starting database initialization...");

            var dbContext = scope.ServiceProvider.GetRequiredService<PartsDBContext>();

            // Ensure database exists and apply migrations
            await dbContext.Database.MigrateAsync();
            logger.LogInformation("Database migrations applied successfully");

            // Seed data in parallel where possible
            await SeedDataAsync(dbContext, logger);

            // Initialize blockchain services
            await InitializeBlockchainAsync(scope.ServiceProvider, logger);

            logger.LogInformation("Database initialization completed successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to initialize database");

            if (app.Environment.IsDevelopment())
            {
                throw; // Re-throw in development for debugging
            }

            // In production, log and continue - app can still start without seed data
            logger.LogWarning("Application starting without complete database initialization");
        }
    }

    private static async Task SeedDataAsync(PartsDBContext dbContext, ILogger logger)
    {
        var seedTasks = new List<Task>();

        // Seed vehicle data
        var modelSplitter = new ModelSplitter(dbContext);

        if (!await modelSplitter.IsDataPresentAsync())
        {
            seedTasks.Add(Task.Run(async () =>
            {
                try
                {
                    logger.LogInformation("Starting vehicle data seeding...");
                    await modelSplitter.SeedAsync();
                    await modelSplitter.SeedVehiclesAsync();
                    logger.LogInformation("Vehicle data seeding completed");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to seed vehicle data");
                }
            }));
        }

        // Seed shop data
        var shopSeeder = new ShopSeeder(dbContext);
        seedTasks.Add(Task.Run(async () =>
        {
            try
            {
                logger.LogInformation("Starting shop data seeding...");
                await shopSeeder.SeedAsync();
                logger.LogInformation("Shop data seeding completed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to seed shop data, trying static fallback");
                try
                {
                    await shopSeeder.SeedWithStaticDataAsync();
                    logger.LogInformation("Static shop data seeding completed");
                }
                catch (Exception fallbackEx)
                {
                    logger.LogError(fallbackEx, "Failed to seed static shop data");
                }
            }
        }));

        // Wait for all seeding tasks to complete
        await Task.WhenAll(seedTasks);
    }

    private static async Task InitializeBlockchainAsync(IServiceProvider serviceProvider, ILogger logger)
    {
        try
        {
            logger.LogInformation("Initializing blockchain services...");

            var paymentFactory = serviceProvider.GetRequiredService<IServiceCenterPaymentServiceFactory>();
            await paymentFactory.CreateServiceAsync();

            logger.LogInformation("Blockchain services initialized successfully");
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to initialize blockchain services - continuing without blockchain functionality");
        }
    }
}