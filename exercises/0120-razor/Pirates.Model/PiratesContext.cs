using Microsoft.EntityFrameworkCore;

namespace Pirates.Model
{
    public class PiratesContext : DbContext
    {
        public PiratesContext(DbContextOptions<PiratesContext> options)
            : base(options)
        { }

        public DbSet<Pirate> Pirates => Set<Pirate>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pirate>().HasData(new Pirate
            {
                Name = "Blackbeard",
                RealName = "Edward Teach",
                YearOfBirth = 1680,
                YearOfDeath = 1718,
                CountryOfOrigin = "England"
            });

            modelBuilder.Entity<Pirate>().HasData(new Pirate
            {
                Name = "Black Sam",
                RealName = "Samuel Bellamy",
                YearOfBirth = 1689,
                YearOfDeath = 1717,
                CountryOfOrigin = "England"
            });

            modelBuilder.Entity<Pirate>().HasData(new Pirate
            {
                RealName = "David Herriot",
                CountryOfOrigin = "Jamaica"
            });
        }
    }
}
