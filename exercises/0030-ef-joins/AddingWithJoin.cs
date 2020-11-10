using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Joins
{
    partial class Program
    {
        static async Task AddWithJoinAsync(OrderContext context)
        {
            var order = new Order { Product = "Bike", 
                Customer = new Customer { Name = "John" } };

            // Note SINGLE call to `Add` and `SaveChangesAsync`
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }
    }
}
