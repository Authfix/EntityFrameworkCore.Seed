using Authfix.EntityFrameworkCore.Seed.Script;
using EFCore.Samples.WebApp.Configuration;
using EFCore.Samples.WebApp.Data;
using EFCore.Samples.WebApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System;

namespace EFCore.Samples.WebApp.Seeds.Standard
{
    [SeedAttribute("20171010165345_Clients")]
    [DbContext(typeof(AnotherDbContext))]
    public class Seed_20171010165345_Clients : SeedBase
    {
        public Seed_20171010165345_Clients(DbContext dbContext, ILogger logger, ISeedConfiguration seedConfiguration) : base(dbContext, logger, seedConfiguration)
        {
            var a = seedConfiguration.Get<IAppConfiguration>();
        }

        protected override void UpdateEntities()
        {
            var clients = DbSet<Client>();

            for (int i = 1; i < 50; i++)
            {
                var newClient = new Client
                {
                    Id = Guid.NewGuid(),
                    Name = Guid.NewGuid().ToString()
                };

                clients.Add(newClient);
            }
        }
    }
}
