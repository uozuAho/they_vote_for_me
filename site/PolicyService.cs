namespace site
{
    public record Policy(string name);

    public class PolicyService : IPolicyService
    {
        public async Task<Policy?> getstuff()
        {
            var client = new HttpClient();
            var policy = await client.GetFromJsonAsync<Policy>(
                "https://theyvoteforyou.org.au/api/v1/policies/1.json?key=notForYou!!");

            return policy;
        }
    }

    public interface IPolicyService
    {
        Task<Policy?> getstuff();
    }
}
