using Microsoft.AspNetCore.Mvc;
using site.TheyVoteForYou;

namespace site.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly ITheyVoteForYouApiClient _theyVoteForYouApiClient;

        public PolicyController(ITheyVoteForYouApiClient theyVoteForYouApiClient)
        {
            _theyVoteForYouApiClient = theyVoteForYouApiClient;
        }

        [HttpGet]
        [Route("{policyId:int}")]
        [Produces(typeof(PartyAgreement[]))]
        public async Task<IActionResult> GetAgreementsByParty(int policyId)
        {
            var policy = await _theyVoteForYouApiClient.GetPolicy(policyId);

            if (policy == null)
                return NotFound();

            var partySummaries = policy.people_comparisons
                .GroupBy(p => p.person.latest_member.party)
                .OrderByDescending(g => g.Count())
                .Select(g => new PartyAgreement(g.Key, g.Select(p => p.agreement).ToArray()));

            return Ok(partySummaries);
        }
    }

    public record PartyAgreement(
        string party,
        double[] agreements);
}
