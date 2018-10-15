//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Extensions;
using Authfix.EntityFrameworkCore.Seed.InMemory.Repositories;
using Authfix.EntityFrameworkCore.Seed.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Authfix.EntityFrameworkCore.Seed.InMemory.Extensions
{
    internal class InMemorySeedDbContextOptionsExtension : SeedDbContextOptionsExtension
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
        /// Gets value indicating if that the provider is an in memory provider or not
        /// </summary>
        public override bool IsInMemoryProvider => true;

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
