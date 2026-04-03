namespace TextFilter.App.Application.Matchers;

public interface IWordMatcher
{
    bool Matches(string word);
}