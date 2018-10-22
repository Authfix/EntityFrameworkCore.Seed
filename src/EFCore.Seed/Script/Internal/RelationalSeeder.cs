//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Authfix.EntityFrameworkCore.Seed.Script.Internal
{
    internal class RelationalSeeder : Seeder
    {
        /// <summary>
        /// Gets the seed dependencies
        /// </summary>
        private readonly HistoryRepositoryDependencies _seedDependencies;

        /// <summary>
        /// Initialize a relational seeder
        /// </summary>
        /// <param name="currentDbContext">The current db context</param>
        /// <param name="seedRepository">The seed repository</param>
        /// <param name="seedAssembly">The seed assembly</param>
        /// <param name="seedDependencies">The relational seed dependencies</param>
        public RelationalSeeder(ICurrentDbContext currentDbContext, ISeedRepository seedRepository, ISeedAssembly seedAssembly, ILoggerFactory loggerFactory, HistoryRepositoryDependencies seedDependencies) : base(currentDbContext, seedRepository, seedAssembly, loggerFactory, seedDependencies.Options)
        {
            _seedDependencies = seedDependencies;
        }

        /// <summary>
        /// Seeds data.
        /// </summary>
        public override void Seed()
        {
            if (!_seedRepository.Exists())
            {
                _seedDependencies.DatabaseCreator.EnsureCreated();

                var createScript = _seedRepository.GetCreateScript();

                ExecuteQuery(createScript);
            }

            base.Seed();
        }

        /// <summary>
        /// Runs the seed.
        /// </summary>
        /// <param name="availableSeed">The available seed.</param>
        protected override void RunSeed(SeedInfo availableSeed)
        {
            base.RunSeed(availableSeed);

            var insertScript = _seedRepository.GetInsertScript(new SeedRow(availableSeed.SeederAttribute.SeedName, "1.0"));

            ExecuteQuery(insertScript);
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="scriptToExecute">The script to execute.</param>
        private void ExecuteQuery(string scriptToExecute)
        {
            var sqlCommandBuilder = _seedDependencies.RawSqlCommandBuilder;
            var connection = _seedDependencies.Connection;

            var command = sqlCommandBuilder.Build(scriptToExecute);

            command.ExecuteNonQuery(connection);
        }
    }
}
