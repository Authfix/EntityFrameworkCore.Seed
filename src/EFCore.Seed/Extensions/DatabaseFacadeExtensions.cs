using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Authfix.EntityFrameworkCore.Seed.Extensions
{
    public static class DatabaseFacadeExtensions
    {
        /// <summary>
        /// Seeds the specified database facade.
        /// </summary>
        /// <param name="databaseFacade">The database facade.</param>
        public static void Seed(this DatabaseFacade databaseFacade)
        {
            databaseFacade.GetRelationalService<ISeeder>().Seed();
        }

        /// <summary>
        /// Gets the relational service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="databaseFacade">The database facade.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Relational Service not found</exception>
        private static TService GetRelationalService<TService>(this IInfrastructure<IServiceProvider> databaseFacade)
        {
            var service = databaseFacade.Instance.GetService<TService>();

            if (service == null)
            {
                throw new InvalidOperationException("Relational Service not found");
            }

            return service;
        }
    }
}
