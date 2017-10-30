using Authfix.EntityFrameworkCore.Seed.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Authfix.EntityFrameworkCore.Seed.Repositories
{
    public static class WebHostExtensions
    {
        /// <summary>
        /// Seeds the data.
        /// </summary>
        /// <param name="webHost">The web host.</param>
        public static void SeedData<T>(this IWebHost webHost) where T : DbContext
        {
            using (var serviceScope = webHost.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var appContext = serviceScope.ServiceProvider.GetService<T>();

                appContext.Database.Seed();
            }
        }
    }
}
