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
        [Produces(typeof(PolicyAgreementByParty[]))]
        public async Task<IActionResult> GetAgreementsByParty(int policyId)
        {
            var policy = await _theyVoteForYouApiClient.GetPolicy(policyId);

            if (policy == null)
                return NotFound();

            var partyAgreements = policy.people_comparisons
                .GroupBy(p => p.person.latest_member.party)
                .OrderByDescending(g => g.Count())
                .Select(g => new PartyAgreement(g.Key, g.Select(p => p.agreement).ToArray()));

            var myIndependentsDetails = policy.people_comparisons
                .Select(p => new MemberDetails(
                    p.person.latest_member.name.FullName,
                    p.person.latest_member.party,
                    p.person.latest_member.electorate,
                    p.agreement))
                .ToArray();

            return Ok(new PolicyAgreementByParty(
                policy.name,
                policy.description,
                partyAgreements.ToArray(),
                myIndependentsDetails)
            );
        }
    }

    public record PolicyAgreementByParty(
        string title,
        string description,
        PartyAgreement[] partyAgreements,
        MemberDetails[] memberDetails
    );

    public record PartyAgreement(string party, double[] agreements)
    {
        public string? color => PartyColour(party);

        private static string? PartyColour(string party)
        {
            return party switch
            {
                "National Party" => "aqua",
                "Liberal Party" => "blue",
                "Australian Labor Party" => "red",
                "Australian Greens" => "green",
                _ => null
            };
        }
    };

    public record MemberDetails(string name, string party, string electorate, double agreement);
}
