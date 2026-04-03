namespace TextFilter.App.Domain.Matchers;

public interface IWordMatcher
{
    bool Matches(string word);
}