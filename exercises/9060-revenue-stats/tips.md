# Tips

## Introduction

This document summarizes frequent mistakes I saw when looking through the exams.

## Coding Style

**Never** use `goto`.

Prefer `var` or target-typed `new` whenever data type is obvious. Example:

```cs
// Avoid:
int indexer = 1;
List<Order> orderDetail = new List<Order>();
class Demo
{
    public List<Product> Products { get; } = new List<Product>();
}

// Prefer:
var indexer = 1;
var orderDetail = new List<Order>();
// or:
List<Order> orderDetail = new();
class Demo
{
    public List<Product> Products { get; } = new();
}
```

Use curly braces for single-line code blocks. As an alternative, write code in the same line as the `if` statement. Even large companies like [Apple paid a high price](https://embeddedgurus.com/barr-code/2014/03/apples-gotofail-ssl-security-bug-was-easily-preventable/) for not following this rule. Example:

```cs
// Avoid:
if (currentOrder != null)
    orders.Add(currentOrder);

// Prefer:
if (currentOrder != null)
{
    orders.Add(currentOrder);
}
// or:
if (currentOrder != null) orders.Add(currentOrder);
```

In C#, methods start with uppercase letters, not lowercase ones (e.g. `GetGroupedByCustomer` instead of `getGroupedByCustomer`).

In C#, local variables start with lowercase letters, not uppercase ones (e.g. `var list = ...` instead of `var List = ...`).

Avoid manual implemented properties. Use auto-implemented properties instead:

```cs
// Avoid:
private int _orderID;
public int OrderID
{
    get { return _orderID; }
    set { _orderID = value; }
}

// Prefer:
public int OrderID { get; set; }
```

**Do not** use get/set methods instead of properties:

```cs
// Avoid:
public int Number;
public int getNumber()
{
    return Number;
}
public void setNumber(int num)
{
    Number = num;
}

// Prefer:
public int Number { get; set; }
```

Use object initialization:

```cs
// Avoid:
order = new Order();
order.OrderID = Convert.ToInt32(item[1]);
order.Customer = item[2];

// Prefer:
order = new Order
{
    OrderID = Convert.ToInt32(item[1]),
    Customer = item[2]
};
```

Use self-describing and meaningful variable names. Avoid names like that (`array`, `dict`):

```cs
var array = new int[splittedLines.Count()];
var dict = new Dictionary<int, int>();
```

## Dictionaries

You can update values in a dictionary without replacing them:

```cs
// Avoid:
sum += results.GetValueOrDefault(lastCustomer);
results.Remove(lastCustomer);
results.Add(lastCustomer, sum);

// Prefer:
results[lastCustomer] += sum;
```

## Async Programming

**Always** prefer async IO methods over sync ones (e.g. `ReadAllLinesAsync` instead of `ReadAllLines`)

## Records

Use `records` if the make your code simpler. Example:

```cs
// Avoid
class OrderEntry
{
    public int ID { get; }
    public string Customer { get; }
    public string Country { get; }
    public List<Product> Products { get; } = new List<Product>();

    public OrderEntry(int id, string customer, string country)
    {
        ID = id;
        Customer = customer;
        Country = country;
    }
}

// Prefer:
record OrderEntry(int ID, string Customer, string Country)
{
    public List<Product> Products { get; } = new();
}
```

Use auto-generated properties for records. Example:

```cs
// Avoid:
record Detail {
    public string Product { get; set; }
    public int Amount { get; set; }
    ...
    public Detail(string name, int amount, ...)
    {
        Product = name;
        Amount = amount;
        ...
    }
}

// Prefer:
record Detail(string Name, int Amount, ...);
```

## LINQ

You can pass a lambda expression to aggregation methods like `Sum`.

```cs
// Avoid:
var result = Details.Select(d => d.TotalPrice).Sum();

// Prefer:
var result = Details.Sum(d => d.TotalPrice);
```

Prefer LINQ's `OrderBy` function instead of `List<T>.Sort`.

Whenever you have a list or an array, prefer `Length` instead of LINQ's `Count()` method. In that case, `Length` performs better.

Think about the best order of LINQ operators in a query. The following query has the `Skip` operator at the end. If it were at the beginning, splitting of the first line would not be done, which would lead to better performance.

```cs
// Avoid:
var lines = (await File.ReadAllLinesAsync("order-data.txt"))
    .Select(line => line.Split('\t'))
    .ToList()
    .Skip(2);

// Prefer:
var lines = (await File.ReadAllLinesAsync("order-data.txt"))
    .Skip(2)
    .Select(line => line.Split('\t'))
    .ToList();
```

Avoid converting to `List<T>` just to use `ForEach`. The conversion only wastes performance:

```cs
// Avoid:
orders.OrderByDescending(o => o.Price).ToList()
    .ForEach(o => Console.WriteLine($"{o.Id} \t {o.Price}"));

// Prefer:
foreach(var o in orders.OrderByDescending(o => o.Price)) Console.WriteLine($"{o.Id} \t {o.Price}");
```

Avoid unnecessary `ToList` operations:

```cs
foreach (var (customer, revenue) in
    orders.GroupBy(n => n.Customer)
    .ToList() // <<<<< NOT NECESSARY, AVOID
    .Select(n => (n.Key, n.Sum(p => p.details.Sum(p => p.TotalPrice))))
    .OrderByDescending(o => o.Item2))
{ ... }
```
