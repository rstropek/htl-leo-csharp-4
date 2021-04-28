using Microsoft.EntityFrameworkCore;

namespace Quotexchange.Api.Data
{
    public class QuotexchangeDataContext : DbContext
    {
        public QuotexchangeDataContext(DbContextOptions<QuotexchangeDataContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<FunnyQuote> Quotes => Set<FunnyQuote>();

        public DbSet<Vote> Votes => Set<Vote>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Add a unique index to name identifier column
            builder.Entity<User>()
                .HasIndex(u => u.NameIdentifier)
                .IsUnique();
        }
    }
}
