using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using UserManagement.Data;

// This file contains classes used to simulate user authentication. It is
// provided by your teacher. You do not need to change it in any way.

namespace UserManagement
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // If app is called with `fill` as first command line argument,
            // it will fill the connected database with demo data using
            // the `DemoDataGenerator` class.
            if (args.Length > 0 && args[0] == "fill")
            {
                // Create an EFCore data context from dependency injection's service collection
                using var scope = host.Services.CreateScope();
                using var dc = scope.ServiceProvider.GetRequiredService<UserManagementDataContext>();

                // Trigger filling of database
                var generator = new DemoDataGenerator(dc);
                await generator.ClearAll();
                await generator.Fill();

                Console.WriteLine("Database has been successfully filled");
                return;
            }

            // We are not in "fill DB with demo data" mode.
            // Therefore, we start the web server for the web API.
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
