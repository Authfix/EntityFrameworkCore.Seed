using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Daztane.EntityFrameworkCore.Seed.Models
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
        protected ILogger Logger;

        /// <summary>
        /// Sets the dependencies.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        internal void SetDependencies(ILogger logger, DbContext dbContext)
        {
            if (_dependenciesAlreadySet)
            {
                throw new Exception("Dependencies are already set for this seed");
            }

            Logger = logger;
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
        protected DbSet<T> GetDbSet<T>() where T : class, new()
        {
            return _dbContext.Set<T>();
        }
    }
}
