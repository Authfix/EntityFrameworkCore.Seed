//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Infrastructure;
using Authfix.EntityFrameworkCore.Seed.InMemory.Script.Internal;
using Authfix.EntityFrameworkCore.Seed.Script;
using Microsoft.Extensions.DependencyInjection;

namespace Authfix.EntityFrameworkCore.Seed.InMemory.Infrastructure.Internal
{
    public class InMemorySeedOptionsExtension : SeedOptionsExtension
    {
        /// <summary>
        /// Initialize default <see cref="InMemorySeedOptionsExtension"/>
        /// </summary>
        public InMemorySeedOptionsExtension()
        {
        }

        /// <summary>
        /// Initialize <see cref="InMemorySeedOptionsExtension"/> based on an existing one
        /// </summary>
        /// <param name="copyFrom">The existing seed options</param>
        public InMemorySeedOptionsExtension(InMemorySeedOptionsExtension copyFrom) : base(copyFrom)
        {
        }

        /// <summary>
        /// Gets the seed provider name
        /// </summary>
        public override string SeedProviderName => "InMemory";

        /// <summary>
        /// Define if the current provider is for an in memory database or not
        /// </summary>
        public override bool IsInMemoryProvider => true;

        /// <summary>
        /// Add specific services
        /// </summary>
        /// <param name="services">The existing service collection</param>
        /// <returns></returns>
        public override bool ApplyServices(IServiceCollection services)
        {
            base.ApplyServices(services);

            services.AddScoped<ISeedRepository, InMemorySeedRepository>();

            return true;
        }

        /// <summary>
        /// Clone the existing seed options
        /// </summary>
        /// <returns></returns>
        protected override SeedOptionsExtension Clone() => new InMemorySeedOptionsExtension(this);
    }
}
