using Core.Entities;
using Core.Pricing;

namespace Tests;

public class FruitTests
{
    [Fact]
    public void CalculateCost_DelegatesToPricingStrategy()
    {
        var fruit = new Fruit("Apple", 2.00m, new PerKgPricingStrategy());

        decimal cost = fruit.CalculateCost(3m);

        Assert.Equal(6.00m, cost);
    }

    [Fact]
    public void CalculateCost_WithDiscountStrategy_AppliesDiscountWhenAboveThreshold()
    {
        var fruit = new Fruit("Cherry", 5.00m,
            new DiscountPricingStrategy(new PerKgPricingStrategy(), 2m, 0.10m));

        decimal cost = fruit.CalculateCost(3m);

        Assert.Equal(13.50m, cost);
    }

    [Fact]
    public void Fruit_RecordEquality_TwoFruitsWithSameDataAreEqual()
    {
        var strategy = new PerKgPricingStrategy();
        var a = new Fruit("Apple", 2.00m, strategy);
        var b = new Fruit("Apple", 2.00m, strategy);

        Assert.Equal(a, b);
    }
}