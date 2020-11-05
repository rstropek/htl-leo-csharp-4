using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Check: Use of async method
IEnumerable<string> lines = await File.ReadAllLinesAsync("order-data.txt");

// Check: Ignore header lines using LINQ
lines = lines.Skip(2);

// Check: Split lines using `Split` and LINQ
var splittedLines = lines.Select(l => l.Split('\t'));

// Check: Algorithm for distinguishing orders and details
var orders = new List<Order>();
Order? order = null; // Note: Nullable reference type is optional
foreach(var line in splittedLines)
{
    switch (line[0])
    {
        case "ORDER":
            order = new(int.Parse(line[1]), line[2], line[3]);
            orders.Add(order);
            break;
        case "DETAIL" when order != null:
            order.Details.Add(new(line[1], int.Parse(line[2]), int.Parse(line[3]), int.Parse(line[4])));
            break;
        default:
            // Optional error handling
            throw new InvalidOperationException("Invalid state, should never happen!");
    }
}

// Check: Handling of command-line arguments
switch (args[0])
{
    case "orderId":
        PrintOrderIDStats(orders);
        break;
    case "customer":
        PrintCustomerStats(orders);
        break;
}

static void PrintOrderIDStats(IEnumerable<Order> orders)
{
    // Check: Correct LINQ query
    // Note: Not using LINQ here is ok for a positive grade.
    var simpleOrderStats = orders
        .Select(o => new { o.OrderID, TotalPrice = o.Details.Sum(d => d.Price) })
        .OrderByDescending(o => o.TotalPrice);
    foreach (var item in simpleOrderStats)
    {
        Console.WriteLine($"{item.OrderID}: {item.TotalPrice}");
    }
}

static void PrintCustomerStats(IEnumerable<Order> orders)
{
    // Check: Correct LINQ query
    var result = orders
        .Select(o => new { o.Customer, TotalPrice = o.Details.Sum(d => d.Price) })
        .GroupBy(o => o.Customer)
        .Select(o => new { Customer = o.Key, TotalPrice = o.Sum(d => d.TotalPrice) })
        .OrderByDescending(o => o.TotalPrice);
    foreach (var item in result)
    {
        Console.WriteLine($"{item.Customer}: {item.TotalPrice} ({((float)item.TotalPrice) / result.Sum(r => r.TotalPrice):P})");
    }
}

// Note: Use of record or class is fine here
record Order(int OrderID, string Customer, string DeliveryCountry)
{
    public List<OrderDetail> Details { get; } = new();
}

record OrderDetail(string Product, int Amount, int UnitPrice, int Price);
