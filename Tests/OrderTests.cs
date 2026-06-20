using Core.Entities;
using Core.Pricing;

namespace Tests;

public class OrderTests
{
    private static Fruit Apple() => new("Apple", 2.00m, new PerKgPricingStrategy());
    private static Fruit Banana() => new("Banana", 0.30m, new PerItemPricingStrategy());
    private static Fruit Cherry() => new("Cherry", 5.00m,
        new DiscountPricingStrategy(new PerKgPricingStrategy(), 2m, 0.10m));

    [Fact]
    public void CalculateTotal_SinglePerKgLine_ReturnsCorrectTotal()
    {
        var order = new Order().AddLine(Apple(), quantity: 2m);

        decimal total = order.CalculateTotal();

        Assert.Equal(4.00m, total);
    }

    [Fact]
    public void CalculateTotal_SinglePerItemLine_ReturnsCorrectTotal()
    {
        var order = new Order().AddLine(Banana(), quantity: 5m);

        decimal total = order.CalculateTotal();

        Assert.Equal(1.50m, total);
    }

    [Fact]
    public void CalculateTotal_DiscountLineAboveThreshold_AppliesDiscount()
    {
        var order = new Order().AddLine(Cherry(), quantity: 3m);

        decimal total = order.CalculateTotal();

        Assert.Equal(13.50m, total);
    }

    [Fact]
    public void CalculateTotal_DiscountLineBelowThreshold_NoDiscount()
    {
        var order = new Order().AddLine(Cherry(), quantity: 1m);

        decimal total = order.CalculateTotal();

        Assert.Equal(5.00m, total);
    }

    [Fact]
    public void CalculateTotal_MultipleLines_SumsAllLineCosts()
    {
        var order = new Order()
            .AddLine(Apple(), quantity: 1.5m)
            .AddLine(Banana(), quantity: 6m)
            .AddLine(Cherry(), quantity: 3m);

        decimal total = order.CalculateTotal();

        Assert.Equal(18.30m, total);
    }

    [Fact]
    public void CalculateTotal_EmptyOrder_ReturnsZero()
    {
        var order = new Order();

        decimal total = order.CalculateTotal();

        Assert.Equal(0m, total);
    }

    [Fact]
    public void AddLine_NullFruit_ThrowsArgumentNullException()
    {
        var order = new Order();

        Assert.Throws<ArgumentNullException>(() => order.AddLine(null!, 1m));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void AddLine_NonPositiveQuantity_ThrowsArgumentOutOfRangeException(decimal quantity)
    {
        var order = new Order();

        Assert.Throws<ArgumentOutOfRangeException>(() => order.AddLine(Apple(), quantity));
    }

    [Fact]
    public void IsEmpty_NewOrder_ReturnsTrue()
    {
        var order = new Order();

        Assert.True(order.IsEmpty);
    }

    [Fact]
    public void IsEmpty_OrderWithLines_ReturnsFalse()
    {
        var order = new Order().AddLine(Apple(), 1m);

        Assert.False(order.IsEmpty);
    }

    [Fact]
    public void Lines_ReflectsAllAddedLines()
    {
        var order = new Order()
            .AddLine(Apple(), 1m)
            .AddLine(Banana(), 3m);

        Assert.Equal(2, order.Lines.Count);
    }
}
