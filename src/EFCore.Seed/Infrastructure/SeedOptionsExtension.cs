//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Script;
using Authfix.EntityFrameworkCore.Seed.Script.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

namespace Authfix.EntityFrameworkCore.Seed.Infrastructure
{
    public abstract class SeedOptionsExtension : IDbContextOptionsExtension
    {
        /// <summary>
        /// The assembly name where we look for seeds
        /// </summary>
        private string _seedAssemblyName;

        /// <summary>
        /// The service resolver
        /// </summary>
        private IServiceProvider _serviceProvider;

        /// <summary>
        /// Initialize default <see cref="SeedOptionsExtension"/>
        /// </summary>
        protected SeedOptionsExtension()
        {
            _serviceProvider = new DefaultServiceProvider();
        }

        /// <summary>
        /// Initialize <see cref="SeedOptionsExtension"/> with existing extension
        /// </summary>
        /// <param name="copyFrom">The existing extension to copy</param>
        public SeedOptionsExtension(SeedOptionsExtension copyFrom)
        {
            _seedAssemblyName = copyFrom._seedAssemblyName;
            _serviceProvider = copyFrom._serviceProvider;
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
        public virtual bool IsInMemoryProvider { get; } = false;

        private string _logFragment;
        /// <summary>
        /// Get the log fragment
        /// </summary>
        public string LogFragment
        {
            get
            {
                if (string.IsNullOrEmpty(_logFragment))
                {
                    var stringBuilder = new StringBuilder();

                    stringBuilder.Append("SeedProviderName=").Append(SeedProviderName).Append(' ');

                    _logFragment = stringBuilder.ToString();
                }

                return _logFragment;
            }
        }

        /// <summary>
        /// Gets the seed configuration from parameters
        /// </summary>
        public ISeedConfiguration SeedConfiguration
        {
            get { return new SeedConfiguration(IsInMemoryProvider, _serviceProvider); }
        }


        /// <summary>
        ///     Override this method in a derived class to ensure that any clone created is also of that class.
        /// </summary>
        /// <returns> A clone of this instance, which can be modified before being returned as immutable. </returns>
        protected abstract SeedOptionsExtension Clone();

        /// <summary>
        /// Return a copy of the options with the seed assembly configured
        /// </summary>
        /// <param name="seedAssemblyName">The full name of the assembly to use.</param>
        /// <returns></returns>
        public virtual SeedOptionsExtension WithSeedAssemblyName(string seedAssemblyName)
        {
            var clone = Clone();

            clone._seedAssemblyName = seedAssemblyName;

            return clone;
        }

        /// <summary>
        /// Use a specific service provider. 
        /// If you call this method, you can't use WithParameter method anymore
        /// </summary>
        /// <param name="serviceProvider">The service provider to use</param>
        /// <returns></returns>
        public SeedOptionsExtension WithServiceProvider(IServiceProvider serviceProvider)
        {
            var clone = Clone();

            clone._serviceProvider = serviceProvider;

            return clone;
        }

        /// <summary>
        /// Return a copy of the options with the new parameter
        /// </summary>
        /// <typeparam name="TParameter">The parameter type</typeparam>
        /// <param name="parameter">The parameter to add</param>
        /// <returns></returns>
        public SeedOptionsExtension WithParameter<TParameter>(TParameter parameter)
        {
            return WithParameter(() => parameter);
        }

        /// <summary>
        /// Return a copy of the options with the new parameter
        /// </summary>
        /// <typeparam name="TParameter">The parameter type</typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        public SeedOptionsExtension WithParameter<TParameter>(Func<TParameter> factory)
        {
            var clone = Clone();

            var updatableServiceProvider = _serviceProvider as IUpdatableServiceProvider;

            if(updatableServiceProvider == null)
            {
                throw new InvalidSeedException("You cannot add parameter if you use UseServiceProvider method");
            }

            updatableServiceProvider.AddService(typeof(TParameter).FullName, factory);

            return clone;
        }

        /// <summary>
        /// Add specific services
        /// </summary>
        /// <param name="services">The existing service collection</param>
        /// <returns></returns>
        public virtual bool ApplyServices(IServiceCollection services)
        {
            if (IsInMemoryProvider)
                services.AddScoped<ISeeder, Seeder>();
            else
                services.AddScoped<ISeeder, RelationalSeeder>();

            services.AddScoped<ISeedAssembly, SeedAssembly>(sp => new SeedAssembly(_seedAssemblyName));

            return true;
        }

        /// <summary>
        /// Gets the service provider hash code
        /// </summary>
        /// <returns></returns>
        public virtual long GetServiceProviderHashCode() => 0;

        /// <summary>
        /// Validate database context options
        /// </summary>
        /// <param name="options">The options to validate</param>
        public virtual void Validate(IDbContextOptions options)
        {
        }
    }
}
