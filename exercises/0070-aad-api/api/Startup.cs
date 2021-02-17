using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using System.Security.Claims;

namespace ProtectedApi
{
    public record Startup(IConfiguration Configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add web API authentication to dependency injection.
            // Note that you need to add Microsoft.Identity.Web NuGet to your
            // ASP.NET Core 5 app to make that work. Docs see
            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.identity.web.microsoftidentitywebappservicecollectionextensions.addmicrosoftidentitywebappauthentication
            services.AddMicrosoftIdentityWebApiAuthentication(Configuration);

            services.AddCors();

            // Don't forget to add authentication and authorization to DI.
            services.AddAuthentication();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RainerOnly", policy => policy.RequireClaim(ClaimTypes.Name, "r.stropek@HTBLALeonding.onmicrosoft.com"));
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseRouting();

            // Add authentication and authorization middleware.
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
