namespace FashionClothesAndTrends.WebAPI.Extensions;

public static class SecureConfigurationExtensions
{
    public static string GetRequiredSqlConnectionString(this IConfiguration configuration)
    {
        return configuration.GetConnectionString("DefaultDockerDbConnection")
               ?? configuration.GetConnectionString("DefaultLocalDbConnection")
               ?? configuration["ConnectionStrings__DefaultDockerDbConnection"]
               ?? configuration["ConnectionStrings__DefaultLocalDbConnection"]
               ?? throw new InvalidOperationException(
                   "SQL connection string is missing. Configure it via User Secrets or environment variables.");
    }

    public static string GetRequiredRedisConnectionString(this IConfiguration configuration)
    {
        return configuration.GetConnectionString("Redis")
               ?? configuration.GetConnectionString("RedisLocalDb")
               ?? configuration["ConnectionStrings__Redis"]
               ?? configuration["ConnectionStrings__RedisLocalDb"]
               ?? throw new InvalidOperationException(
                   "Redis connection string is missing. Configure it via User Secrets or environment variables.");
    }
}
