using Shouldly;
using TextFilter.App.Domain.Matchers;

namespace TextFilter.Tests.Domain.Filters;

public class ContainsTMatcherTests
{
    private readonly ContainsTMatcher _matcher;

    public ContainsTMatcherTests()
    {
        _matcher = new ContainsTMatcher();
    }

    [Theory]
    [InlineData("'t' at the start of the word", "the")]
    [InlineData("'t' in the middle of the word", "filter")]
    [InlineData("'t' at the end of the word", "cat")]
    public void Matches_WordContainsT_ReturnsTrue(string _, string input)
    {
        var actual = _matcher.Matches(input);
        actual.ShouldBeTrue();
    }

    [Theory]
    [InlineData("word with no 't'", "clean")]
    [InlineData("another word with no 't'", "bird")]
    public void Matches_WordDoesNotContainT_ReturnsFalse(string _, string input)
    {
        var actual = _matcher.Matches(input);
        actual.ShouldBeFalse();
    }
}
