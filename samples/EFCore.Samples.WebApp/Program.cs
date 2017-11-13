using Authfix.EntityFrameworkCore.Seed.Repositories;
using EFCore.Samples.WebApp.Data;
using EFCore.Samples.WebApp.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace EFCore.Samples.WebApp
{
    public class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            var webHost = BuildWebHost(args);

            // only for postgres
            webHost.MigrateDatabase();

            webHost.SeedData<ApplicationDbContext>();

            webHost.Run();
        }

        /// <summary>
        /// Builds the web host.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
    }
}
