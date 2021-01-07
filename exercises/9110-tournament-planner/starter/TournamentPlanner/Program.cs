using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TournamentPlanner
{
    public class Program
    {
        // Note: This class is COMPLETE. You do not need to change anything here.

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
