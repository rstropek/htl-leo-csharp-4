using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UserManagement.Data;

namespace UserManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserManagementDataContext>(option => option
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddAuthentication(SimulatedAuthenticationOptions.AuthScheme)
                .AddSimulatedAuthentication(
                    userNameidentifier: "foo.bar", // Will be written into ClaimTypes.NameIdentifier
                    userRole: "administrator");    // Will be written into ClaimTypes.Role
            services.AddControllers();

            // Add NSwag to ASP.NET Core dependency injection. To enable that, you need to
            // add the NuGet package NSwag.AspNetCore. For more details see
            // https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag
            services.AddOpenApiDocument(doc =>
            {
                doc.Title = "User Management API";
                doc.Description = "Demo API for C# course of HTL Leonding";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
