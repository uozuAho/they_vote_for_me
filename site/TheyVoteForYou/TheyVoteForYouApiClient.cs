using Microsoft.Extensions.Options;
using site.TheyVoteForYou.Models;

namespace site.TheyVoteForYou
{
    public interface ITheyVoteForYouApiClient
    {
        Task<Policy?> GetPolicy();
    }

    public class TheyVoteForYouApiClient : ITheyVoteForYouApiClient
    {
        private readonly string _apiKey;

        public TheyVoteForYouApiClient(IOptions<TheyVoteForYouApiClientConfig> config)
        {
            _apiKey = config.Value.ApiKey;
        }

        public async Task<Policy?> GetPolicy()
        {
            var client = new HttpClient();
            var policy = await client.GetFromJsonAsync<Policy>(
                $"https://theyvoteforyou.org.au/api/v1/policies/1.json?key={_apiKey}");

            return policy;
        }
    }
}
