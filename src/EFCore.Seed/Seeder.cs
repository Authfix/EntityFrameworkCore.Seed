using Authfix.EntityFrameworkCore.Seed.Entities;
using Authfix.EntityFrameworkCore.Seed.Internal;
using Authfix.EntityFrameworkCore.Seed.Models;
using Authfix.EntityFrameworkCore.Seed.Repositories;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Authfix.EntityFrameworkCore.Seed
{
    public class Seeder : ISeeder
    {
        /// <summary>
        /// The seed repository
        /// </summary>
        private readonly ISeedRepository _seedRepository;

        /// <summary>
        /// The database creator
        /// </summary>
        private readonly IDatabaseCreator _databaseCreator;
        
        /// <summary>
        /// The seed assembly
        /// </summary>
        private readonly ISeedAssembly _seedAssembly;

        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider _serviceProvider;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Seeder"/> class.
        /// </summary>
        /// <param name="seedRepository">The seed repository.</param>
        /// <param name="databaseCreator">The database creator.</param>
        public Seeder(ISeedRepository seedRepository, IDatabaseCreator databaseCreator, ISeedAssembly seedAssembly, IServiceProvider serviceProvider)
        {
            _seedRepository = seedRepository;
            _databaseCreator = databaseCreator;
            _seedAssembly = seedAssembly;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Seeds data.
        /// </summary>
        public void Seed()
        {
            if (!_seedRepository.Exists())
            {
                _databaseCreator.EnsureCreated();

                if (!_seedRepository.IsInMemory)
                {
                    var createScript = _seedRepository.GetCreateScript();

                    ExecuteQuery(createScript);
                }
            }

            var appliedSeeds = _seedRepository.GetAppliedSeeds();

            var availableSeeds = _seedAssembly.GetAvailableSeeds();

            ApplySeeds(availableSeeds, appliedSeeds);
        }

        /// <summary>
        /// Applies the seeds.
        /// </summary>
        /// <param name="availableSeeds">The available seeds.</param>
        /// <param name="appliedSeeds">The applied seeds.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void ApplySeeds(IEnumerable<SeederAttributesInfo> availableSeeds, IReadOnlyList<SeedRow> appliedSeeds)
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
        private void RunSeed(SeederAttributesInfo availableSeed)
        {
            var currentContext = _serviceProvider.GetService<ICurrentDbContext>();
            var logger = _serviceProvider.GetService<ILogger>();

            var concreteClass = Activator.CreateInstance(availableSeed.ConcreteType) as SeederBase;
            concreteClass.SetDependencies(logger, currentContext.Context);

            concreteClass.SeedData();

            if (!_seedRepository.IsInMemory)
            {
                var insertScript = _seedRepository.GetInsertScript(new SeedRow(availableSeed.SeederAttribute.SeedName, "1.0"));

                ExecuteQuery(insertScript);
            }
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="scriptToExecute">The script to execute.</param>
        private void ExecuteQuery(string scriptToExecute)
        {
            var sqlCommandBuilder = _serviceProvider.GetService<IRawSqlCommandBuilder>();
            var connection = _serviceProvider.GetService<IRelationalConnection>();

            var command = sqlCommandBuilder.Build(scriptToExecute);

            command.ExecuteNonQuery(connection);
        }
    }
}
