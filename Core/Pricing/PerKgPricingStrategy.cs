using Core.Interfaces;

namespace Core.Pricing;

public sealed class PerKgPricingStrategy : IPricingStrategy
{
    public string Description => "Per kilogram";

    public decimal Calculate(decimal basePrice, decimal quantity)
    {
        // For input validation
        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be a positive number");
        }

        return basePrice * quantity;
    }
}