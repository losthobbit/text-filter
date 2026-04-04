using System.Text.RegularExpressions;
using TextFilter.App.Application.Interfaces;
using TextFilter.App.Application.WordMatchers;

namespace TextFilter.App.Application;

internal partial class TextFilterService
{
    private readonly IOutputWriter _outputWriter;
    private readonly ITextReader _textReader;
    private readonly IWordMatcher[] _wordMatchers = [
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
        text = Filter(text);
        _outputWriter.Write(text);
    }

    private string Filter(string text)
    {
        foreach (var wordMatcher in _wordMatchers)
        {
            text = WordMatchRegex().Replace(text, match =>
                wordMatcher.Matches(match.Value) ? string.Empty : match.Value);
        }
        return text;
    }

    [GeneratedRegex(@"\w+")]
    private static partial Regex WordMatchRegex();
}
