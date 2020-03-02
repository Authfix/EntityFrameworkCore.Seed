//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Infrastructure;
using Authfix.EntityFrameworkCore.Seed.InMemory.Script.Internal;
using Authfix.EntityFrameworkCore.Seed.Script;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;

namespace Authfix.EntityFrameworkCore.Seed.InMemory.Infrastructure.Internal
{
    public class InMemorySeedOptionsExtension : SeedOptionsExtension
    {
        private DbContextOptionsExtensionInfo _info;

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
        /// Gets the context options info
        /// </summary>
        public override DbContextOptionsExtensionInfo Info => _info ??= new ExtensionInfo(this);

        /// <summary>
        /// Add specific services
        /// </summary>
        /// <param name="services">The existing service collection</param>
        /// <returns></returns>
        public override void ApplyServices(IServiceCollection services)
        {
            base.ApplyServices(services);

            services.AddScoped<ISeedRepository, InMemorySeedRepository>();
        }

        /// <summary>
        /// Clone the existing seed options
        /// </summary>
        /// <returns></returns>
        protected override SeedOptionsExtension Clone() => new InMemorySeedOptionsExtension(this);

        private sealed class ExtensionInfo : DbContextOptionsExtensionInfo
        {
            private string _logFragment;

            public ExtensionInfo(IDbContextOptionsExtension extension) : base(extension)
            {
            }

            private new InMemorySeedOptionsExtension Extension => (InMemorySeedOptionsExtension)base.Extension;

            public override bool IsDatabaseProvider => false;

            public override string LogFragment 
            {
                get
                {
                    if(_logFragment == null)
                    {
                        var builder = new StringBuilder();

                        builder.Append("StoreName=").Append(Extension.SeedProviderName).Append(' ');

                        _logFragment = builder.ToString();
                    }

                    return _logFragment;
                }
            }

            public override long GetServiceProviderHashCode()
            {
                return 0L;
            }

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            {
            }
        }
    }
}
