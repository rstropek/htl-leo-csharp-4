using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;
using VaccinateRegistration.Data;

// Note: This file is COMPLETE. You do not need to change anything here.

namespace VaccinateRegistration
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            if (args.Length > 0) await ImportRegistrations(args, host!);
            else host.Run();
        }

        private static async Task ImportRegistrations(string[] args, IHost host)
        {
            if (args.Length == 2 && args[0] == "import")
            {
                using var scope = host.Services.CreateScope();
                var context = (VaccinateDbContext)(scope.ServiceProvider.GetService(typeof(VaccinateDbContext)))!;
                await context.DeleteEverything();
                var registrations = await context.ImportRegistrations(args[1]);
                Console.WriteLine($"{registrations.Count()} successfully imported");
                return;
            }

            Console.Error.WriteLine("Invalid command line arguments");
            return;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
