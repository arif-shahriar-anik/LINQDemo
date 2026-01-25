using LINQMethods.Helpers;
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
        // AggregateByExample();
        // AggregateByExampleWithoutCustomAccumulator();
        IndexExample();
    }
        
    /// <summary>
    /// Demonstrates the functionality of the AggregateBy LINQ method.
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
        {
            Console.WriteLine($"{bucket}: {averageAccumulator.Value:P0}");
        }
    }
    
    /// <summary>
    /// Demonstrates the functionality of the AggregateBy LINQ method without using a custom accumulator.
    /// </summary>
    private static void AggregateByExampleWithoutCustomAccumulator()
    {
        double[] probabilities = [0.05, 0.12, 0.31, 0.40, 0.55, 0.66, 0.71, 0.88, 0.97];
        
        var averageByBucket = probabilities
            .Select(static probability => (probability: probability, bucket: Bucket(probability)))
            .AggregateBy(
                keySelector: static bin => bin.bucket,
                seedSelector: static _ => (Sum: 0.0, Count: 0),
                func: static (accumulator, bin) => (accumulator.Sum + bin.probability, accumulator.Count + 1));
        
        Console.WriteLine("\nAverage Probability by Bucket:");
        foreach (var (bucket, accumulator) in averageByBucket)
        {
            var average = accumulator.Count == 0 
                ? 0
                : accumulator.Sum / accumulator.Count;
            Console.WriteLine($"{bucket}: {average:P0}");
        }
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
        {
            Console.WriteLine($"{word}: {frequency}");
        }
    }
    
    /// <summary>
    /// Demonstrates the functionality of the Index LINQ method
    /// </summary>
    private static void IndexExample()
    {
        var words= DocumentHelper.SplitWords(Document);

        foreach (var (index, word) in words.Index())
        {
            Console.WriteLine($"{index + 1}: {word}");
        }
    }
}