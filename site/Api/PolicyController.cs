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
        public async Task<IActionResult> GetPolicy()
        {
            var policy = await _policyService.getstuff();
            
            if (policy == null)
                return NotFound();
            
            return Ok(new [] {1, 2, 3, 4, 5, 6});
        }
    }
}
