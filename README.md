# Usage

```
dotnet run --project src/TextFilter.App -- <path-to-file>
```

Example:
```
dotnet run --project src/TextFilter.App -- c:\temp\alice.txt
```

# Assumptions

## Output Formatting
The brief specifies "filter out words" but does not specify how the 
output string should be formatted after removal. We have assumed that 
words are removed as-is, leaving all surrounding punctuation, spacing, 
and structure exactly as it appears in the original text.

## Word Definition
A word is defined as a contiguous sequence of word characters (`\w+`),
which includes letters, digits, and underscores. This means punctuation 
and whitespace are not considered part of a word and are preserved in 
the output. Numbers are treated as words (e.g. "123" is a valid word).

## Case Sensitivity
Filter 3 is case-sensitive, as the requirement is unclear, so will filter
out words with `t`, but not `T`.

## Filter1 — Vowel in Middle
- For odd-length words, the middle is the single centre character.
  e.g. "clean" (length 5) → middle is index 2 → 'e'
- For even-length words, the middle is the two centre characters.
  e.g. "what" (length 4) → middle is indices 1,2 → 'ha'
- The word is filtered if **either** middle character is a vowel.
- Words of length 1 or 2 have no meaningful "middle" distinct from 
  their entire content and are handled by Filter2 anyway.

## Filter Ordering
Filters are applied sequentially: Filter1 → Filter2 → Filter3. 
The output of each filter is the input to the next.

## File Input
- The input file path is provided as a command-line argument (`args[0]`).
- The file is assumed to exist and be readable. No file-not-found 
  handling is implemented.
- The file is assumed to be UTF-8 encoded. `File.ReadAllText` is used 
  with default encoding (UTF-8 with BOM detection).
- No guarantees are made about preserving line endings in the output.

## Word Splitting for FilterBase
`Regex.Matches(\w+)` is used to identify words. Each matched word is 
tested against the filter predicate. If the predicate returns true, 
the word is removed from the string using `Regex.Replace` with a 
word-boundary pattern (`\bword\b`) to avoid partial matches.

## Language
- English only. Vowels are defined as `aeiou` (case-insensitive).
- Accented or non-Latin characters (e.g. é, ü, ñ) are not considered 
  vowels by this implementation.
  
