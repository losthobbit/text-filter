namespace TextFilter.App.Application.WordMatchers;

public interface IWordMatcher
{
    bool Matches(string word);
}