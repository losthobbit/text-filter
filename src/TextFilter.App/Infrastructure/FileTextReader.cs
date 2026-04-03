using TextFilter.App.Application.Interfaces;

namespace TextFilter.App.Infrastructure;

public class FileTextReader : ITextReader
{
    private readonly string _filename;

    public FileTextReader(string filename) => _filename = filename;

    public string Read() => File.ReadAllText(_filename);
}