using Shouldly;
using TextFilter.App.Application.WordMatchers;

namespace TextFilter.Tests.Application.Filters;

public class ShortWordMatcherTests
{
    private readonly ShortWordMatcher _matcher;

    public ShortWordMatcherTests()
    {
        _matcher = new ShortWordMatcher();
    }

    [Theory]
    [InlineData("single character", "a")]
    [InlineData("two characters", "be")]
    public void Matches_WithLengthLessThanThree_ReturnsTrue(string _, string input)
    {
        var actual = _matcher.Matches(input);
        actual.ShouldBeTrue();
    }

    [Theory]
    [InlineData("exactly three characters", "the")]
    [InlineData("more than three characters", "clean")]
    public void Matches_WithLengthThreeOrMore_ReturnsFalse(string _, string input)
    {
        var actual = _matcher.Matches(input);
        actual.ShouldBeFalse();
    }
}
