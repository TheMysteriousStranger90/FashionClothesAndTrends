namespace FashionClothesAndTrends.WebAPI.Extensions;

public static class SecureConfigurationExtensions
{
    public static string GetRequiredSqlConnectionString(this IConfiguration configuration)
    {
        var profile = ResolveConnectionProfile();

        var dockerValue = FirstNonEmpty(
            configuration.GetConnectionString("DefaultDockerDbConnection"),
            configuration["ConnectionStrings__DefaultDockerDbConnection"]);

        var localValue = FirstNonEmpty(
            configuration.GetConnectionString("DefaultLocalDbConnection"),
            configuration["ConnectionStrings__DefaultLocalDbConnection"]);

        return profile switch
               {
                   ConnectionProfile.Docker => FirstNonEmpty(dockerValue, localValue),
                   ConnectionProfile.Local => FirstNonEmpty(localValue, dockerValue),
                   _ => FirstNonEmpty(localValue, dockerValue)
               }
               ?? throw new InvalidOperationException(
                   "SQL connection string is missing. Configure local or docker connection via env/User Secrets.");
    }

    public static string GetRequiredRedisConnectionString(this IConfiguration configuration)
    {
        var profile = ResolveConnectionProfile();

        var dockerValue = FirstNonEmpty(
            configuration.GetConnectionString("Redis"),
            configuration["ConnectionStrings__Redis"]);

        var localValue = FirstNonEmpty(
            configuration.GetConnectionString("RedisLocalDb"),
            configuration["ConnectionStrings__RedisLocalDb"]);

        return profile switch
               {
                   ConnectionProfile.Docker => FirstNonEmpty(dockerValue, localValue),
                   ConnectionProfile.Local => FirstNonEmpty(localValue, dockerValue),
                   _ => FirstNonEmpty(localValue, dockerValue)
               }
               ?? throw new InvalidOperationException(
                   "Redis connection string is missing. Configure local or docker connection via env/User Secrets.");
    }

    private static ConnectionProfile ResolveConnectionProfile()
    {
        var explicitProfile = Environment.GetEnvironmentVariable("Database__ConnectionProfile");

        if (string.Equals(explicitProfile, "docker", StringComparison.OrdinalIgnoreCase))
        {
            return ConnectionProfile.Docker;
        }

        if (string.Equals(explicitProfile, "local", StringComparison.OrdinalIgnoreCase))
        {
            return ConnectionProfile.Local;
        }

        var isRunningInContainer = string.Equals(
            Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"),
            "true",
            StringComparison.OrdinalIgnoreCase);

        return isRunningInContainer ? ConnectionProfile.Docker : ConnectionProfile.Local;
    }

    private static string? FirstNonEmpty(params string?[] candidates)
    {
        return candidates.FirstOrDefault(value => !string.IsNullOrWhiteSpace(value));
    }

    private enum ConnectionProfile
    {
        Local,
        Docker
    }
}
