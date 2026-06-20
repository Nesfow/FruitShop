using Core.Pricing;

namespace Tests.Pricing;

public class PerKgPricingStrategyTests
{
    private readonly PerKgPricingStrategy _sut = new();

    [Fact]
    public void Calculate_ReturnsBasePriceMultipliedByQuantity()
    {
        decimal basePrice = 2.00m;
        decimal quantity = 3m;

        decimal result = _sut.Calculate(basePrice, quantity);

        Assert.Equal(6.00m, result);
    }

    [Fact]
    public void Calculate_WithFractionalKg_ReturnsCorrectCost()
    {
        decimal basePrice = 2.00m;
        decimal quantity = 1.5m;

        decimal result = _sut.Calculate(basePrice, quantity);

        Assert.Equal(3.00m, result);
    }

    [Fact]
    public void Calculate_WithZeroQuantity_ReturnsZero()
    {
        decimal basePrice = 2.00m;
        decimal quantity = 0m;

        decimal result = _sut.Calculate(basePrice, quantity);

        Assert.Equal(0m, result);
    }

    [Fact]
    public void Calculate_WithNegativeQuantity_ThrowsArgumentOutOfRangeException()
    {
        decimal basePrice = 2.00m;
        decimal quantity = -1m;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.Calculate(basePrice, quantity));
    }

    [Fact]
    public void Description_ReturnsPerKg()
    {
        string description = _sut.Description;

        Assert.Equal("Per kilogram", description);
    }
}
