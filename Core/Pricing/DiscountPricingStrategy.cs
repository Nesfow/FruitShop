using Core.Interfaces;

namespace Core.Pricing;

public class DiscountPricingStrategy : IPricingStrategy
{
    private readonly IPricingStrategy _innerStrategy;
    private readonly decimal _quantityThreshold;
    private readonly decimal _discountPercentage;

    public DiscountPricingStrategy(IPricingStrategy innerStrategy, decimal quantityThreshold, decimal discountPercentage)
    {
        _quantityThreshold = quantityThreshold;
        _discountPercentage = discountPercentage;
        _innerStrategy = innerStrategy;
    }

    public string Description => throw new NotImplementedException();

    public decimal Calculate(decimal basePrice, decimal quantity)
    {
        var initialTotal = _innerStrategy.Calculate(basePrice, quantity);

        return quantity > _quantityThreshold
            ? initialTotal - (initialTotal * _discountPercentage)
            : initialTotal;
    }
}