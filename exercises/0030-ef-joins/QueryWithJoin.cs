using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Joins
{
    partial class Program
    {
        static async Task QueryWithJoinAsync(OrderContext context)
        {
            var result = await context.Orders
                .Include("Customer")
                .FirstAsync();
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}
