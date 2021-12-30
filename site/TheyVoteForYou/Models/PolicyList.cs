namespace site.TheyVoteForYou.Models
{
    public record PolicyListItem(
        int id,
        string name,
        string description,
        bool provisional,
        DateTime last_edited_at
    );
}
