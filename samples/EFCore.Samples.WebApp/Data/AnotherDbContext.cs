using EFCore.Samples.WebApp.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Samples.WebApp.Data
{
    public class AnotherDbContext : DbContext
    {
        public AnotherDbContext(DbContextOptions<AnotherDbContext> options) : base(options)
        {
        }

        protected AnotherDbContext()
        {
        }

        public DbSet<Client> Clients { get; set; }
    }
}
