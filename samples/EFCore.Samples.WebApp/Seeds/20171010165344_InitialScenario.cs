﻿using Authfix.EntityFrameworkCore.Seed.Attributes;
using Authfix.EntityFrameworkCore.Seed.Models;
using EFCore.Samples.WebApp.Data;
using EFCore.Samples.WebApp.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.Samples.WebApp.Seeds
{
    [SeedAttribute("20171010165344_InitialScenario")]
    [DbContext(typeof(ApplicationDbContext))]
    public class Seed_20171010165344_InitialScenario : SeederBase
    {
        protected override void UpdateEntities()
        {
            var users = GetDbSet<User>();

            for (int i = 1; i < 50; i++)
            {
                var newUser = new User
                {
                    Id = i
                };

                users.Add(newUser);
            }
        }
    }
}
