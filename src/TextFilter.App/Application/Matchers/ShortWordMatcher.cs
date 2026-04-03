namespace TextFilter.App.Application.Matchers;

internal class ShortWordMatcher : IWordMatcher
{
    public bool Matches(string word)
    {
        return word.Length < 3;
    }
}
