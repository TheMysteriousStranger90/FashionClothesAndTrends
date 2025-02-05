using Hangfire.Dashboard;

namespace FashionClothesAndTrends.WebAPI.Filters;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    private readonly ILogger<HangfireAuthorizationFilter> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HangfireAuthorizationFilter(ILogger<HangfireAuthorizationFilter> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public bool Authorize(DashboardContext context)
    {
        HttpContext httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null)
        {
            _logger.LogWarning("HttpContext is null. Authorization denied.");
            return false;
        }

        if (httpContext.User.Identity == null || !httpContext.User.Identity.IsAuthenticated)
        {
            _logger.LogInformation("User is not authenticated. Authorization denied.");
            return false;
        }

        if (!httpContext.User.IsInRole("Administrator"))
        {
            _logger.LogInformation("User is not in the 'Administrator' role. Authorization denied.");
            return false;
        }

        _logger.LogInformation("User is authorized to access the Hangfire dashboard.");
        return true;
    }
}