
using Core.Pricing;

namespace Tests.Pricing;

public class DiscountPricingStrategyTests
{
    private readonly DiscountPricingStrategy _cherrySut = new(new PerKgPricingStrategy(), 2m, 0.10m);

    [Fact]
    public void Calculate_BelowThreshold_ReturnsFullPrice()
    {
        decimal basePrice = 5.00m;
        decimal quantity = 1m;

        decimal result = _cherrySut.Calculate(basePrice, quantity);

        Assert.Equal(5.00m, result);
    }

    [Fact]
    public void Calculate_ExactlyAtThreshold_ReturnsFullPrice()
    {
        decimal basePrice = 5.00m;
        decimal quantity = 2m;

        decimal result = _cherrySut.Calculate(basePrice, quantity);

        Assert.Equal(10.00m, result);
    }

    [Fact]
    public void Calculate_AboveThreshold_AppliesDiscount()
    {
        decimal basePrice = 5.00m;
        decimal quantity = 3m;

        decimal result = _cherrySut.Calculate(basePrice, quantity);

        Assert.Equal(13.50m, result);
    }

    [Fact]
    public void Calculate_WrapsPerItemStrategy_AppliesDiscountCorrectly()
    {
        var sut = new DiscountPricingStrategy(new PerItemPricingStrategy(), 10m, 0.20m);

        decimal basePrice = 1.00m;
        decimal quantity = 12m;

        decimal result = sut.Calculate(basePrice, quantity);

        Assert.Equal(9.60m, result);
    }

    [Fact]
    public void Description_IncludesDiscountAndThresholdDetails()
    {
        string description = _cherrySut.Description;

        Assert.Contains("10%", description);
        Assert.Contains("2", description);
    }
}