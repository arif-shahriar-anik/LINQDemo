using BenchmarkDotNet.Attributes;

namespace LINQMethods.Benchmark;

[SimpleJob(warmupCount: 1, iterationCount: 3)]
[MemoryDiagnoser]
public class CountByBenchmark
{
    private const int DocumentRepetitionCount = 100;
    
    /// <summary>
    /// Holds a large document for benchmarking term frequency calculations.
    /// </summary>
    private static readonly string LargeDocument = string.Join(" ",
        Enumerable.Repeat(
            "In .NET., LINQ makes data queries readable, and CountBy makes counting terms easy. " +
            "CountBy is great for frequency counters, keyword extraction, and simple search ranking. " +
            "In many apps, counting terms is the first step toward relevance scoring.", DocumentRepetitionCount));

    [Benchmark]
    public List<KeyValuePair<string,int>> CountByExample()
    {
        return CountByMethod.CalculateTermFrequency(LargeDocument).ToList();
    }

    [Benchmark]
    public List<KeyValuePair<string,int>> GroupByExample()
    {
        return CountByMethod.CalculateTermFrequencyOld(LargeDocument).ToList();
    }
}