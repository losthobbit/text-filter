using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using TextFilter.App.Application;
using TextFilter.App.Application.Interfaces;

namespace TextFilter.Tests.Application;

public class TestFilterServiceTests
{
    private readonly TextFilterService _sut;

    private readonly IOutputWriter _mockOutputWriter;
    private readonly ITextReader _mockTextReader;

    public TestFilterServiceTests()
    {
        _mockTextReader = Substitute.For<ITextReader>();
        _mockOutputWriter = Substitute.For<IOutputWriter>();

        _sut = new TextFilterService(_mockTextReader, _mockOutputWriter);
    }

    [Fact]
    void Run_OutputsFilteredText()
    {
        const string input = "bb `k33p \"`beeep ,IHaveAt";
        var wordsToKeep = new[] { "k33p" };
        var wordsToRemove = new[] { "bb", "beeep", "IHaveAt" };

        string? actual = null;
        _mockTextReader.Read().Returns(input);
        _mockOutputWriter.Write(Arg.Do<string>(text => actual = text));

        _sut.Run();

        actual.ShouldNotBeNull();
        foreach (var word in wordsToKeep)
            actual.ShouldContain(word);
        foreach (var word in wordsToRemove)
            actual.ShouldNotContain(word);
    }
}
