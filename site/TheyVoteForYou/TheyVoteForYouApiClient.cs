using Microsoft.Extensions.Options;
using site.TheyVoteForYou.Models;

namespace site.TheyVoteForYou
{
    public interface ITheyVoteForYouApiClient
    {
        Task<PolicyDetails?> GetPolicy();
    }

    public class TheyVoteForYouApiClient : ITheyVoteForYouApiClient
    {
        private readonly string _apiKey;

        public TheyVoteForYouApiClient(IOptions<TheyVoteForYouApiClientConfig> config)
        {
            _apiKey = config.Value.ApiKey;
        }

        public async Task<PolicyDetails?> GetPolicy()
        {
            var client = new HttpClient();
            var policy = await client.GetFromJsonAsync<PolicyDetails>(
                $"https://theyvoteforyou.org.au/api/v1/policies/1.json?key={_apiKey}");

            return policy;
        }
    }
}
