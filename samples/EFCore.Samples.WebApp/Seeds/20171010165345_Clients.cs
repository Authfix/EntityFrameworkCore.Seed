using Authfix.EntityFrameworkCore.Seed.Attributes;
using Authfix.EntityFrameworkCore.Seed.Models;
using EFCore.Samples.WebApp.Data;
using EFCore.Samples.WebApp.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace EFCore.Samples.WebApp.Seeds
{
    [SeedAttribute("20171010165345_Clients")]
    [DbContext(typeof(AnotherDbContext))]
    public class Seed_20171010165345_Clients : SeederBase
    {
        protected override void UpdateEntities()
        {
            var clients = GetDbSet<Client>();

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
