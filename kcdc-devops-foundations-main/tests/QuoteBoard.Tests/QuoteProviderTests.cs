using QuoteBoard.Web.Services;
using Xunit;

namespace QuoteBoard.Tests;

public class QuoteProviderTests
{
    private readonly QuoteProvider _sut = new();

    [Fact]
    public void GetAll_ReturnsAtLeastFiveQuotes() =>
        Assert.True(_sut.GetAll().Count >= 5);

    [Fact]
    public void GetDailyQuote_IsDeterministicForSameDate()
    {
        var date = new DateOnly(2026, 8, 13);
        Assert.Equal(_sut.GetDailyQuote(date), _sut.GetDailyQuote(date));
    }

    [Fact]
    public void GetDailyQuote_NeverThrows_ForAnyDate()
    {
        for (var d = 0; d < 3650; d += 37)
            Assert.False(string.IsNullOrEmpty(
                _sut.GetDailyQuote(DateOnly.FromDayNumber(730000 + d))));
    }
}
