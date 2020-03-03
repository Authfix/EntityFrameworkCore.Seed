using Authfix.EntityFrameworkCore.Seed.Postgres.Extensions;
using Authfix.EntityFrameworkCore.Seed.InMemory.Extensions;
using EFCore.Samples.WebApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using EFCore.Samples.WebApp.Seeds;
using System;
using EFCore.Samples.WebApp.Configuration;
using Microsoft.Extensions.Hosting;

namespace EFCore.Samples.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });

            services.AddSingleton<IAppConfiguration, AppConfiguration>();

            services
                .AddEntityFrameworkInMemoryDatabase()
                //.AddEntityFrameworkNpgsql()
                .AddDbContext<AnotherDbContext>(options =>
                {
                    options.UseInMemoryDatabase("MemoryDB");
                    options.UseInMemorySeed(Assembly.GetEntryAssembly().FullName, o =>
                    {
                        o.UseServiceProvider(services.BuildServiceProvider());
                    });
                    //options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                    //options.UseNpgsqlSeed(Assembly.GetEntryAssembly().FullName);
                })
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("MemoryDB");
                    options.UseInMemorySeed(Assembly.GetEntryAssembly().FullName, o =>
                    {
                        o.AddParameter(new IdentityConfiguration(Guid.NewGuid()));
                    });
                    //options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                    //options.UseNpgsqlSeed(Assembly.GetEntryAssembly().FullName);
                });
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
