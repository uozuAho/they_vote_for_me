using Microsoft.AspNetCore.Mvc.RazorPages;

namespace site.Pages;

public class IndexModel : PageModel
{
    public Policy Policy { get; set; } = new Policy("derp");

    private readonly IPolicyService _policyService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(
        IPolicyService policyService,
        ILogger<IndexModel> logger)
    {
        _policyService = policyService;
        _logger = logger;
    }

    public async Task OnGet()
    {
        var policy = await _policyService.getstuff();
        if (policy != null) Policy = policy;
    }
}
