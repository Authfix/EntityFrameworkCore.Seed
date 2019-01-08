using System;
using System.Collections.Generic;

namespace Authfix.EntityFrameworkCore.Seed.Infrastructure
{
    internal class DefaultServiceProvider : IServiceProvider, IUpdatableServiceProvider
    {
        private readonly IDictionary<string, object> _services;

        /// <summary>
        /// Initialize a <see cref="DefaultServiceProvider"/>
        /// </summary>
        public DefaultServiceProvider() : this(new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initialize a <see cref="DefaultServiceProvider"/>
        /// </summary>
        /// <param name="dictionary"></param>
        public DefaultServiceProvider(Dictionary<string, object> dictionary)
        {
            _services = dictionary;
        }

        /// <summary>
        /// Add a service to the service provider
        /// </summary>
        /// <param name="serviceType">The service type</param>
        /// <param name="serviceImplementation">The service implementation</param>
        public void AddService(string serviceType, object serviceImplementation)
        {
            _services.Add(serviceType, serviceImplementation);
        }

        /// <summary>
        /// Gets the service
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            return _services[serviceType.FullName];
        }
    }

    
    internal interface IUpdatableServiceProvider
    {
        /// <summary>
        /// Add a service to the service provider
        /// </summary>
        /// <param name="serviceType">The service type</param>
        /// <param name="serviceImplementation">The service implementation</param>
        void AddService(string serviceType, object serviceImplementation);
    }
}
