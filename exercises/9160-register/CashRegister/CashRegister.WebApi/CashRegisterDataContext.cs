using CashRegister.Shared;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.WebApi
{
    public class CashRegisterDataContext : DbContext
    {
        public CashRegisterDataContext(DbContextOptions<CashRegisterDataContext> options)
            : base(options)
        { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<ReceiptLine> ReceiptLines => Set<ReceiptLine>();
        public DbSet<Receipt> Receipts => Set<Receipt>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.ProductName).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.UnitPrice).IsRequired().HasColumnType("numeric(6,2)");

            modelBuilder.Entity<ReceiptLine>().Property(rl => rl.Amount).IsRequired();
            modelBuilder.Entity<ReceiptLine>().Property(rl => rl.TotalPrice).IsRequired().HasColumnType("numeric(6,2)");

            modelBuilder.Entity<Receipt>().Property(r => r.ReceiptTimestamp).IsRequired();
            modelBuilder.Entity<Receipt>().Property(r => r.TotalPrice).IsRequired().HasColumnType("numeric(6,2)");
        }
    }
}
