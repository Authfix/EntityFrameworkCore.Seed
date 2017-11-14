//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Extensions;
using Authfix.EntityFrameworkCore.Seed.Postgres.Repositories;
using Authfix.EntityFrameworkCore.Seed.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Authfix.EntityFrameworkCore.Seed.Postgres.Extensions
{
    internal class PostgresSeedDbContextOptionsExtension : SeedDbContextOptionsExtension
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
