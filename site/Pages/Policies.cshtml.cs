using Microsoft.AspNetCore.Mvc.RazorPages;
using site.TheyVoteForYou;
using site.TheyVoteForYou.Models;

namespace site.Pages;

public class PoliciesModel : PageModel
{
    public PolicyListItem[] Policies { get; set; }

    private readonly ITheyVoteForYouApiClient _theyVoteForYouApiClient;
    private readonly ILogger<IndexModel> _logger;

    public PoliciesModel(
        ITheyVoteForYouApiClient theyVoteForYouApiClient,
        ILogger<IndexModel> logger)
    {
        _theyVoteForYouApiClient = theyVoteForYouApiClient;
        _logger = logger;
    }

    public async Task OnGet()
    {
        var policies = await _theyVoteForYouApiClient.GetAllPolicies();
        Policies = policies.OrderByDescending(p => p.last_edited_at).ToArray();
    }
}
