using EFCore.Samples.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.Samples.WebApp.Extensions
{
    /// <summary>
    /// Class who manage <see cref="IApplicationBuilder"/> extensions
    /// </summary>
    public static class WebHostExtensions
    {
        /// <summary>
        /// Migrates the database.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void MigrateDatabase(this IWebHost app)
        {
            var services = app.Services;

            using (var serviceScope = services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var provider = serviceScope.ServiceProvider.GetService<IConfiguration>()["Provider"];

                if(provider == "InMemory")
                {
                    return;
                }

                var applicationDbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                applicationDbContext.Database.Migrate();
                applicationDbContext.Dispose();

                var anotherDbContext = serviceScope.ServiceProvider.GetService<AnotherDbContext>();
                anotherDbContext.Database.Migrate();
                anotherDbContext.Dispose();
            }
        }
    }
}
