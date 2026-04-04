namespace TextFilter.App.Application.WordMatchers;

internal class ContainsTMatcher : IWordMatcher
{
    public bool Matches(string word)
    {
        return word.Contains('t');
    }
}
