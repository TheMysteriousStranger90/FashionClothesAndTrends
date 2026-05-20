using System.Threading.RateLimiting;
using Asp.Versioning;
using FashionClothesAndTrends.WebAPI.Filters;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.ResponseCompression;

namespace FashionClothesAndTrends.WebAPI.Extensions;

public static class ApiInfrastructureExtensions
{
    public const string FrontendCorsPolicy = "FrontendCorsPolicy";

    public static IServiceCollection AddApiInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<HangfireAuthorizationFilter>();

        services.AddConfiguredCors(configuration, environment);
        services.AddConfiguredRateLimiting(configuration);
        services.AddConfiguredResponseCompression();
        services.AddConfiguredApiVersioning();
        services.AddConfiguredHangfire(configuration);
        services.AddResponseCaching();

        return services;
    }

    public static IServiceCollection AddConfiguredCors(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        var configuredOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
                                ?? Array.Empty<string>();

        var allowedOrigins = configuredOrigins
            .Where(origin => !string.IsNullOrWhiteSpace(origin))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        if (!environment.IsDevelopment() && allowedOrigins.Length == 0)
        {
            throw new InvalidOperationException(
                "Cors:AllowedOrigins must be configured for non-development environments.");
        }

        services.AddCors(options =>
        {
            options.AddPolicy(FrontendCorsPolicy, policy =>
            {
                var origins = allowedOrigins.Length > 0
                    ? allowedOrigins
                    : ["https://localhost:4200"];

                policy.WithOrigins(origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("WWW-Authenticate");
            });
        });

        return services;
    }

    public static IServiceCollection AddConfiguredRateLimiting(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var globalPermitLimit = configuration.GetValue<int?>("RateLimiting:Global:PermitLimit") ?? 100;
        var globalWindowSeconds = configuration.GetValue<int?>("RateLimiting:Global:WindowSeconds") ?? 60;

        var authPermitLimit = configuration.GetValue<int?>("RateLimiting:Authentication:PermitLimit") ?? 5;
        var authWindowMinutes = configuration.GetValue<int?>("RateLimiting:Authentication:WindowMinutes") ?? 5;

        var registrationPermitLimit = configuration.GetValue<int?>("RateLimiting:Registration:PermitLimit") ?? 3;
        var registrationWindowMinutes = configuration.GetValue<int?>("RateLimiting:Registration:WindowMinutes") ?? 60;

        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: context.User.Identity?.Name ??
                                  context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = globalPermitLimit,
                        Window = TimeSpan.FromSeconds(globalWindowSeconds)
                    }));

            options.AddPolicy("authentication", context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = authPermitLimit,
                        Window = TimeSpan.FromMinutes(authWindowMinutes)
                    }));

            options.AddPolicy("registration", context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = registrationPermitLimit,
                        Window = TimeSpan.FromMinutes(registrationWindowMinutes)
                    }));
        });

        return services;
    }

    public static IServiceCollection AddConfiguredResponseCompression(this IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });

        return services;
    }

    public static IServiceCollection AddConfiguredApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        return services;
    }

    public static IServiceCollection AddConfiguredHangfire(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var sqlConnectionString = configuration.GetRequiredSqlConnectionString();

        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(sqlConnectionString));

        services.AddHangfireServer();

        return services;
    }
}
