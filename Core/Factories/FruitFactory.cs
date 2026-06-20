using Core.Entities;
using Core.Pricing;

namespace Core.Factories;

public class FruitFactory
{
    public static Fruit Create(string fruitName)
    {
        return fruitName.Trim().ToLowerInvariant() switch
        {
            "apple" => new Fruit("apple", 1m, new PerItemPricingStrategy()),
            "banana" => new Fruit("banana", 1.5m, new DiscountPricingStrategy(new PerItemPricingStrategy(), 10, 0.1m)),
            "cherry" => new Fruit("cherry", 5m, new PerKgPricingStrategy()),
            "mango" => new Fruit("mango", 3.99m, new PerItemPricingStrategy()),
            "watermelon" => new Fruit("watermelon", 6.5m, new PerItemPricingStrategy()),

            _ => throw new ArgumentException($"'{fruitName}' is not an available item to order.\n Fruits available: 'apple', 'banana', 'cherry', 'mango', 'watermelon'")
        };
    }
}