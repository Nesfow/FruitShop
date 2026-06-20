using Core.Interfaces;

namespace Core.Pricing;

public sealed class PerItemPricingStrategy : IPricingStrategy
{
    public string Description => "Per item";

    public decimal Calculate(decimal basePrice, decimal quantity)
    {
        // For input validation
        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be a positive number");
        }

        if (quantity != Math.Floor(quantity))
            throw new ArgumentException("Item quantity must be a whole number.", nameof(quantity));

        return basePrice * quantity;
    }
}