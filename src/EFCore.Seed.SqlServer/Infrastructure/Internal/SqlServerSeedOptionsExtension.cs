//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Infrastructure;
using Authfix.EntityFrameworkCore.Seed.Script;
using Authfix.EntityFrameworkCore.Seed.SqlServer.Script.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Authfix.EntityFrameworkCore.Seed.SqlServer.Infrastructure.Internal
{
    public class SqlServerSeedOptionsExtension : SeedOptionsExtension
    {
        /// <summary>
        /// Initialize default <see cref="SqlServerSeedOptionsExtension"/>
        /// </summary>
        public SqlServerSeedOptionsExtension()
        {
        }

        /// <summary>
        /// Initialize <see cref="SqlServerSeedOptionsExtension"/> based on an existing one
        /// </summary>
        /// <param name="copyFrom">The existing seed options</param>
        public SqlServerSeedOptionsExtension(SqlServerSeedOptionsExtension copyFrom) : base(copyFrom)
        {
        }

        /// <summary>
        /// Gets the seed provider name
        /// </summary>
        public override string SeedProviderName => "SQL Server";

        /// <summary>
        /// Define if the current provider is for an in memory database or not
        /// </summary>
        public override bool IsInMemoryProvider => false;

        public override DbContextOptionsExtensionInfo Info => new ExtensionInfo(this);

        /// <summary>
        /// Add specific services
        /// </summary>
        /// <param name="services">The existing service collection</param>
        /// <returns></returns>
        public override void ApplyServices(IServiceCollection services)
        {
            base.ApplyServices(services);

            services.AddScoped<ISeedRepository, SqlServerSeedRepository>();
        }

        /// <summary>
        /// Clone the existing seed options
        /// </summary>
        /// <returns></returns>
        protected override SeedOptionsExtension Clone() => new SqlServerSeedOptionsExtension(this);

        private sealed class ExtensionInfo : DbContextOptionsExtensionInfo
        {

            public ExtensionInfo(IDbContextOptionsExtension extension) : base(extension)
            {
            }

            private new SqlServerSeedOptionsExtension Extension => (SqlServerSeedOptionsExtension)base.Extension;

            public override bool IsDatabaseProvider => false;

            public override string LogFragment
            {
                get
                {
                    return Extension.LogFragment;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <remarks>
            /// Returns a hash code created from any options that would cause a new <see cref="IServiceProvider" />
            /// to be needed. Most extensions do not have any such options and should return zero.
            /// </remarks>
            /// <returns></returns>
            public override long GetServiceProviderHashCode()
            {
                return 0L;
            }

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            {
                debugInfo["SqlServer"] = "1";
            }
        }
    }
}
