using TextFilter.App.Application.IO;
using TextFilter.App.Application.WordMatchers;

namespace TextFilter.App.Application;

public class TextFilterService
{
    private readonly ITextReader _textReader;
    private readonly IOutputWriter _outputWriter;
    private readonly WordFilter _wordFilter = new();
    private readonly IWordMatcher[] _matchers = [
        new MiddleVowelMatcher(),
        new ShortWordMatcher(),
        new ContainsTMatcher(),
    ];

    public TextFilterService(ITextReader textReader, IOutputWriter outputWriter)
    {
        _textReader = textReader;
        _outputWriter = outputWriter;
    }

    public void Run()
    {
        var text = _textReader.Read();
        text = _wordFilter.Filter(text, _matchers);
        _outputWriter.Write(text);
    }
}
