namespace TextFilter.App.Domain.Matchers;

internal class ContainsTMatcher : IWordMatcher
{
    public bool Matches(string word)
    {
        return word.Contains('t');
    }
}
