namespace Core.Entities;

/// <summary>
/// Represents a single line in an order: a fruit and how much of it was requested.
/// Quantity is in kg for per-kg fruits, or item count for per-item fruits.
/// </summary>
public sealed record OrderLine(Fruit Fruit, decimal Quantity)
{
    /// <summary>The cost for this line, calculated by the fruit's own pricing strategy.</summary>
    public decimal LineCost => Fruit.CalculateCost(Quantity);
}
