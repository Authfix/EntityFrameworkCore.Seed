﻿using Daztane.EntityFrameworkCore.Seed.Extensions;
using Daztane.EntityFrameworkCore.Seed.Postgres.Repositories;
using Daztane.EntityFrameworkCore.Seed.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Daztane.EntityFrameworkCore.Seed.Postgres.Extensions
{
    public class PostgresSeedDbContextOptionsExtension : SeedDbContextOptionsExtension
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgresSeedDbContextOptionsExtension"/> class.
        /// </summary>
        /// <param name="seedAssembly">The seed assembly.</param>
        public PostgresSeedDbContextOptionsExtension(string seedAssembly) : base(seedAssembly)
        {
        }

        /// <summary>
        /// Gets the name of the seed provider.
        /// </summary>
        /// <value>
        /// The name of the seed provider.
        /// </value>
        public override string SeedProviderName => "Postgres";

        /// <summary>
        /// Applies the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public override bool ApplyServices(IServiceCollection services)
        {
            base.ApplyServices(services);

            services.AddScoped<ISeedRepository, PostgresSeedRepository>();

            return true; 
        }
    }
}
