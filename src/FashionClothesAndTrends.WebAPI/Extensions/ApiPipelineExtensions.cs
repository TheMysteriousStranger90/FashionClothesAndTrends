using FashionClothesAndTrends.WebAPI.Filters;
using Hangfire;

namespace FashionClothesAndTrends.WebAPI.Extensions;

public static class ApiPipelineExtensions
{
    public static WebApplication UseConfiguredSecurityHeaders(
        this WebApplication app,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        var csp = configuration["SecurityHeaders:ContentSecurityPolicy"]
                  ??
                  "default-src 'self'; frame-ancestors 'none'; object-src 'none'; base-uri 'self'; form-action 'self'";
        var referrerPolicy = configuration["SecurityHeaders:ReferrerPolicy"]
                             ?? "strict-origin-when-cross-origin";
        var permissionsPolicy = configuration["SecurityHeaders:PermissionsPolicy"]
                                ??
                                "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()";

        app.Use(async (context, next) =>
        {
            context.Response.Headers["X-Frame-Options"] = "DENY";
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["Referrer-Policy"] = referrerPolicy;
            context.Response.Headers["Permissions-Policy"] = permissionsPolicy;
            context.Response.Headers["Content-Security-Policy"] = csp;
            context.Response.Headers["Cross-Origin-Opener-Policy"] = "same-origin";
            context.Response.Headers["Cross-Origin-Resource-Policy"] = "same-origin";

            if (!environment.IsDevelopment())
            {
                context.Response.Headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains; preload";
            }

            await next();
        });

        return app;
    }

    public static WebApplication UseConfiguredHangfireDashboard(this WebApplication app)
    {
        app.UseHangfireDashboard("/jobs", new DashboardOptions
        {
            Authorization =
                [app.Services.CreateScope().ServiceProvider.GetRequiredService<HangfireAuthorizationFilter>()]
        });

        return app;
    }

    public static WebApplication UseConfiguredRequestPipeline(
        this WebApplication app,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        app.UseRouting();
        app.UseCors(ApiInfrastructureExtensions.FrontendCorsPolicy);
        app.UseResponseCaching();
        app.UseRateLimiter();
        app.UseConfiguredSecurityHeaders(configuration, environment);
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}
