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

    /// <summary>
    /// Program entry point; executes the AggregateByExample demonstration.
    /// </summary>
    /// <param name="args">Command-line arguments supplied to the process (not used by this demo).</param>
    static void Main(string[] args)
    {
        // CountByExample();
        AggregateByExample();
    }
        
    /// <summary>
    /// Demonstrates the functionality of the AggregateBy LINQ method.
    /// <summary>
    /// Demonstrates grouping a hard-coded set of probabilities into "Low", "Medium", and "High" buckets and prints each bucket's average probability to the console.
    /// </summary>
    private static void AggregateByExample()
    {
        double[] probabilities = [0.05, 0.12, 0.31, 0.40, 0.55, 0.66, 0.71, 0.88, 0.97];

        var averageByBucket = probabilities
            .Select(static probability => (Probability: probability, Bucket: Bucket(probability)))
            .AggregateBy(
                keySelector: static bin => bin.Bucket,
                seedSelector: static _ => new AverageAccumulator(0, 0),
                func: static (accumulator, bin) => accumulator.Add(bin.Probability));
            
        Console.WriteLine("\nAverage Probability by Bucket:");
        foreach (var (bucket, averageAccumulator) in averageByBucket)
            Console.WriteLine($"{bucket}: {averageAccumulator.Value:P0}");
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