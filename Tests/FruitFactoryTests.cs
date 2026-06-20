using Core.Entities;
using Core.Factories;

namespace Tests;

public class FruitFactoryTests
{
    [Fact]
    public void Create_Apple_ReturnsFruitWithCorrectNameAndPrice()
    {
        // Arrange & Act
        Fruit apple = FruitFactory.Create("apple");

        // Assert
        Assert.Equal("Apple", apple.Name);
        Assert.Equal(1.0m, apple.BasePrice);
    }
}