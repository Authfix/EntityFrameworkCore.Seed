﻿using Authfix.EntityFrameworkCore.Seed.Extensions;
using Authfix.EntityFrameworkCore.Seed.InMemory.Repositories;
using Authfix.EntityFrameworkCore.Seed.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Authfix.EntityFrameworkCore.Seed.InMemory.Extensions
{
    public class InMemorySeedDbContextOptionsExtension : SeedDbContextOptionsExtension
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InMemorySeedDbContextOptionsExtension"/> class.
        /// </summary>
        /// <param name="seedAssembly">The seed assembly.</param>
        public InMemorySeedDbContextOptionsExtension(string seedAssembly) : base(seedAssembly)
        {
        }

        /// <summary>
        /// Gets the name of the seed provider.
        /// </summary>
        /// <value>
        /// The name of the seed provider.
        /// </value>
        public override string SeedProviderName => "InMemory";

        /// <summary>
        /// Applies the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public override bool ApplyServices(IServiceCollection services)
        {
            base.ApplyServices(services);

            services.AddScoped<ISeedRepository, InMemorySeedRepository>();

            return true;
        }
    }
}
