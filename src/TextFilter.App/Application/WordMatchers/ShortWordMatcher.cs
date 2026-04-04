namespace TextFilter.App.Application.WordMatchers;

internal class ShortWordMatcher : IWordMatcher
{
    public bool Matches(string word)
    {
        return word.Length < 3;
    }
}
