using Shouldly;
using TextFilter.App.Domain.Matchers;

namespace TextFilter.Tests.Domain.Filters;

public class MiddleVowelMatcherTests
{
    private readonly MiddleVowelMatcher _matcher;

    public MiddleVowelMatcherTests()
    {
        _matcher = new MiddleVowelMatcher();
    }

    [Theory]
    [InlineData("odd length word with middle vowel", "clean")]
    [InlineData("odd length word with middle vowel 'i'", "shirt")]
    [InlineData("even length word with first middle letter a vowel 'o'", "long")]
    [InlineData("even length word with second middle letter a vowel 'a'", "what")]
    [InlineData("two character word with vowel 'u'", "up")]
    [InlineData("case insensitive vowel check", "CLEAN")]
    [InlineData("single character vowel", "a")]
    public void Matches_WithVowelsInMiddle_ReturnsTrue(string _, string input)
    {
        var actual = _matcher.Matches(input);
        actual.ShouldBeTrue();
    }

    [Theory]
    [InlineData("odd length word with middle consonant", "try")]
    [InlineData("even length word with neither middle letter a vowel", "rather")]
    [InlineData("single character consonant", "b")]
    [InlineData("empty string", "")]
    public void Matches_WithoutVowelsInMiddle_ReturnsFalse(string _, string input)
    {
        var actual = _matcher.Matches(input);
        actual.ShouldBeFalse();
    }
}
