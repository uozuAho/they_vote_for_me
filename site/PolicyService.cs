using Microsoft.Extensions.Options;

namespace site
{
    public record Policy(
        string name,
        string description,
        bool provisional,
        PersonComparison[] people_comparisons);

    public record PersonComparison(
        Person person,
        double agreement,
        bool voted);

    public record Person(Member latest_member);

    public record Member(Name name, string house, string party);

    public record Name(string first, string last);

    public interface IPolicyService
    {
        Task<Policy?> getstuff();
    }

    public class PolicyServiceConfig
    {
        public string ApiKey { get; set; } = "";
    }

    public class PolicyService : IPolicyService
    {
        private readonly string _apiKey;

        public PolicyService(IOptions<PolicyServiceConfig> config)
        {
            _apiKey = config.Value.ApiKey;
        }

        public async Task<Policy?> getstuff()
        {
            var client = new HttpClient();
            var policy = await client.GetFromJsonAsync<Policy>(
                $"https://theyvoteforyou.org.au/api/v1/policies/1.json?key={_apiKey}");

            return policy;
        }
    }
}
