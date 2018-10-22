//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Postgres.Infrastructure;
using Authfix.EntityFrameworkCore.Seed.Postgres.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Authfix.EntityFrameworkCore.Seed.Postgres.Extensions
{
    public static class PostgresSeedDbContextOptionsExtension
    {
        /// <summary>
        /// Specify usage of the in memory seed
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <param name="seedAssemblyName"></param>
        /// <param name="inMemorySeedOptionsAction"></param>
        /// <returns></returns>
        public static DbContextOptionsBuilder UseNpgsqlSeed(this DbContextOptionsBuilder optionsBuilder, string seedAssemblyName, Action<PostgresSeedDbContextOptionsBuilder> postgresSeedOptionsAction = null)
        {
            var extension = (PostgresSeedOptionsExtension)GetOrCreateExtension(optionsBuilder).WithSeedAssemblyName(seedAssemblyName);
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            postgresSeedOptionsAction?.Invoke(new PostgresSeedDbContextOptionsBuilder(optionsBuilder));

            return optionsBuilder;
        }

        /// <summary>
        /// Returns an existing instance of <see cref="InMemorySeedOptionsExtension"/>, or a new instance if one does not exist.
        /// </summary>
        /// <param name="optionsBuilder">The <see cref="DbContextOptionsBuilder"/> to search.</param>
        /// <returns>
        /// An existing instance of <see cref="InMemorySeedOptionsExtension"/>, or a new instance if one does not exist.
        /// </returns>
        private static PostgresSeedOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.Options.FindExtension<PostgresSeedOptionsExtension>() is PostgresSeedOptionsExtension existing
                ? new PostgresSeedOptionsExtension(existing)
                : new PostgresSeedOptionsExtension();
    }
}
