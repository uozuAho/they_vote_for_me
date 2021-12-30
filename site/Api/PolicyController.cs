using Microsoft.AspNetCore.Mvc;

namespace site.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAgreementsByParty()
        {
            var policy = await _policyService.getstuff();

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
