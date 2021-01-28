using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public class Price
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public decimal ProductPrice { get; set; }
    }

    public class Log
    {
        public int Id { get; set; }
        public string User { get; set; }
        public DateTime LogDateTime { get; set; }
        public string Description { get; set; }
    }

    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        { }

        public DbSet<Price> Prices { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
