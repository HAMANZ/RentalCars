using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Account;

public class AccessDeniedModel : PageModel
{
    private readonly ILogger<AccessDeniedModel> _logger;

    public AccessDeniedModel(ILogger<AccessDeniedModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        var userName = User.Identity?.Name ?? "Unknown";
        var requestPath = HttpContext.Request.Path;

        _logger.LogWarning("Access denied for user {User} to {Path}", userName, requestPath);
    }
}
