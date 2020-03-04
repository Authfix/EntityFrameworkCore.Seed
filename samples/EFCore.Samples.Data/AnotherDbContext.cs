using EFCore.Samples.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Samples.Data
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
