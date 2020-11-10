using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Joins
{
    class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    class Order
    {
        public int ID { get; set; }
        public string Product { get; set; }
        public Customer Customer { get; set; }
    }

    class OrderContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Orders");
        }

    }

    partial class Program
    {
        static void Main(string[] args)
        {
            using (var context = new OrderContext())
            {
                Program.AddWithJoinAsync(context).Wait();
                Program.QueryWithJoinAsync(context).Wait();
            }
        }
    }
}
