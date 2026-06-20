# Fruit Shop 

A small .NET 10 console application that calculates the price of fruit orders, demonstrating the **Strategy**, **Factory** and  **Decorator** design patterns with clean separation of concerns and thorough unit tests.

---

## Getting started

```bash
# Run the console app
dotnet run --project Console

# Run all tests
dotnet test

# Run tests with output
dotnet test --logger "console;verbosity=normal"
```

**Expected output:**
```

🍎 Fruit Shop Order
=================================================
Apple x5 ($2 Per kilogram): $10
Banana x11 ($0.3 Per item): $3.3
Cherry x3 ($5 Per kilogram with 10% off over 2): $13.5
Mango x2 ($3.99 Per item): $7.98
=================================================
Total: $34.78

```

---

## Design decisions

### Separating `Core` from `Console`

The business logic (pricing, orders, fruits) lives in `Core` with no dependency on `Console`. This means:

- **All business logic is fully testable** — tests reference Core directly, no console output to suppress.
- **The same Core could be used by a REST API or a web UI** without changing a line of business logic.

### `record` for `Fruit` and `OrderLine`

Both are value objects — two fruits with the same name, price, and strategy are the same fruit. Records give value-based equality, immutability, and a concise syntax.

## Design patterns used

### 1. Strategy pattern — `IPricingStrategy`

Strategy is the logical fit — it defines a family of algorithms (pricing rules), encapsulates each one, and makes them interchangeable without touching the classes that use them.

**How it works:**

```
IPricingStrategy
    ├── PerKgPricingStrategy    → price × weight
    ├── PerItemPricingStrategy  → price × count (whole numbers only)
    └── DiscountPricingStrategy → wraps any strategy, applies % off above a threshold
```

Each `Fruit` holds a reference to its strategy. When the order asks a fruit for its cost, the fruit delegates to its strategy — `Order` and `OrderLine` are completely unaware of how any fruit is priced.

**The benefit:** Adding a new pricing model (e.g. `BuyOneGetOneFreeStrategy`) requires:
1. Create a class implementing `IPricingStrategy`
2. Register it in `FruitFactory` for the relevant fruit
3. Zero changes to `Fruit`, `Order`, `OrderLine`, or `Program.cs`

This is the **Open/Closed Principle** in practice.

### 2. Factory pattern — `FruitFactory`

Without a factory, every place that creates a fruit needs to know its base price, pricing strategy, and constructor arguments. The factory centralises it.

**How it works:**

```csharp
Fruit cherry = FruitFactory.Create("cherry");
```

**The benefit:** Changing cherry's discount from 10% to 15% means editing one line in one file. No search-and-replace across the codebase.

### 3. Decorator pattern — `DiscountPricingStrategy`

Rather than creating a separate `DiscountedPerKgStrategy` and `DiscountedPerItemStrategy`, the `DiscountPricingStrategy` wraps any `IPricingStrategy` and layers discount logic on top.

```csharp
// 10% off cherries over 2kg
new DiscountPricingStrategy(new PerKgPricingStrategy(), thresholdQuantity: 2m, discountPercentage: 0.10m)

// 20% off mangoes when buying 10+
new DiscountPricingStrategy(new PerItemPricingStrategy(), thresholdQuantity: 10m, discountPercentage: 0.20m)

// Tiered discounts: stack decorators
new DiscountPricingStrategy(
    new DiscountPricingStrategy(new PerKgPricingStrategy(), 5m, 0.05m), 10m, 0.10m)
```

---

## How to extend the system

### Add a new fruit

Add new to the switch expression in `FruitFactory.Create()`:

```csharp
"grape" => new Fruit(
    Name: "Grape",
    BasePrice: 3.50m,
    PricingStrategy: new PerKgPricingStrategy()),
```


### Add a new pricing model

1. Create a class implementing `IPricingStrategy`:

```csharp
public sealed class BulkPricingStrategy(int bulkSize, decimal bulkPrice) : IPricingStrategy
{
    public string Description => $"${bulkPrice} per {bulkSize}";

    public decimal Calculate(decimal basePrice, decimal quantity)
    {
        var bulks = Math.Floor(quantity / bulkSize);
        var remainder = quantity % bulkSize;
        return bulks * bulkPrice + remainder * basePrice;
    }
}
```

2. Use it in the factory.

### Add a new discount type

Either:
- **Wrap** `DiscountPricingStrategy` around any existing strategy (covered by Decorator)
- **Create a new strategy** implementing `IPricingStrategy` for the new logic

## Testing approach

Tests are in `Tests` using **xUnit** . 

Key scenarios covered:

| Test class | What it covers |
|---|---|
| `PerKgPricingStrategyTests` | Normal calc, fractional kg, zero qty, negative qty |
| `PerItemPricingStrategyTests` | Normal calc, single item, fractional qty rejection, negative qty |
| `DiscountPricingStrategyTests` | Below/at/above threshold, wraps per-item, invalid constructor args |
| `FruitFactoryTests` | Several fruit types, case insensitivity, unknown fruit exception |
| `OrderTests` | Per-kg/per-item/discount lines, multi-line sum, empty order, guard clauses |
| `FruitTests` | Strategy delegation, discount application, record equality |

---