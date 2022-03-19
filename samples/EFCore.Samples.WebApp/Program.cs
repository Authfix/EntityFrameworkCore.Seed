using System;
using System.Reflection;
using Authfix.EntityFrameworkCore.Seed.Extensions;
using Authfix.EntityFrameworkCore.Seed.InMemory.Extensions;
using Authfix.EntityFrameworkCore.Seed.Postgres.Extensions;
using Authfix.EntityFrameworkCore.Seed.SqlServer.Extensions;
using EFCore.Samples.Data;
using EFCore.Samples.Data.Migrations.Postgresql;
using EFCore.Samples.Data.Migrations.SqlServer;
using EFCore.Samples.WebApp.Configuration;
using EFCore.Samples.WebApp.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Configuration["Provider"];

builder.Services.AddRouting(o => o.LowercaseUrls = true);
builder.Services.AddMvc();

builder.Services.AddSingleton<IAppConfiguration, AppConfiguration>();

builder.Services.AddDbContext<AnotherDbContext>(options =>
{
    switch (provider)
    {
        case "SqlServer":
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
                o => { o.MigrationsAssembly(typeof(SqlServerMigrationExtension).Assembly.FullName); });
            options.UseSqlServerSeed(Assembly.GetEntryAssembly()!.FullName,
                o => { o.UseServiceProvider(builder.Services.BuildServiceProvider()); });
            break;
        case "Postgresql":
            options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"),
                o => { o.MigrationsAssembly(typeof(PostgresqlMigrationExtension).Assembly.FullName); });
            options.UseNpgsqlSeed(Assembly.GetEntryAssembly()!.FullName,
                o => { o.UseServiceProvider(builder.Services.BuildServiceProvider()); });
            break;
        default:
            options.UseInMemoryDatabase("MemoryDB");
            options.UseInMemorySeed(Assembly.GetEntryAssembly()!.FullName,
                o => { o.UseServiceProvider(builder.Services.BuildServiceProvider()); });
            break;
    }
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    switch (provider)
    {
        case "SqlServer":
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
                o => { o.MigrationsAssembly(typeof(SqlServerMigrationExtension).Assembly.FullName); });
            options.UseSqlServerSeed(Assembly.GetEntryAssembly()!.FullName,
                o => { o.AddParameter(new IdentityConfiguration(Guid.NewGuid())); });
            break;
        case "Postgresql":
            options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"),
                o => { o.MigrationsAssembly(typeof(PostgresqlMigrationExtension).Assembly.FullName); });
            options.UseNpgsqlSeed(Assembly.GetEntryAssembly()!.FullName,
                o => { o.AddParameter(new IdentityConfiguration(Guid.NewGuid())); });
            break;
        default:
            options.UseInMemoryDatabase("MemoryDB");
            options.UseInMemorySeed(Assembly.GetEntryAssembly()!.FullName,
                o => { o.AddParameter(new IdentityConfiguration(Guid.NewGuid())); });
            break;
    }
});

// ===========================================================================================================================================================

var app = builder.Build();

app.MapControllers();

// ===========================================================================================================================================================

app.SeedData<ApplicationDbContext>();
app.SeedData<AnotherDbContext>();

app.Run();