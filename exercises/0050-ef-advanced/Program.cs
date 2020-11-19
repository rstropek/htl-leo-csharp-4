using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Console;

var factory = new CookbookContextFactory();
using var context = factory.CreateDbContext();

// An experiment...
var newDish = new Dish { Title = "Foo", Notes = "Bar" };
context.Dishes.Add(newDish);
await context.SaveChangesAsync();
newDish.Notes = "Baz";
await context.SaveChangesAsync();
// Question: How does EFCore know that only Notes column has to be updated??
// Answer: The change tracker. Let's learn more about that:
await EntityStates(factory);
var newDishId = await ChangeTracking(factory);
await AttachEntities(factory, newDishId);
await NoTracking(factory);

// Sometimes you have to go raw...
await RawSql(factory);

// EFCore also knows transactions...
await Transactions(factory);

// How queries really work...
await ExpressionTrees(factory);

static async Task EntityStates(CookbookContextFactory factory)
{
    using var dbContext = factory.CreateDbContext();

    var newDish = new Dish { Title = "Foo", Notes = "Bar" };
    var state = dbContext.Entry(newDish).State; // DB does not know anything about newDish -> Detached

    dbContext.Dishes.Add(newDish);
    state = dbContext.Entry(newDish).State; // Record has been added in memory, not yet in DB -> Added

    await dbContext.SaveChangesAsync();
    state = dbContext.Entry(newDish).State; // Record has been written to DB, no changes in memory -> Unchanged

    newDish.Notes = "Baz";
    state = dbContext.Entry(newDish).State; // Record has been modified in memory -> Modified

    await dbContext.SaveChangesAsync();
    state = dbContext.Entry(newDish).State; // -> Unchanged

    dbContext.Dishes.Remove(newDish);
    state = dbContext.Entry(newDish).State; // Record has been marked for deletion -> Deleted

    await dbContext.SaveChangesAsync();
    state = dbContext.Entry(newDish).State; // Record is no longer connected to db -> Detached again
}

static async Task<int> ChangeTracking(CookbookContextFactory factory)
{
    using var dbContext = factory.CreateDbContext();

    // Let's add a new dish to the DB
    var newDish = new Dish { Title = "Foo", Notes = "Bar" };
    dbContext.Dishes.Add(newDish);
    await dbContext.SaveChangesAsync();

    // Change the dish
    newDish.Notes = "Baz";

    // Note that EF remembers all objects read from the DB including their
    // original, unchanged values.
    var entry = dbContext.Entry(newDish);
    var originalNotes = entry.OriginalValues["Notes"].ToString();

    // If we re-read the object, EF recognizes that the object has been
    // changed in-memory and gives us the changed object.
    var dishReadAgain = await dbContext.Dishes.SingleAsync(d => d.Id == newDish.Id);
    var state = dbContext.Entry(newDish).State; // -> Modified

    // Let's create another context and re-read the dish from there
    using var dbContext2 = factory.CreateDbContext();
    dishReadAgain = await dbContext2.Dishes.SingleAsync(d => d.Id == newDish.Id);
    var note = dishReadAgain.Notes; // -> Bar, because 2nd context does not know about modification

    // Ask 2nd context to "forget" dish read from DB
    dbContext2.Entry(dishReadAgain).State = EntityState.Detached;

    // Attach modified dish instead
    dbContext2.Attach(newDish);
    state = dbContext2.Entry(newDish).State; // -> Modified
    dishReadAgain = await dbContext2.Dishes.SingleAsync(d => d.Id == newDish.Id);
    note = dishReadAgain.Notes; // -> Baz, because now 2nd context knows about modification

    return newDish.Id;
}

static async Task AttachEntities(CookbookContextFactory factory, int newDishId)
{
    using var dbContext = factory.CreateDbContext();

    // Create a dish in memory. In the real world, this might be data received by a web api.
    // Note that this dish is currently untracked. EF does not know about it.
    var changedDish = new Dish { Id = newDishId, Title = "Oof", Notes = "Rab" };

    // We can update the dish without having to attach it explicitly.
    // EFCore will generate a SQL Update statement updating all columns because
    // it does not know which properites have changed.
    dbContext.Update(changedDish);
    await dbContext.SaveChangesAsync();
}

static async Task NoTracking(CookbookContextFactory factory)
{
    using var dbContext = factory.CreateDbContext();

    // Read dishes without change tracking. This is the preferred way of
    // reading data if you do not intend to update the returned object.
    // EFCore does not have to track results in this case.
    var dishes = await dbContext.Dishes.AsNoTracking().ToArrayAsync();
}

static async Task RawSql(CookbookContextFactory factory)
{
    using var dbContext = factory.CreateDbContext();

    // Read data with raw SQL. Note that change tracking works just like before
    var dishes = await dbContext.Dishes
        .FromSqlRaw("SELECT * FROM Dishes")
        .ToArrayAsync();

    // Read data with parameters. Make sure to check the generated SQL query.
    var filter = "%z";
    dishes = await dbContext.Dishes
        .FromSqlInterpolated($"SELECT * FROM Dishes WHERE Notes LIKE {filter}")
        .AsNoTracking()
        .ToArrayAsync();

    // This is BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD,
    // BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD, BAD
    dishes = await dbContext.Dishes
        .FromSqlRaw("SELECT * FROM Dishes WHERE Notes LIKE '" + filter + "'")
        .AsNoTracking()
        .ToArrayAsync();

    // Write data
    await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM Dishes WHERE Id NOT IN (SELECT DishId FROM Ingredients)");
}

static async Task Transactions(CookbookContextFactory factory)
{
    using var dbContext = factory.CreateDbContext();

    // Let's begin a transaction. All DB change from here until Commit
    // are either completely written or not written at all (rollback).
    using var transaction = await dbContext.Database.BeginTransactionAsync();
    try
    {
        dbContext.Dishes.Add(new Dish { Title = "Foo", Notes = "Bar" });
        await dbContext.SaveChangesAsync();

        // Let's generate an error in the DB (division by 0)
        await dbContext.Database.ExecuteSqlRawAsync("SELECT 1/0 AS Meaningless");
        await transaction.CommitAsync();
    }
    catch (SqlException ex)
    {
        Error.WriteLine($"Something bad happened: {ex.Message}");
    }
}

static async Task ExpressionTrees(CookbookContextFactory factory)
{
    var dbContext = factory.CreateDbContext();

    // Let's add some demo data
    dbContext.Dishes.Add(new Dish { Title = "Foo", Notes = "Barbarbarbarbar" });
    await dbContext.SaveChangesAsync();

    // How can EFCore translate the following query into SQL???
    var dishes = await dbContext.Dishes
        .Where(d => d.Title.StartsWith("F"))
        .Select(d => new { Description = $"{d.Title} ({d.Notes!.Substring(0, 10)}...)" })
        .ToArrayAsync();

    // The secret: Expression trees
    Func<Dish, bool> f = d => d.Title.StartsWith("F");
    Expression<Func<Dish, bool>> ex = d => d.Title.StartsWith("F");

    // Dynamic query with Expression trees
    var d = Expression.Parameter(typeof(Dish), "d");
    var sw = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) });
    ex = Expression.Lambda<Func<Dish, bool>>(
        Expression.Call(
            Expression.Property(d, nameof(Dish.Title)),
            sw!,
            Expression.Constant("F")), 
        d);

    dishes = await dbContext.Dishes
        .Where(ex)
        .Select(d => new { Description = $"{d.Title} ({d.Notes!.Substring(0, 10)}...)" })
        .ToArrayAsync();
}

class Dish
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Notes { get; set; }

    public int? Stars { get; set; }

    public List<DishIngredient> Ingredients { get; set; } = new();
}

class DishIngredient
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(50)]
    public string UnitOfMeasure { get; set; } = string.Empty;

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Amount { get; set; }

    public Dish? Dish { get; set; }

    public int? DishId { get; set; }
}

class CookbookContext : DbContext
{
    public DbSet<Dish> Dishes { get; set; }

    public DbSet<DishIngredient> Ingredients { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public CookbookContext(DbContextOptions<CookbookContext> options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

class CookbookContextFactory : IDesignTimeDbContextFactory<CookbookContext>
{
    public CookbookContext CreateDbContext(string[]? args = null)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<CookbookContext>();
        optionsBuilder
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

        return new CookbookContext(optionsBuilder.Options);
    }
}
