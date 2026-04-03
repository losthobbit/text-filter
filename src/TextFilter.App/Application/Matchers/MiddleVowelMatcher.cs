namespace TextFilter.App.Application.Matchers;

internal class MiddleVowelMatcher : IWordMatcher
{
    private const string Vowels = "aeiou";

    public bool Matches(string word)
    {
        if (word.Length == 0)
            return false;

        var mid = word.Length / 2;

        if (word.Length % 2 == 1)
            return Vowels.Contains(char.ToLower(word[mid]));

        return 
            Vowels.Contains(char.ToLower(word[mid - 1])) ||
            Vowels.Contains(char.ToLower(word[mid]));
    }
}
