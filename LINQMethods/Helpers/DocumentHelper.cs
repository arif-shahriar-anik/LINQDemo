using System.Text.RegularExpressions;

namespace LINQMethods.Helpers;

public static partial class DocumentHelper
{
    [GeneratedRegex(@"[\w]+")] 
    private static partial Regex WordMatcher();
    
    /// <summary>
    /// Splits the input document into individual words.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static IEnumerable<string> SplitWords(string document) =>
        WordMatcher().Matches(document).Select(static match => match.Value);
}