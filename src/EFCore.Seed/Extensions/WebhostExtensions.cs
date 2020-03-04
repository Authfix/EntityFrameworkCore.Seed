//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Authfix.EntityFrameworkCore.Seed.Extensions
{
    public static class WebHostExtensions
    {
        /// <summary>
        /// Seeds the data for a specific <see cref="DbContext"/>.
        /// </summary>
        /// <param name="webHost">The web host.</param>
        public static IWebHost SeedData<T>(this IWebHost webHost) where T : DbContext
        {
            Seed(webHost.Services, typeof(T));

            return webHost;
        }

        /// <summary>
        /// Seeds the data for a specific <see cref="DbContext"/>.
        /// </summary>
        /// <param name="host">The host.</param>
        public static IHost SeedData<T>(this IHost host) where T : DbContext
        {
            Seed(host.Services, typeof(T));

            return host;
        }

        /// <summary>
        /// Seed the current database
        /// </summary>
        /// <param name="serviceProvider"></param>
        private static void Seed(IServiceProvider serviceProvider, Type dbContextType)
        {
            using(var serviceScope = serviceProvider.CreateScope())
            {
                var appContext = serviceScope.ServiceProvider.GetService(dbContextType) as DbContext;

                appContext.Database.Seed();
            }
        }
    }
}
