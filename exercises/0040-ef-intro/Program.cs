using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using static System.Console;

var factory = new CookbookContextFactory();
using var context = factory.CreateDbContext();

// Here is a simple query that checks if a certain dish exists.
var porridge = await context.Dishes.FirstOrDefaultAsync(d => d.Title == "Breakfast Porridge");
if (porridge == null)
{
    WriteLine("Breakfast porridge missing, adding it");

    // Here is a simple insert.
    porridge = new Dish { Title = "Breakfast Porridge", Notes = "This is soooo good", Stars = 4 };
    context.Dishes.Add(porridge);
    await context.SaveChangesAsync();

    // Check the output of the following line. Note that the auto-generated key
    // from the DB has been set automatically.
    WriteLine($"\tAdded ({JsonSerializer.Serialize(porridge)})");
}

if (porridge.Stars == 4)
{
    WriteLine("Adding a star to porridge");

    // Oh, we made a mistake. Porridge should have 5 stars. We have to update it.
    // We DO NOT NEED TO RELOAD the object again. It is already in memory. Just
    // change the property and save all changes again.
    porridge.Stars = 5;
    await context.SaveChangesAsync();
}

// Let's check if we already have ingredients for porridge in the DB.
// Note that we can simple do comparison in C#. It will be translated properly into SQL.
if (!await context.Ingredients.AnyAsync(i => i.Dish == porridge))
{
    WriteLine("Adding ingredients");

    // Let's add some dish ingredients
    // Add a single ingredient
    await context.AddAsync(new DishIngredient() { Dish = porridge, Description = "Oat", Amount = 50m, UnitOfMeasure = "g" });

    // Add multiple ingredients
    var ingredients = new DishIngredient[]
    {
        new() { Dish = porridge, Description = "Oat Milk", Amount = 250m, UnitOfMeasure = "g" },
        new() { Dish = porridge, Description = "Cashew Nuts", Amount = 1m, UnitOfMeasure = "hand" },
        new() { Dish = porridge, Description = "Poppyseed", Amount = 1m, UnitOfMeasure = "spoon" },
        new() { Dish = porridge, Description = "Pumpkin Seed Protein Powder", Amount = 1m, UnitOfMeasure = "spoon" },
        new() { Dish = porridge, Description = "Seasonal Fruits", Amount = 2m, UnitOfMeasure = "small apples" }
    };
    await context.AddRangeAsync(ingredients);
    await context.SaveChangesAsync();
}

// For demo purposes, we will delete the dish now
WriteLine("Removing Porridge and all its ingredients");
foreach (var ingredient in await context.Ingredients.Where(i => i.DishId == porridge.Id).ToArrayAsync())
{
    context.Remove(ingredient);
}

context.Remove(porridge);
await context.SaveChangesAsync();

// Create model classes. They will become tables in the database.
// In more advanced scenarios (e.g. with inheritance, m:n relationships),
// the mapping becomes more complex.
// RECOMMENDATION: Use singlar for class names (NOT Dishes, BUT Dish)
class Dish
{
    // Note that by convention, a property named `Id` or `<type name>Id` will be 
    // configured as the primary key of an entity.
    // RECOMMENDATION: Add `Id` to each model class if you do not explicitly want 
    //   to have a different behavior.
    public int Id { get; set; }

    // Note that not-nullable reference type will result in a not-nullable column
    // in the database. 
    // RECOMMENDATION: Always turn Nullable Reference Types on in csproj and 
    //   control nullability via C# types.
    // RECOMMENDATION: Use Data Annotations (https://docs.microsoft.com/en-us/ef/core/modeling/entity-properties)
    //   to add additional metadata (e.g. maximum length, precision, etc.).
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    // This time we use a nullable type
    [MaxLength(1000)]
    public string? Notes { get; set; }

    public int? Stars { get; set; }

    // A dish consists of multiple ingredients. So, we use a `List` on this side
    // of the relation.
    // RECOMMENDATION: Always add a `List<T>` for the n-side of a relationship.
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

    // Note that we reference the Dish that this ingredient relates to
    // by adding a property with the corresponding type.
    // RECOMMENDATION: Add such a property the 1-side of a relationship
    public Dish? Dish { get; set; }

    // Note the naming of the following property. It is <relation>Id.
    // Because of that, it will receive the foreign key value from the DB.
    // RECOMMENDATION: Add such a property the 1-side of a relationship
    public int? DishId { get; set; }
}

// In the DB context we add sets for each model class. We can skip
// entities which will not be used as the basis for queries (in our case
// 
class CookbookContext : DbContext
{
    // RECOMMENDATION: Use plural in the context (Dishes, NOT Dish)
    public DbSet<Dish> Dishes { get; set; }

    public DbSet<DishIngredient> Ingredients { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public CookbookContext(DbContextOptions<CookbookContext> options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

// This factory is responsible for creating our DB context. Note that
// this will NOT BE NECESSARY anymore once we move to ASP.NET.
class CookbookContextFactory : IDesignTimeDbContextFactory<CookbookContext>
{
    public CookbookContext CreateDbContext(string[]? args = null)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<CookbookContext>();
        optionsBuilder
            // Uncomment the following line if you want to print generated
            // SQL statements on the console.
            // .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

        return new CookbookContext(optionsBuilder.Options);
    }
}
