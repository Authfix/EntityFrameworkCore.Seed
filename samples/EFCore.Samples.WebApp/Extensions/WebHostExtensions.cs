using EFCore.Samples.WebApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
                var appContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                appContext.Database.Migrate();
            }
        }
    }
}
