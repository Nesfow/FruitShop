using Core.Interfaces;

namespace Core.Pricing;

public class DiscountPricingStrategy : IPricingStrategy
{
    private readonly IPricingStrategy _innerStrategy;
    private readonly decimal _quantityThreshold;
    private readonly decimal _discountPercentage; // in decimal value: e.g. 0.1 = 10%

    public DiscountPricingStrategy(IPricingStrategy innerStrategy, decimal quantityThreshold, decimal discountPercentage)
    {
        _quantityThreshold = quantityThreshold;
        _discountPercentage = discountPercentage;
        _innerStrategy = innerStrategy;
    }

    public string Description => $"{_innerStrategy.Description} with {_discountPercentage:P0} off over {_quantityThreshold}";

    public decimal Calculate(decimal basePrice, decimal quantity)
    {
        var initialTotal = _innerStrategy.Calculate(basePrice, quantity);

        return quantity > _quantityThreshold
            ? initialTotal - (initialTotal * _discountPercentage)
            : initialTotal;
    }
}