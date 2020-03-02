//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Infrastructure;
using Authfix.EntityFrameworkCore.Seed.Postgres.Script.Internal;
using Authfix.EntityFrameworkCore.Seed.Script;
using Microsoft.Extensions.DependencyInjection;

namespace Authfix.EntityFrameworkCore.Seed.Postgres.Infrastructure.Internal
{
    public class PostgresSeedOptionsExtension : SeedOptionsExtension
    {
        /// <summary>
        /// Initialize default <see cref="PostgresSeedOptionsExtension"/>
        /// </summary>
        public PostgresSeedOptionsExtension()
        {
        }

        /// <summary>
        /// Initialize <see cref="PostgresSeedOptionsExtension"/> based on an existing one
        /// </summary>
        /// <param name="copyFrom">The existing seed options</param>
        public PostgresSeedOptionsExtension(PostgresSeedOptionsExtension copyFrom) : base(copyFrom)
        {
        }

        /// <summary>
        /// Gets the seed provider name
        /// </summary>
        public override string SeedProviderName => "Postgres";

        /// <summary>
        /// Define if the current provider is for an in memory database or not
        /// </summary>
        public override bool IsInMemoryProvider => false;

        /// <summary>
        /// Add specific services
        /// </summary>
        /// <param name="services">The existing service collection</param>
        /// <returns></returns>
        public override bool ApplyServices(IServiceCollection services)
        {
            base.ApplyServices(services);

            services.AddScoped<ISeedRepository, PostgresSeedRepository>();

            return true;
        }

        /// <summary>
        /// Clone the existing seed options
        /// </summary>
        /// <returns></returns>
        protected override SeedOptionsExtension Clone() => new PostgresSeedOptionsExtension(this);
    }
}
