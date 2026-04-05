using Shouldly;
using TextFilter.App.Infrastructure;

namespace TextFilter.Tests.Infrastructure;

public class FileTextReaderTests
{
    [Fact]
    void Read_ReturnsFileContents()
    {
        var path = Path.GetTempFileName();
        try
        {
            File.WriteAllText(path, "hello world");
            var result = new FileTextReader(path).Read();
            result.ShouldBe("hello world");
        }
        finally
        {
            File.Delete(path);
        }
    }
}
