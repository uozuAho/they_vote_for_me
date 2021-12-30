namespace site.TheyVoteForYou.Models;

public record PolicyDetails(
    string name,
    string description,
    bool provisional,
    PersonComparison[] people_comparisons
);

public record PersonComparison(
    Person person,
    double agreement,
    bool voted
);

public record Person(Member latest_member);

public record Member(Name name, string house, string party);

public record Name(string first, string last);
