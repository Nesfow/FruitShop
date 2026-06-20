using Core.Entities;
using Core.Factories;

var order = new Order()
    .AddLine(FruitFactory.Create("apple"), 5)
    .AddLine(FruitFactory.Create("banana"), 11)
    .AddLine(FruitFactory.Create("cherry"), 1)
    .AddLine(FruitFactory.Create("mango"), 2);

Console.WriteLine("🍎 Fruit Shop Order");
Console.WriteLine("=================================================");

foreach (var item in order.Lines)
{
    Console.WriteLine($"{item.Fruit.Name} x{item.Quantity} (${item.Fruit.BasePrice} {item.Fruit.PricingStrategy.Description}): ${item.LineCost}");

}
Console.WriteLine("=================================================");
Console.WriteLine($"Total: ${order.CalculateTotal()}");