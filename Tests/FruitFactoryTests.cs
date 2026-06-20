using Core.Entities;
using Core.Factories;
using Core.Pricing;

namespace Tests;

public class FruitFactoryTests
{
    [Fact]
    public void Create_Apple_ReturnsFruitWithCorrectNameAndPrice()
    {
        Fruit apple = FruitFactory.Create("apple");

        Assert.Equal("Apple", apple.Name);
        Assert.Equal(1.0m, apple.BasePrice);
    }

    [Fact]
    public void Create_Apple_UsesPerKgPricingStrategy()
    {
        Fruit apple = FruitFactory.Create("apple");

        Assert.IsType<PerKgPricingStrategy>(apple.PricingStrategy);
    }

    [Fact]
    public void Create_Banana_ReturnsFruitWithCorrectNameAndPrice()
    {
        Fruit banana = FruitFactory.Create("banana");

        Assert.Equal("Banana", banana.Name);
        Assert.Equal(1.5m, banana.BasePrice);
    }

    [Fact]
    public void Create_Banana_UsesDiscountPricingStrategy()
    {
        Fruit banana = FruitFactory.Create("banana");

        Assert.IsType<DiscountPricingStrategy>(banana.PricingStrategy);
    }

    [Fact]
    public void Create_Cherry_ReturnsFruitWithCorrectNameAndPrice()
    {
        Fruit cherry = FruitFactory.Create("cherry");

        Assert.Equal("Cherry", cherry.Name);
        Assert.Equal(5.00m, cherry.BasePrice);
    }

    [Fact]
    public void Create_Cherry_UsesPerKgPricingStrategy()
    {
        Fruit cherry = FruitFactory.Create("cherry");

        Assert.IsType<PerKgPricingStrategy>(cherry.PricingStrategy);
    }

    [Theory]
    [InlineData("APPLE")]
    [InlineData("Apple")]
    [InlineData("  apple  ")]
    public void Create_IsCaseAndWhitespaceInsensitive(string input)
    {
        Fruit fruit = FruitFactory.Create(input);

        Assert.Equal("Apple", fruit.Name);
    }

    [Fact]
    public void Create_UnknownFruit_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => FruitFactory.Create("durian"));
        Assert.Contains("durian", ex.Message);
    }
}