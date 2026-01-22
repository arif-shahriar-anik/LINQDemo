using LINQMethods.Models;

namespace LINQDemo;

class Program
{
    /// <summary>
    /// Represents a constant string as an input document
    /// in the context of term frequency operations, such as counting word occurrences.
    /// </summary>
    private const string Document =
        "In .NET., LINQ makes data queries readable, and CountBy makes counting terms easy. " +
        "CountBy is great for frequency counters, keyword extraction, and simple search ranking. " +
        "In many apps, counting terms is the first step toward relevance scoring.";

    static void Main(string[] args)
    {
        // CountByExample();
    }
    
    /// <summary>
    /// Categorizes a probability into "Low", "Medium", or "High" buckets.
    /// </summary>
    /// <param name="probability"></param>
    /// <returns></returns>
    private static string Bucket(double probability) => probability switch
    {
        < 0.33 => "Low",
        <= 0.66 => "Medium",
        _ => "High"
    };

    /// <summary>
    /// Demonstrates the functionality of the CountBy LINQ method
    /// </summary>
    private static void CountByExample()
    {
        var wordFrequency = LINQMethods
            .CountByMethod.CalculateTermFrequency(Document);

        foreach (var (word, frequency) in wordFrequency)
            Console.WriteLine($"{word}: {frequency}");
    }
}