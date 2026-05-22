using System.Reflection;
using FashionClothesAndTrends.WebAPI.Hubs;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Infrastructure.Context;
using FashionClothesAndTrends.Infrastructure.SeedData;
using FashionClothesAndTrends.WebAPI.Extensions;
using FashionClothesAndTrends.WebAPI.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    EnvironmentBootstrapExtensions.LoadDotEnvIntoProcessEnvironment();

    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddEnvironmentVariables();
    var serviceName = builder.Configuration["OpenTelemetry:ServiceName"] ?? builder.Environment.ApplicationName;
    var serviceVersion = builder.Configuration["OpenTelemetry:ServiceVersion"]
                         ?? Assembly.GetEntryAssembly()?.GetName().Version?.ToString()
                         ?? "1.0.0";

    builder.Host.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithThreadId()
        .Enrich.WithProcessId()
        .Enrich.WithProperty("Application", serviceName)
        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
        .Enrich.WithProperty("Version", serviceVersion));

    builder.Services.AddControllers();
    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddIdentityServices(builder.Configuration);
    builder.Services.AddSwaggerDocumentation();
    builder.Services.AddObservability(builder.Configuration, builder.Environment);
    builder.Services.AddApiInfrastructure(builder.Configuration, builder.Environment);

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwaggerDocumentation();
    }
    else
    {
        app.UseHsts();
    }

    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "Handled {RequestMethod} {RequestPath} => {StatusCode} in {Elapsed:0.0000} ms";
        options.GetLevel = (httpContext, _, exception) =>
        {
            if (exception != null || httpContext.Response.StatusCode >= StatusCodes.Status500InternalServerError)
            {
                return LogEventLevel.Error;
            }

            if (httpContext.Request.Path.StartsWithSegments("/health") ||
                httpContext.Request.Path.StartsWithSegments("/metrics"))
            {
                return LogEventLevel.Verbose;
            }

            return LogEventLevel.Information;
        };
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value ?? string.Empty);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
            diagnosticContext.Set("TraceIdentifier", httpContext.TraceIdentifier);
            diagnosticContext.Set("RemoteIpAddress",
                httpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty);
            diagnosticContext.Set("UserName", httpContext.User.Identity?.Name ?? string.Empty);
        };
    });

    app.UseResponseCompression();
    app.UseHttpsRedirection();
    app.UseMiddleware<ExceptionMiddleware>();

    app.UseConfiguredRequestPipeline(builder.Configuration, app.Environment);
    app.UseConfiguredHangfireDashboard();

    app.UseDefaultFiles();

    app.MapObservabilityEndpoints();
    app.MapControllers();
    app.MapHub<DiscountNotificationHub>("/notify");

    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
        await context.Database.MigrateAsync();

        await SeedDataInitializer.SeedUsersAsync(userManager, roleManager);
    }
    catch (Exception exception)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, "An error occured during database migration or seed execution");
    }

    await app.RunAsync();
}
catch (Microsoft.Extensions.Hosting.HostAbortedException)
{
    // Expected in some design-time tooling scenarios (for example, dotnet ef).
}
catch (Exception exception)
{
    Log.Fatal(exception, "Application terminated unexpectedly during startup");
    throw;
}
finally
{
    await Log.CloseAndFlushAsync();
}


