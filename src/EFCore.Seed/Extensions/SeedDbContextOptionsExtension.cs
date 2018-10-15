//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Configuration;
using Authfix.EntityFrameworkCore.Seed.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Authfix.EntityFrameworkCore.Seed.Extensions
{
    public abstract class SeedDbContextOptionsExtension : IDbContextOptionsExtension
    {
        /// <summary>
        /// The seed assembly
        /// </summary>
        private readonly string _seedAssembly;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeedDbContextOptionsExtension"/> class.
        /// </summary>
        /// <param name="seedAssembly">The seed assembly.</param>
        public SeedDbContextOptionsExtension(string seedAssembly)
        {
            _seedAssembly = seedAssembly;
        }

        private string _logFragment;
        /// <summary>
        /// Creates a message fragment for logging typically containing information about
        /// any useful non-default options that have been configured.
        /// </summary>
        public string LogFragment
        {
            get
            {
                if (_logFragment == null)
                {
                    var builder = new StringBuilder();

                    builder.Append("SeedProviderName=").Append(SeedProviderName).Append(' ');

                    _logFragment = builder.ToString();
                }

                return _logFragment;
            }
        }

        /// <summary>
        /// Gets the name of the seed provider.
        /// </summary>
        /// <value>
        /// The name of the seed provider.
        /// </value>
        public abstract string SeedProviderName { get; }

        /// <summary>
        /// Gets value indicating if the provider is an in memory provider
        /// </summary>
        public virtual bool IsInMemoryProvider { get; }

        /// <summary>
        /// Adds the services required to make the selected options work. This is used when there
        /// is no external <see cref="T:System.IServiceProvider" /> and EF is maintaining its own service
        /// provider internally. This allows database providers (and other extensions) to register their
        /// required services when EF is creating an service provider.
        /// </summary>
        /// <param name="services">The collection to add services to.</param>
        /// <returns>
        /// True if a database provider and was registered; false otherwise.
        /// </returns>
        public virtual bool ApplyServices(IServiceCollection services)
        {
            services.AddScoped<ISeeder, Seeder>();
            services.AddSingleton<ISeedAssembly>(new SeedAssembly(_seedAssembly));
            services.AddSingleton(new SeedConfiguration(IsInMemoryProvider));

            return true;
        }

        /// <summary>
        /// Returns a hash code created from any options that would cause a new <see cref="T:System.IServiceProvider" />
        /// to be needed. Most extensions do not have any such options and should return zero.
        /// </summary>
        /// <returns>
        /// A hash over options that require a new service provider when changed.
        /// </returns>
        public long GetServiceProviderHashCode()
        {
            return LogFragment.GetHashCode();
        }

        /// <summary>
        /// Gives the extension a chance to validate that all options in the extension are valid.
        /// Most extensions do not have invalid combinations and so this will be a no-op.
        /// If options are invalid, then an exception should be thrown.
        /// </summary>
        /// <param name="options">The options being validated.</param>
        public void Validate(IDbContextOptions options)
        {
        }
    }
}
