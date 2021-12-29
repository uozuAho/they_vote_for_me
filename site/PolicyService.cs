namespace site
{
    public record Policy(
        string name,
        string description,
        bool provisional,
        PersonComparison[] people_comparisons);

    public record PersonComparison(
        Member latest_member,
        double agreement,
        bool voted);

    public record Member(Name name, string house, string party);

    public record Name(string first, string last);

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
