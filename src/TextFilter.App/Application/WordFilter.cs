using System.Text.RegularExpressions;
using TextFilter.App.Application.WordMatchers;

namespace TextFilter.App.Application;

internal partial class WordFilter
{
    public string Filter(string text, IEnumerable<IWordMatcher> matchers)
    {
        foreach (var matcher in matchers)
        {
            text = WordMatchRegex().Replace(text, match =>
                matcher.Matches(match.Value) ? string.Empty : match.Value);
        }
        return text;
    }

    [GeneratedRegex(@"\w+")]
    private static partial Regex WordMatchRegex();
}
