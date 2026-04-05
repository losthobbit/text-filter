using TextFilter.App.Application.IO;

namespace TextFilter.App.Infrastructure;

public class ConsoleOutputWriter : IOutputWriter
{
    public void Write(string text) => Console.WriteLine(text);
}