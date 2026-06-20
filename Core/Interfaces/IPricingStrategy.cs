namespace Core.Interfaces;

/// <summary>
/// Defines the contract for all pricing algorithms.
/// </summary>
public interface IPricingStrategy
{
    /// <summary>
    /// Calculates the cost for a given base price and quantity.
    /// </summary>
    /// <param name="basePrice">The unit price of the fruit (per kg or per item).</param>
    /// <param name="quantity">The amount ordered (kg or item count).</param>
    /// <returns>The total cost for this line.</returns>
    decimal Calculate(decimal basePrice, decimal quantity);

    string Description { get; }
}
