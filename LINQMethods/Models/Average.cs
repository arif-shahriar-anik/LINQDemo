namespace LINQMethods.Models;

/// <summary>
/// Represents the calculation of an average value.
/// </summary>
public record struct AverageAccumulator(double Sum, int Count)
{
    public AverageAccumulator Add(double value) => 
        new(Sum + value, Count + 1);
    
    public double Value => 
        Count == 0 ? 0: Sum / Count;
}