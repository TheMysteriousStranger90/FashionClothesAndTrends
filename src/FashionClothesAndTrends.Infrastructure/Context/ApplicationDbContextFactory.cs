using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FashionClothesAndTrends.Infrastructure.Context;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var values = LoadEnvironmentLikeValues();
        var profile = ResolveConnectionProfile(values);

        var dockerSql = GetValue(values, "ConnectionStrings__DefaultDockerDbConnection");
        var localSql = GetValue(values, "ConnectionStrings__DefaultLocalDbConnection");

        var connectionString = profile switch
        {
            ConnectionProfile.Docker => FirstNonEmpty(dockerSql, localSql),
            ConnectionProfile.Local => FirstNonEmpty(localSql, dockerSql),
            _ => FirstNonEmpty(localSql, dockerSql)
        };

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "SQL connection string is missing for design-time DbContext. " +
                "Set ConnectionStrings__DefaultLocalDbConnection or ConnectionStrings__DefaultDockerDbConnection in environment variables or .env file.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }

    private static ConnectionProfile ResolveConnectionProfile(IDictionary<string, string> values)
    {
        var explicitProfile = GetValue(values, "Database__ConnectionProfile");

        if (string.Equals(explicitProfile, "docker", StringComparison.OrdinalIgnoreCase))
        {
            return ConnectionProfile.Docker;
        }

        if (string.Equals(explicitProfile, "local", StringComparison.OrdinalIgnoreCase))
        {
            return ConnectionProfile.Local;
        }

        var isContainer = string.Equals(
            Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"),
            "true",
            StringComparison.OrdinalIgnoreCase);

        return isContainer ? ConnectionProfile.Docker : ConnectionProfile.Local;
    }

    private static Dictionary<string, string> LoadEnvironmentLikeValues()
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (System.Collections.DictionaryEntry variable in Environment.GetEnvironmentVariables())
        {
            if (variable.Key is string key && variable.Value is string value)
            {
                result[key] = value;
            }
        }

        foreach (var path in CandidateEnvPaths())
        {
            if (!File.Exists(path))
            {
                continue;
            }

            foreach (var line in File.ReadAllLines(path))
            {
                var trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith('#'))
                {
                    continue;
                }

                var separatorIndex = trimmed.IndexOf('=');
                if (separatorIndex <= 0)
                {
                    continue;
                }

                var key = trimmed[..separatorIndex].Trim();
                var value = trimmed[(separatorIndex + 1)..].Trim();

                if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
                {
                    result[key] = value;
                }
            }
        }

        return result;
    }

    private static IEnumerable<string> CandidateEnvPaths()
    {
        var current = Directory.GetCurrentDirectory();

        yield return Path.Combine(current, ".env");
        yield return Path.GetFullPath(Path.Combine(current, "..", ".env"));
        yield return Path.GetFullPath(Path.Combine(current, "..", "..", ".env"));
        yield return Path.GetFullPath(Path.Combine(current, "..", "..", "..", ".env"));
    }

    private static string? GetValue(IDictionary<string, string> values, string key)
    {
        return values.TryGetValue(key, out var value) && !string.IsNullOrWhiteSpace(value)
            ? value
            : null;
    }

    private static string? FirstNonEmpty(params string?[] values)
    {
        return values.FirstOrDefault(v => !string.IsNullOrWhiteSpace(v));
    }

    private enum ConnectionProfile
    {
        Local,
        Docker
    }
}

