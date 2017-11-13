using Authfix.EntityFrameworkCore.Seed.Attributes;
using Authfix.EntityFrameworkCore.Seed.Models;
using EFCore.Samples.WebApp.Models;

namespace EFCore.Samples.WebApp.Seeds
{
    [SeedAttribute("20171010165344_InitialScenario")]
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
