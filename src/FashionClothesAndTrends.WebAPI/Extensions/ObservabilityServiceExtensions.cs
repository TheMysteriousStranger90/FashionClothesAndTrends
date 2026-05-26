using System.Reflection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace FashionClothesAndTrends.WebAPI.Extensions;

public static class ObservabilityServiceExtensions
{
    public static IServiceCollection AddObservability(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        var sqlConnectionString = configuration.GetRequiredSqlConnectionString();
        var redisConnectionString = configuration.GetRequiredRedisConnectionString();

        var serviceName = configuration["OpenTelemetry:ServiceName"] ?? environment.ApplicationName;
        var serviceVersion = configuration["OpenTelemetry:ServiceVersion"]
                             ?? Assembly.GetEntryAssembly()?.GetName().Version?.ToString()
                             ?? "1.0.0";
        var samplingRatio = configuration.GetValue<double?>("OpenTelemetry:Tracing:SamplingRatio") ?? 1.0d;
        var enableConsoleExporter = configuration.GetValue<bool?>("OpenTelemetry:ConsoleExporter:Enabled")
                                    ?? environment.IsDevelopment();
        var otlpEndpoint = configuration["OpenTelemetry:Otlp:Endpoint"];
        var otlpHeaders = configuration["OpenTelemetry:Otlp:Headers"];
        var otlpProtocol = ParseOtlpProtocol(configuration["OpenTelemetry:Otlp:Protocol"]);

        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), tags: ["live"])
            .AddSqlServer(sqlConnectionString, name: "sqlserver", tags: ["ready"])
            .AddRedis(redisConnectionString, name: "redis", tags: ["ready"]);

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource
                .AddService(serviceName, serviceVersion: serviceVersion)
                .AddAttributes([
                    new KeyValuePair<string, object>("deployment.environment.name", environment.EnvironmentName),
                    new KeyValuePair<string, object>("service.namespace", "FashionClothesAndTrends")
                ]))
            .WithTracing(tracing =>
            {
                tracing
                    .SetSampler(new ParentBasedSampler(new TraceIdRatioBasedSampler(samplingRatio)))
                    .AddAspNetCoreInstrumentation(options =>
                    {
                        options.RecordException = true;
                        options.Filter = httpContext =>
                            !httpContext.Request.Path.StartsWithSegments("/health") &&
                            !httpContext.Request.Path.StartsWithSegments("/metrics") &&
                            !httpContext.Request.Path.StartsWithSegments("/swagger");
                    })
                    .AddHttpClientInstrumentation(options => { options.RecordException = true; })
                    .AddSqlClientInstrumentation(options => { options.RecordException = true; });

                if (enableConsoleExporter)
                {
                    tracing.AddConsoleExporter();
                }

                if (!string.IsNullOrWhiteSpace(otlpEndpoint))
                {
                    tracing.AddOtlpExporter(options =>
                        ConfigureOtlpExporter(options, otlpEndpoint, otlpHeaders, otlpProtocol));
                }
            })
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddPrometheusExporter();

                if (enableConsoleExporter)
                {
                    metrics.AddConsoleExporter();
                }

                if (!string.IsNullOrWhiteSpace(otlpEndpoint))
                {
                    metrics.AddOtlpExporter(options =>
                        ConfigureOtlpExporter(options, otlpEndpoint, otlpHeaders, otlpProtocol));
                }
            });

        return services;
    }

    public static WebApplication MapObservabilityEndpoints(this WebApplication app)
    {
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.MapHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains("live"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.MapPrometheusScrapingEndpoint("/metrics");

        return app;
    }

    private static void ConfigureOtlpExporter(
        OtlpExporterOptions options,
        string endpoint,
        string? headers,
        OtlpExportProtocol protocol)
    {
        if (!Uri.TryCreate(endpoint, UriKind.Absolute, out var exporterEndpoint))
        {
            throw new InvalidOperationException("OpenTelemetry:Otlp:Endpoint must be a valid absolute URI.");
        }

        options.Endpoint = exporterEndpoint;
        options.Protocol = protocol;

        if (!string.IsNullOrWhiteSpace(headers))
        {
            options.Headers = headers;
        }
    }

    private static OtlpExportProtocol ParseOtlpProtocol(string? protocol)
    {
        return Enum.TryParse<OtlpExportProtocol>(protocol, ignoreCase: true, out var parsed)
            ? parsed
            : OtlpExportProtocol.Grpc;
    }
}
