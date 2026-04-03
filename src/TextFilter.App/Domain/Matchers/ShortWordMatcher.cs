namespace TextFilter.App.Domain.Matchers;

internal class ShortWordMatcher : IWordMatcher
{
    public bool Matches(string word)
    {
        return word.Length < 3;
    }
}
