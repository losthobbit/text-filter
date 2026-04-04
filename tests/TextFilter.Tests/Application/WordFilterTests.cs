using NSubstitute;
using Shouldly;
using TextFilter.App.Application;
using TextFilter.App.Application.WordMatchers;

namespace TextFilter.Tests.Application;

public class WordFilterTests
{
    private readonly WordFilter _sut = new();

    [Theory]
    [InlineData("no punctuation", "keep remove gone")]
    [InlineData("with punctuation", "keep, remove. gone!")]
    void Filter_RemovesOnlyWordsThatMatchAnyMatcher(string _, string input)
    {
        var matcherA = Substitute.For<IWordMatcher>();
        var matcherB = Substitute.For<IWordMatcher>();
        matcherA.Matches(Arg.Any<string>()).Returns(false);
        matcherB.Matches(Arg.Any<string>()).Returns(false);
        matcherA.Matches("remove").Returns(true);
        matcherB.Matches("gone").Returns(true);

        var result = _sut.Filter(input, [matcherA, matcherB]);

        result.ShouldContain("keep");
        result.ShouldNotContain("remove");
        result.ShouldNotContain("gone");
    }
}
