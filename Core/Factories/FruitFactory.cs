using Core.Entities;
using Core.Pricing;

namespace Core.Factories;

public class FruitFactory
{
    public static Fruit Create(string fruitName) => fruitName.Trim().ToLowerInvariant() switch
    {
        "apple" => new Fruit("Apple", 1m, new PerKgPricingStrategy()),
        "banana" => new Fruit("Banana", 1.5m, new DiscountPricingStrategy(new PerItemPricingStrategy(), 10, 0.1m)),
        "cherry" => new Fruit("Cherry", 5m, new PerKgPricingStrategy()),
        "mango" => new Fruit("Mango", 3.99m, new PerItemPricingStrategy()),
        "watermelon" => new Fruit("Watermelon", 6.5m, new PerItemPricingStrategy()),

        _ => throw new ArgumentException($"'{fruitName}' is not an available item to order.\n Fruits available: 'apple', 'banana', 'cherry', 'mango', 'watermelon'")
    };
}