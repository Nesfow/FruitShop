using Core.Interfaces;

namespace Core.Entities;

/// <summary>
/// Decided on a record type for immutability and value-based equality.
/// </summary>
public sealed record Fruit(
    string Name,
    decimal BasePrice,
    IPricingStrategy PricingStrategy)
{
    /// <summary>
    /// Calculates the cost for a given quantity of this fruit.
    /// Delegates entirely to the fruit's own pricing strategy.
    /// </summary>
    public decimal CalculateCost(decimal quantity) =>
        PricingStrategy.Calculate(BasePrice, quantity);
}
