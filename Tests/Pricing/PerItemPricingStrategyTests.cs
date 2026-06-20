using Core.Pricing;

namespace Tests.Pricing;

public class PerItemPricingStrategyTests
{
    private readonly PerItemPricingStrategy _sut = new();

    [Fact]
    public void Calculate_ReturnsBasePriceMultipliedByItemCount()
    {
        decimal basePrice = 0.30m;
        decimal quantity = 6m;

        decimal result = _sut.Calculate(basePrice, quantity);

        Assert.Equal(1.80m, result);
    }

    [Fact]
    public void Calculate_WithSingleItem_ReturnsBasePrice()
    {
        decimal basePrice = 1.50m;
        decimal quantity = 1m;

        decimal result = _sut.Calculate(basePrice, quantity);

        Assert.Equal(1.50m, result);
    }

    [Fact]
    public void Calculate_WithZeroItems_ReturnsZero()
    {
        decimal basePrice = 0.30m;
        decimal quantity = 0m;

        decimal result = _sut.Calculate(basePrice, quantity);

        Assert.Equal(0m, result);
    }

    [Fact]
    public void Calculate_WithNegativeQuantity_ThrowsArgumentOutOfRangeException()
    {
        decimal basePrice = 0.30m;
        decimal quantity = -3m;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.Calculate(basePrice, quantity));
    }

    [Fact]
    public void Calculate_WithFractionalQuantity_ThrowsArgumentException()
    {
        decimal basePrice = 0.30m;
        decimal quantity = 1.5m;

        Assert.Throws<ArgumentException>(() =>
            _sut.Calculate(basePrice, quantity));
    }

    [Fact]
    public void Description_ReturnsPerItem()
    {
        string description = _sut.Description;

        Assert.Equal("Per item", description);
    }
}
