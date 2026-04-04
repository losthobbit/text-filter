using NSubstitute;
using TextFilter.App.Application;
using TextFilter.App.Application.Interfaces;

namespace TextFilter.Tests.Application;

public class TextFilterServiceTests
{
    private readonly ITextReader _mockTextReader = Substitute.For<ITextReader>();
    private readonly IOutputWriter _mockOutputWriter = Substitute.For<IOutputWriter>();

    [Fact]
    void Run_ReadsInputAndWritesFilteredOutput()
    {
        _mockTextReader.Read().Returns("some input");

        new TextFilterService(_mockTextReader, _mockOutputWriter).Run();

        _mockTextReader.Received().Read();
        _mockOutputWriter.Received().Write(Arg.Any<string>());
    }
}
