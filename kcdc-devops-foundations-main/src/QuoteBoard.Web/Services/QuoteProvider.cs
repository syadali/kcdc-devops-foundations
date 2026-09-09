namespace QuoteBoard.Web.Services;

public class QuoteProvider
{
    private static readonly List<string> Quotes =
    [
        "You build it, you run it. — Werner Vogels",
        "If it hurts, do it more often. — Martin Fowler",
        "Hope is not a strategy. — Google SRE",
        "Simplicity is a prerequisite for reliability. — Edsger Dijkstra",
        "Blameless postmortems turn incidents into investments."
    ];

    public IReadOnlyList<string> GetAll() => Quotes;

    public string GetDailyQuote(DateOnly date) =>
        Quotes[date.DayNumber % Quotes.Count];
}
