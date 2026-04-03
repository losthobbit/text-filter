using System.Text.RegularExpressions;
using TextFilter.App.Application.Interfaces;
using TextFilter.App.Application.Matchers;

namespace TextFilter.App.Application;

internal class TextFilterService
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
        foreach (var wordMatcher in _wordMatchers)
        {
            text = Regex.Replace(text, @"\w+", match =>
                wordMatcher.Matches(match.Value) ? string.Empty : match.Value);
        }
        _outputWriter.Write(text);
    }
}
