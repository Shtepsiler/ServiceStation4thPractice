using JOBS.BLL;
using JOBS.BLL.Helpers;
using JOBS.DAL.Data;
using JOBS.DAL.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Polly;
using ServiceCenterPayment;
using System.Net;
using System.Reflection;
using System.Text;

namespace JOBS.API;

public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure logging
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
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        if (builder.Environment.IsDevelopment())
        {
            builder.Logging.AddDebug();
            builder.Logging.SetMinimumLevel(LogLevel.Debug);
        }
        else
        {
            builder.Logging.SetMinimumLevel(LogLevel.Information);
        }
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        // Controllers
        builder.Services.AddControllers(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
        });

        // Business Logic Layer
        builder.Services.AddBuisnesLogicLayer();

        // Database
        ConfigureDatabase(builder);

        // Authentication & Authorization
        ConfigureAuthentication(builder);

        // External Services
        ConfigureHttpClients(builder);

        // Caching
        ConfigureRedisCache(builder);

        // Blockchain Services
        ConfigureBlockchainServices(builder);

        // API Documentation
        ConfigureSwagger(builder);

        // CORS
        ConfigureCors(builder);

        // Health Checks
        ConfigureHealthChecks(builder);

        // Other services
        builder.Services.AddEndpointsApiExplorer();
    }

    private static void ConfigureDatabase(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ServiceStationDBContext>(options =>
        {
            var connectionString = GetConnectionString(builder.Configuration);

            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                sqlOptions.CommandTimeout(60);
                sqlOptions.MigrationsAssembly("JOBS.DAL");
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
            // Define custom authorization policies if needed
            options.AddPolicy("RequireAdminRole", policy =>
                policy.RequireRole("Admin"));
            options.AddPolicy("RequireJobAccess", policy =>
                policy.RequireAuthenticatedUser());
        });
    }

    private static void ConfigureHttpClients(WebApplicationBuilder builder)
    {
        var apiBaseString = builder.Configuration["APIBaseString"]
            ?? throw new InvalidOperationException("APIBaseString configuration is required");

        builder.Services.AddHttpClient("Model", client =>
        {
            client.BaseAddress = new Uri(apiBaseString);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "JobsAPI/1.0");
            client.Timeout = TimeSpan.FromSeconds(30);
        })
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler()
            {
                CookieContainer = new CookieContainer(),
                UseCookies = true
            };

            // Only ignore SSL errors in development
            if (builder.Environment.IsDevelopment())
            {
                handler.ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => true;
            }

            return handler;
        })
        .AddPolicyHandler(GetRetryPolicy())
        .AddPolicyHandler(GetCircuitBreakerPolicy());

        // Health check client
        builder.Services.AddHttpClient("HealthCheck", client =>
        {
            client.Timeout = TimeSpan.FromSeconds(10);
            client.DefaultRequestHeaders.Add("User-Agent", "JobsAPI-HealthCheck/1.0");
        });
    }

    private static void ConfigureRedisCache(WebApplicationBuilder builder)
    {
        var redisConfiguration = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"
            ? Environment.GetEnvironmentVariable("REDIS")
            : builder.Configuration.GetValue<string>("Redis");

        if (string.IsNullOrEmpty(redisConfiguration))
        {
            throw new InvalidOperationException("Redis configuration is required");
        }

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConfiguration;
            options.InstanceName = "ServiceStationJobs";
        });
    }

    private static void ConfigureBlockchainServices(WebApplicationBuilder builder)
    {
        builder.Services.Configure<Web3Config>(builder.Configuration.GetSection("Blockchain"));
        builder.Services.AddWeb3Context();
        builder.Services.AddSingleton<IServiceCenterPaymentServiceFactory, ServiceCenterPaymentServiceFactory>();
        builder.Services.AddHostedService<EventProcessingService>();
    }

    private static void ConfigureSwagger(WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Jobs API",
                Version = "v1",
                Description = "Job management service for Service Station system"
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
                options.AddPolicy("Development", policy =>
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
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    //policy.WithOrigins(
                    //        builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()
                    //        ?? ["https://yourdomain.com"])
                    //      .AllowCredentials()
                    //      .AllowAnyHeader()
                    //      .AllowAnyMethod()
                    //      .WithExposedHeaders("Token-Expired");
                });
            }

            // Default policy for API Gateway
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                //policy.WithOrigins("http://localhost:2060", "http://api_gateway:8083")
                //      .AllowCredentials()
                //      .AllowAnyHeader()
                //      .AllowAnyMethod();
            });
        });
    }

    private static void ConfigureHealthChecks(WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy("Jobs API is running"))
            .AddSqlServer(GetConnectionString(builder.Configuration))
            .AddRedis(
                redisConnectionString: Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"
                    ? Environment.GetEnvironmentVariable("REDIS") ?? "localhost:6379"
                    : builder.Configuration.GetValue<string>("Redis") ?? "localhost:6379",
                name: "redis",
                timeout: TimeSpan.FromSeconds(15));
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
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Jobs API V1");
                options.RoutePrefix = string.Empty;
                options.DocumentTitle = "Jobs API - Service Station";
            });
        }

        // Initialize database and services
        await InitializeServicesAsync(app);

        // Request pipeline
        //  app.UseHttpsRedirection(); // Commented out for container deployment

        // CORS
        if (app.Environment.IsDevelopment())
        {
            app.UseCors("Development");
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
        app.MapGet("/ping", () => Results.Ok(new { status = "healthy", service = "jobs-api", timestamp = DateTime.UtcNow }));

        // Controllers
        app.MapControllers();
    }

    private static async Task InitializeServicesAsync(WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            logger.LogInformation("Starting Jobs service initialization...");

            var context = scope.ServiceProvider.GetRequiredService<ServiceStationDBContext>();

            // Apply migrations
            await context.Database.MigrateAsync();
            logger.LogInformation("Database migrations applied successfully");

            // Seed data
            await Seed.Initialize(scope.ServiceProvider);
            logger.LogInformation("Database seeding completed successfully");

            // Initialize blockchain services
            await InitializeBlockchainAsync(scope.ServiceProvider, logger);

            logger.LogInformation("Jobs service initialization completed");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to initialize Jobs service");

            if (app.Environment.IsDevelopment())
            {
                throw; // Re-throw in development for debugging
            }

            logger.LogWarning("Jobs API starting without complete initialization");
        }
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

    // HTTP Client Resilience Policies
    private static Polly.IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return Polly.Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (outcome, delay, retryCount, context) =>
                {
                    Console.WriteLine($"Retry {retryCount} after {delay} seconds");
                });
    }

    private static Polly.IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return Polly.Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromSeconds(30),
                onBreak: (result, timespan) =>
                {
                    Console.WriteLine($"Circuit breaker opened for {timespan} seconds");
                },
                onReset: () =>
                {
                    Console.WriteLine("Circuit breaker reset");
                });
    }
}