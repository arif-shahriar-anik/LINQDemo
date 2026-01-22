using System.Text.RegularExpressions;

namespace LINQMethods;

public static partial class CountByMethod
{
    [GeneratedRegex(@"[\w]+")] 
    private static partial Regex WordMatcher();

    /// <summary>
    /// Calculates the term frequency for all words in a given document.
    /// </summary>
    /// <param name="document">The input string representing the document to process.</param>
    /// <returns>A collection of key-value pairs where the key is a word and the value is the frequency of that word in the document.</returns>
    public static IEnumerable<KeyValuePair<string, int>> CalculateTermFrequency(
        string document)
    {
        var words = WordMatcher()
            .Matches(document)
            .Select(static match => match.Value.ToLowerInvariant());

        return words
            .CountBy(static word => word)
            .OrderByDescending(static keyValuePair => keyValuePair.Value);
    }

    /// <summary>
    /// Calculates the term frequency for all words in a given document.
    /// </summary>
    /// <param name="document">The input string containing the document to analyze.</param>
    /// <returns>A collection of key-value pairs where each key is a word and the corresponding value is its frequency in the document.</returns>
    public static IEnumerable<KeyValuePair<string, int>> CalculateTermFrequencyOld(
        string document)
    {
        var words = WordMatcher()
            .Matches(document)
            .Select(static match => match.Value.ToLowerInvariant());
        
        return words
            .GroupBy(static word => word)
            .Select(static group => new KeyValuePair<string, int>(group.Key, group.Count()))
            .OrderByDescending(static keyValuePair => keyValuePair.Value);
    }
}