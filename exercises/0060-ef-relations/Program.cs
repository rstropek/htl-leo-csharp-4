using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

var factory = new BrickContextFactory();
using var context = factory.CreateDbContext();

await AddData();
await QueryData();

#region Adding data with relations
async Task AddData()
{
    Vendor brickKing, bunteSteine, heldDerSteine, brickHeaven;
    await context.AddRangeAsync(new[]
    {
        brickKing = new Vendor { VendorName = "Brick King" },
        bunteSteine = new Vendor { VendorName = "Bunte Steine" },
        heldDerSteine = new Vendor { VendorName = "Held der Steine" },
        brickHeaven = new Vendor { VendorName = "Brick Heaven" },
    });
    await context.SaveChangesAsync();

    Tag rare, ninjago, minecraft;
    await context.AddRangeAsync(new[]
    {
    rare = new Tag { Title = "Rare" },
    ninjago = new Tag { Title = "Ninjago" },
    minecraft = new Tag { Title = "Mincraft" },
});
    await context.SaveChangesAsync();

    await context.AddAsync(new BasePlate
    {
        Title = "Baseplate 16 x 16 with Island on Blue Water Pattern",
        Color = Color.Green,
        Tags = new() { rare, minecraft },
        Length = 16,
        Width = 16,
        Availability = new List<BrickAvailability>
    {
       new() { Vendor = bunteSteine, AvailableAmount = 5, PriceEur = 6.6m },
       new() { Vendor = heldDerSteine, AvailableAmount = 10, PriceEur = 5.9m },
    }
    });
    await context.AddAsync(new Brick
    {
        Title = "Brick 1 x 2 x 1",
        Color = Color.Orange
    });
    await context.AddAsync(new MinifigHead
    {
        Title = "Minifigure, Head Dual Sided Black Eyebrows, Wide Open Mouth / Lopsided Grin",
        Color = Color.Yellow
    });
    await context.SaveChangesAsync();
}
#endregion

#region Loading data with relations
async Task QueryData()
{
    var availabilities = await context.BrickAvailabilities
        .Include(ba => ba.Brick)
        .Include(ba => ba.Vendor)
        .ToArrayAsync();
    foreach (var item in availabilities)
    {
        WriteLine($"Brick {item.Brick!.Title} available at {item.Vendor!.VendorName} for {item.PriceEur}");
    }

    WriteLine();
    var brickWithVendors = await context.Bricks
        .Include("Availability.Vendor")
        .Include(b => b.Tags)
        .ToArrayAsync();
    foreach (var item in brickWithVendors)
    {
        Write($"Brick {item.Title} ");
        if (item.Tags.Any()) Write($"({string.Join(',', item.Tags.Select(t => t.Title))}) ");
        if (item.Availability.Any()) Write($"is available at {string.Join(',', item.Availability.Select(a => a.Vendor!.VendorName))}");
        WriteLine();
    }

    WriteLine();
    var bricks = await context.Bricks.ToArrayAsync();
    foreach (var item in bricks)
    {
        await context.Entry(item).Collection(b => b.Tags).LoadAsync();
        if (!item.Tags.Any()) continue;
        WriteLine($"Brick {item.Title} ({string.Join(',', item.Tags.Select(t => t.Title))}) ");
    }
}
#endregion

#region Model
// Note that we can use enums with EF without any issues
enum Color
{
    White,
    Black,
    DarkRed,
    Red,
    Coral,
    Tan,
    Nougat,
    DarkOrange,
    Orange,
    Yellow,
    Green
}

class Brick
{
    public int Id { get; set; }

    [MaxLength(250)]
    public string Title { get; set; } = string.Empty;

    public Color? Color { get; set; }

    public List<Tag> Tags { get; set; } = new();

    public List<BrickAvailability> Availability { get; set; } = new();
}

class BasePlate : Brick
{
    public int Length { get; set; }

    public int Width { get; set; }
}

class MinifigHead: Brick
{
    public bool IsDualSided { get; set; }
}

class Tag
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    public List<Brick> Bricks { get; set; } = new();
}

class Vendor
{
    public int Id { get; set; }

    [MaxLength(250)]
    public string VendorName { get; set; } = string.Empty;

    public List<BrickAvailability> Availability { get; set; } = new();
}

class BrickAvailability
{
    public int Id { get; set; }

    [Required]
    public Brick? Brick { get; set; }

    public int? BrickId { get; set; }

    [Required]
    public Vendor? Vendor { get; set; }

    public int? VendorId { get; set; }

    public int AvailableAmount { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal PriceEur { get; set; }
}
#endregion

#region DBContext
class BrickContext : DbContext
{
    public DbSet<Brick> Bricks { get; set; }

    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<BrickAvailability> BrickAvailabilities { get; set; }
    public DbSet<Tag> Tags { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public BrickContext(DbContextOptions<BrickContext> options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BasePlate>().HasBaseType<Brick>();
        modelBuilder.Entity<MinifigHead>().HasBaseType<Brick>();
    }
}

class BrickContextFactory : IDesignTimeDbContextFactory<BrickContext>
{
    public BrickContext CreateDbContext(string[]? args = null)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<BrickContext>();
        optionsBuilder
            // Uncomment the following line if you want to print generated
            // SQL statements on the console.
            //.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

        return new BrickContext(optionsBuilder.Options);
    }
}
#endregion
