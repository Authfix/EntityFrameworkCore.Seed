using Authfix.EntityFrameworkCore.Seed.Script;
using EFCore.Samples.WebApp.Data;
using EFCore.Samples.WebApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace EFCore.Samples.WebApp.Seeds.Standard
{
    [SeedAttribute("20171010165344_InitialScenario")]
    [DbContext(typeof(ApplicationDbContext))]
    public class Seed_20171010165344_InitialScenario : SeedBase
    {
        public Seed_20171010165344_InitialScenario(DbContext dbContext, ILogger logger, ISeedConfiguration seedConfiguration) : base(dbContext, logger, seedConfiguration)
        {
        }

        protected override void UpdateEntities()
        {
            var users = DbSet<User>();

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
