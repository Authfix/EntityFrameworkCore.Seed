//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Authfix.EntityFrameworkCore.Seed.Models
{
    public abstract class SeederBase
    {
        /// <summary>
        /// The data context already set
        /// </summary>
        private bool _dependenciesAlreadySet;

        /// <summary>
        /// The database context
        /// </summary>
        private DbContext _dbContext;

        /// <summary>
        /// The logger
        /// </summary>
        protected ILogger Logger { get; private set; }

        /// <summary>
        /// Gets the current seed configuration
        /// </summary>
        protected SeedConfiguration Configuration { get; private set; }

        /// <summary>
        /// Sets the dependencies.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        internal void SetDependencies(ILogger logger, DbContext dbContext, SeedConfiguration seedConfiguration)
        {
            if (_dependenciesAlreadySet)
            {
                throw new Exception("Dependencies are already set for this seed");
            }

            Logger = logger;
            Configuration = seedConfiguration;
            _dbContext = dbContext;

            _dependenciesAlreadySet = true;
        }

        /// <summary>
        /// Seeds the data.
        /// </summary>
        public void SeedData()
        {
            InternalSeed();

            UpdateEntities();

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Updates the entities.
        /// </summary>
        protected abstract void UpdateEntities();

        /// <summary>
        /// Values the tuple.
        /// </summary>
        /// <returns></returns>
        internal virtual void InternalSeed()
        {
        }
        
        /// <summary>
        /// Gets the database set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected DbSet<T> GetDbSet<T>() where T : class
        {
            return _dbContext.Set<T>();
        }
    }
}
