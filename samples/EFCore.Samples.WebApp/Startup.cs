using Authfix.EntityFrameworkCore.Seed.Postgres.Extensions;
using Authfix.EntityFrameworkCore.Seed.InMemory.Extensions;
using Authfix.EntityFrameworkCore.Seed.SqlServer.Extensions;
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
using EFCore.Samples.Data;
using EFCore.Samples.Data.Migrations.SqlServer;
using EFCore.Samples.Data.Migrations.Postgresql;

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

            var provider = Configuration["Provider"];

            switch (provider)
            {
                case "SqlServer":
                    services.AddEntityFrameworkSqlServer();
                    break;
                case "Postgresql":
                    services.AddEntityFrameworkNpgsql();
                    break;
                case "InMemory":
                default:
                    services.AddEntityFrameworkInMemoryDatabase();
                    break;
            }

            services.AddDbContext<AnotherDbContext>(options =>
            {
                switch (provider)
                {
                    case "SqlServer":
                        options.UseSqlServer(Configuration.GetConnectionString("SqlServer"), o => 
                        {
                            o.MigrationsAssembly(typeof(SqlServerMigrationExtension).Assembly.FullName); 
                        });
                        options.UseSqlServerSeed(Assembly.GetEntryAssembly().FullName, o =>
                        {
                            o.UseServiceProvider(services.BuildServiceProvider());
                        });
                        break;
                    case "Postgresql":
                        options.UseNpgsql(Configuration.GetConnectionString("Postgres"), o =>
                        {
                            o.MigrationsAssembly(typeof(PostgresqlMigrationExtension).Assembly.FullName);
                        });
                        options.UseNpgsqlSeed(Assembly.GetEntryAssembly().FullName, o =>
                        {
                            o.UseServiceProvider(services.BuildServiceProvider());
                        });
                        break;
                    case "InMemory":
                    default:
                        options.UseInMemoryDatabase("MemoryDB");
                        options.UseInMemorySeed(Assembly.GetEntryAssembly().FullName, o =>
                        {
                            o.UseServiceProvider(services.BuildServiceProvider());
                        });
                        break;
                }
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                switch (provider)
                {
                    case "SqlServer":
                        options.UseSqlServer(Configuration.GetConnectionString("SqlServer"), o =>
                        {
                            o.MigrationsAssembly(typeof(SqlServerMigrationExtension).Assembly.FullName);
                        });
                        options.UseSqlServerSeed(Assembly.GetEntryAssembly().FullName, o =>
                        {
                            o.AddParameter(new IdentityConfiguration(Guid.NewGuid()));
                        });
                        break;
                    case "Postgresql":
                        options.UseNpgsql(Configuration.GetConnectionString("Postgres"), o =>
                        {
                            o.MigrationsAssembly(typeof(PostgresqlMigrationExtension).Assembly.FullName);
                        });
                        options.UseNpgsqlSeed(Assembly.GetEntryAssembly().FullName, o =>
                        {
                            o.AddParameter(new IdentityConfiguration(Guid.NewGuid()));
                        });
                        break;
                    case "InMemory":
                    default:
                        options.UseInMemoryDatabase("MemoryDB");
                        options.UseInMemorySeed(Assembly.GetEntryAssembly().FullName, o =>
                        {
                            o.AddParameter(new IdentityConfiguration(Guid.NewGuid()));
                        });
                        break;
                }
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
