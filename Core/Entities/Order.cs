namespace Core.Entities;

/// <summary>
/// Represents a customer's order. Contains one or more OrderLines and
/// knows how to calculate the total cost across all of them.
/// </summary>
public sealed class Order
{
    private readonly List<OrderLine> _lines = [];

    /// <summary>Read-only view of the order lines.</summary>
    public IReadOnlyList<OrderLine> Lines => _lines.AsReadOnly();

    /// <summary>
    /// Adds a fruit and quantity to the order.
    /// </summary>
    /// <param name="fruit">The fruit to add.</param>
    /// <param name="quantity">How much (kg or item count).</param>
    public Order AddLine(Fruit fruit, decimal quantity)
    {
        ArgumentNullException.ThrowIfNull(fruit);

        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity),
                "Quantity must be greater than zero.");

        _lines.Add(new OrderLine(fruit, quantity));
        return this; // fluent interface for readable setup
    }

    /// <summary>
    /// Calculates the total cost of all lines in the order.
    /// Each line delegates to its fruit's pricing strategy.
    /// </summary>
    public decimal CalculateTotal() => _lines.Sum(line => line.LineCost);

    /// <summary>Whether the order has any items in it.</summary>
    public bool IsEmpty => _lines.Count == 0;
}
