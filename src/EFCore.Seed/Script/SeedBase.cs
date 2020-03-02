//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authfix.EntityFrameworkCore.Seed.Script
{
    public abstract class SeedBase
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// Initialize <see cref="SeedBase"/>
        /// </summary>
        /// <param name="dbContext">The database context</param>
        /// <param name="logger">The logger</param>
        public SeedBase(DbContext dbContext, ILogger logger, ISeedConfiguration seedConfiguration)
        {
            Logger = logger;
            SeedConfiguration = seedConfiguration;
            _dbContext = dbContext;
        }

        /// <summary>
        /// The logger
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the seed configuration
        /// </summary>
        protected ISeedConfiguration SeedConfiguration { get; }

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
        protected DbSet<T> DbSet<T>() where T : class
        {
            return _dbContext.Set<T>();
        }
    }
}
