namespace FashionClothesAndTrends.WebAPI.Extensions;

public static class EnvironmentBootstrapExtensions
{
    public static void LoadDotEnvIntoProcessEnvironment()
    {
        foreach (var envPath in FindDotEnvPaths())
        {
            if (!File.Exists(envPath))
            {
                continue;
            }

            var values = ParseDotEnv(envPath);
            var profile = ResolveProfile(values);

            foreach (var (key, value) in values)
            {
                var normalizedValue = NormalizeValue(key, value, profile);

                if (string.IsNullOrWhiteSpace(normalizedValue))
                {
                    continue;
                }

                var currentValue = Environment.GetEnvironmentVariable(key);
                if (string.IsNullOrWhiteSpace(currentValue))
                {
                    Environment.SetEnvironmentVariable(key, normalizedValue);
                }
            }

            // The closest discovered .env is enough.
            break;
        }
    }

    private static string ResolveProfile(IReadOnlyDictionary<string, string> values)
    {
        return values.TryGetValue("Database__ConnectionProfile", out var profile)
            ? profile
            : "local";
    }

    private static string NormalizeValue(string key, string value, string profile)
    {
        if (!string.Equals(profile, "local", StringComparison.OrdinalIgnoreCase))
        {
            return value;
        }

        if (!string.Equals(key, "ASPNETCORE_Kestrel__Certificates__Default__Path", StringComparison.OrdinalIgnoreCase))
        {
            return value;
        }

        if (!value.StartsWith("/https/", StringComparison.Ordinal))
        {
            return value;
        }

        var localCertPath = FindLocalCertPath();
        return localCertPath ?? value;
    }

    private static string? FindLocalCertPath()
    {
        foreach (var root in EnumerateCurrentAndParentDirectories())
        {
            var directCandidate = Path.Combine(root.FullName, "certs", "localhost.pfx");
            if (File.Exists(directCandidate))
            {
                return directCandidate;
            }

            var nestedCandidate = Path.Combine(root.FullName, "src", "FashionClothesAndTrends.WebAPI", "certs",
                "localhost.pfx");
            if (File.Exists(nestedCandidate))
            {
                return nestedCandidate;
            }
        }

        return null;
    }

    private static Dictionary<string, string> ParseDotEnv(string envPath)
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var line in File.ReadLines(envPath))
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

        return result;
    }

    private static IEnumerable<string> FindDotEnvPaths()
    {
        foreach (var directory in EnumerateCurrentAndParentDirectories())
        {
            yield return Path.Combine(directory.FullName, ".env");
        }
    }

    private static IEnumerable<DirectoryInfo> EnumerateCurrentAndParentDirectories()
    {
        var current = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (current != null)
        {
            yield return current;
            current = current.Parent;
        }
    }
}
