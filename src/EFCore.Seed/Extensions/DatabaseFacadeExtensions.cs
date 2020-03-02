//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Script.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Authfix.EntityFrameworkCore.Seed.Extensions
{
    public static class DatabaseFacadeExtensions
    {
        /// <summary>
        /// Seeds data for the specified facade.
        /// </summary>
        /// <param name="databaseFacade">The database facade.</param>
        public static void Seed(this DatabaseFacade databaseFacade)
        {
            databaseFacade.GetDatabaseService<ISeeder>().Seed();
        }

        /// <summary>
        /// Gets the database service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="databaseFacade">The database facade where looking for the service.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">The service is not registered in the facade.</exception>
        private static TService GetDatabaseService<TService>(this IInfrastructure<IServiceProvider> databaseFacade)
        {
            var service = databaseFacade.Instance.GetService<TService>();

            if (service == null)
            {
                throw new InvalidOperationException($"Service {typeof(TService).Name} not found");
            }

            return service;
        }
    }
}
