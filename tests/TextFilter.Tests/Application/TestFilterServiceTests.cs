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

    [Theory]
    [InlineData("No words removed", "hello", "hello")]
    [InlineData("All types removed", "bb k33p beeep IHaveAt", " k33p  ")]
    void Run_OutputsFilteredText(string _, string input, string expected)
    {
        string? actual = null;
        _mockTextReader.Read().Returns(input);
        _mockOutputWriter.Write(Arg.Do<string>(text => actual = text));

        _sut.Run();

        actual.ShouldBe(expected);
    }
}
