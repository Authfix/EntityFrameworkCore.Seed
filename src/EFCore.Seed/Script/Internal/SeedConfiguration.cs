//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using System;

namespace Authfix.EntityFrameworkCore.Seed.Script.Internal
{
    internal class SeedConfiguration : ISeedConfiguration
    {
        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initialize <see cref="SeedConfiguration"/>
        /// </summary>
        /// <param name="isInMemory">If the configuration is in memory</param>
        /// <param name="serviceProvider">The service provider</param>
        public SeedConfiguration(bool isInMemory, IServiceProvider serviceProvider)
        {
            IsInMemory = isInMemory;

            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Define if the seed run in memory or not
        /// </summary>
        public bool IsInMemory { get; }

        /// <summary>
        /// Gets the configuration
        /// </summary>
        /// <typeparam name="TConfiguration"></typeparam>
        /// <returns></returns>
        public TConfiguration Get<TConfiguration>()
        {
            var configurationType = typeof(TConfiguration);

            var service = _serviceProvider.GetService(configurationType);

            if(service is Func<TConfiguration> x)
            {
                return x.Invoke();
            }

            return (TConfiguration)service;
        }
    }
}
