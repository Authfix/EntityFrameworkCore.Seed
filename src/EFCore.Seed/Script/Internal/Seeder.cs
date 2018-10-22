//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authfix.EntityFrameworkCore.Seed.Script.Internal
{
    internal class Seeder : ISeeder
    {
        /// <summary>
        /// The seed repository
        /// </summary>
        protected readonly ISeedRepository _seedRepository;

        /// <summary>
        /// The seed assembly
        /// </summary>
        protected readonly ISeedAssembly _seedAssembly;

        /// <summary>
        /// The current db context
        /// </summary>
        protected readonly ICurrentDbContext _currentDbContext;

        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// The seed configuration
        /// </summary>
        protected readonly ISeedConfiguration SeedConfiguration;

        /// <summary>
        /// Initialize <see cref="Seeder"/>
        /// </summary>
        /// <param name="options">The database context options</param>
        public Seeder(ICurrentDbContext currentDbContext, ISeedRepository seedRepository, ISeedAssembly seedAssembly, ILoggerFactory loggerFactory, IDbContextOptions dbContextOptions)
        {
            var seedOptions = (SeedOptionsExtension)dbContextOptions.Extensions.Where(o => o.GetType().IsSubclassOf(typeof(SeedOptionsExtension))).FirstOrDefault();
            SeedConfiguration = seedOptions.SeedConfiguration;

            _logger = loggerFactory.CreateLogger<Seeder>();
            _currentDbContext = currentDbContext;
            _seedRepository = seedRepository;
            _seedAssembly = seedAssembly;
        }

        /// <summary>
        /// Seeds data.
        /// </summary>
        public virtual void Seed()
        {
            var appliedSeeds = _seedRepository.GetAppliedSeeds();

            var contextType = _currentDbContext.Context.GetType();

            var availableSeeds = _seedAssembly.GetAvailableSeeds(contextType.Name);

            ApplySeeds(availableSeeds, appliedSeeds);
        }

        /// <summary>
        /// Applies the seeds.
        /// </summary>
        /// <param name="availableSeeds">The available seeds.</param>
        /// <param name="appliedSeeds">The applied seeds.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void ApplySeeds(IEnumerable<SeedInfo> availableSeeds, IReadOnlyList<SeedRow> appliedSeeds)
        {
            foreach (var availableSeed in availableSeeds)
            {
                var isAlreadyApplied = appliedSeeds.FirstOrDefault(s => s.SeedId == availableSeed.SeederAttribute.SeedName) != null;

                if (!isAlreadyApplied)
                {
                    RunSeed(availableSeed);
                }
            }
        }

        /// <summary>
        /// Runs the seed.
        /// </summary>
        /// <param name="availableSeed">The available seed.</param>
        protected virtual void RunSeed(SeedInfo availableSeed)
        {
            var concreteClass = Activator.CreateInstance(availableSeed.ConcreteType, _currentDbContext.Context, _logger, SeedConfiguration) as SeedBase;

            concreteClass.SeedData();
        }
    }
}
