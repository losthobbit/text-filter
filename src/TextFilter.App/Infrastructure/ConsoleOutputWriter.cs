using TextFilter.App.Application.Interfaces;

namespace TextFilter.App.Infrastructure;

public class ConsoleOutputWriter : IOutputWriter
{
    public void Write(string text) => Console.WriteLine(text);
}