//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Entities;
using Authfix.EntityFrameworkCore.Seed.Repositories;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using System.Collections.Generic;
using System.Linq;

namespace Authfix.EntityFrameworkCore.Seed.Postgres.Repositories
{
    internal class PostgresSeedRepository : NpgsqlHistoryRepository, ISeedRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgresSeedRepository"/> class.
        /// </summary>
        /// <param name="historyRepositoryDependencies">The history repository dependencies.</param>
        public PostgresSeedRepository(HistoryRepositoryDependencies historyRepositoryDependencies) : base(historyRepositoryDependencies)
        {
        }

        /// <summary>
        /// Gets a value indicating whether this instance is in memory.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is in memory; otherwise, <c>false</c>.
        /// </value>
        public bool IsInMemory => false;

        /// <summary>
        /// Gets the table name
        /// </summary>
        protected override string TableName => Constants.DefaultTableName;

        /// <summary>
        /// Gets the MigrationId column name
        /// </summary>
        protected override string MigrationIdColumnName => Constants.DefaultSeedIdColumnName;

        /// <summary>
        /// Gets the applied seeds.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<SeedRow> GetAppliedSeeds()
        {
            var seeds = base.GetAppliedMigrations();

            return seeds.Select(m => new SeedRow(m.MigrationId, m.ProductVersion)).ToList();
        }

        /// <summary>
        /// Gets the create script.
        /// </summary>
        /// <returns></returns>
        public override string GetCreateScript()
        {
            var script = base.GetCreateScript();

            // workaround because create script doesn't use the MigrationidColumnName
            script = script.Replace("MigrationId", MigrationIdColumnName);

            return script;
        }

        /// <summary>
        /// Gets the insert script.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public string GetInsertScript(SeedRow row)
        {
            var insertScript = base.GetInsertScript(new HistoryRow(row.SeedId, row.ProductVersion));

            return insertScript;
        }
    }
}
