using Microsoft.AspNetCore.Mvc.RazorPages;
using site.TheyVoteForYou;
using site.TheyVoteForYou.Models;

namespace site.Pages;

public class IndexModel : PageModel
{
    public PolicyListItem[] Policies { get; set; }

    private readonly ITheyVoteForYouApiClient _theyVoteForYouApiClient;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(
        ITheyVoteForYouApiClient theyVoteForYouApiClient,
        ILogger<IndexModel> logger)
    {
        _theyVoteForYouApiClient = theyVoteForYouApiClient;
        _logger = logger;
    }

    public async Task OnGet()
    {
        Policies = await _theyVoteForYouApiClient.GetAllPolicies();
    }
}
