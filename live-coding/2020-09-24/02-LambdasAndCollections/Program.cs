using System;
using System.Collections.Generic;

var heroes = new List<Hero>
{
    new("Wade", "Wilson", "Deadpool", false),
    new(string.Empty, string.Empty, "Homelander", true),
    new("Bruce", "Wayne", "Batman", false),
    new(string.Empty, string.Empty, "Stormfront", true)
};

var result = Filter(heroes, h => string.IsNullOrEmpty(h.LastName));
var heroesWhoCanFly = string.Join(", ", result);
Console.WriteLine(heroesWhoCanFly);

Filter(new[] { "Homelander", "The Deep", "Stormfront" }, hn => hn.StartsWith("H"));
Filter(new[] { 1, 2, 3, 4, 5 }, n => n % 2 == 0);

// Linq: Where
IEnumerable<T> Filter<T>(IEnumerable<T> items, Func<T, bool> f)
{
    foreach (var item in items)
    {
        if (f(item))
        {
            yield return item;
        }
    }
}

// delegate bool Filter<T>(T h);

record Hero(string FirstName, string LastName, string HeroName, bool CanFly);