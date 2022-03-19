//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

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
        /// <param name="host">The host.</param>
        public static IHost SeedData<T>(this IHost host) where T : DbContext
        {
            Seed(host.Services, typeof(T));

            return host;
        }

        /// <summary>
        /// Seed the current database
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="dbContextType">The database context type.</param>
        private static void Seed(IServiceProvider serviceProvider, Type dbContextType)
        {
            using var serviceScope = serviceProvider.CreateScope();
            
            var appContext = (DbContext)serviceScope.ServiceProvider.GetService(dbContextType);

            if (appContext == null)
            {
                throw new Exception(
                    $"The database context {dbContextType.Name} is not registered into the application");
            }
            
            appContext.Database.Seed();
        }
    }
}
