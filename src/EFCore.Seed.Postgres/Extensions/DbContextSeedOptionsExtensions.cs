//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Authfix.EntityFrameworkCore.Seed.Postgres.Extensions
{
    public static class DbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// Uses the NPGSQL seed.
        /// </summary>
        /// <param name="dbContextOptionBuilder">The database context option builder.</param>
        /// <param name="seedAssembly">The seed assembly.</param>
        public static void UseNpgsqlSeed(this DbContextOptionsBuilder dbContextOptionBuilder, string seedAssembly)
        {
            var extension = GetOrCreateExtension(dbContextOptionBuilder, seedAssembly);
            ((IDbContextOptionsBuilderInfrastructure)dbContextOptionBuilder).AddOrUpdateExtension(extension);
        }

        /// <summary>
        /// Gets the or create extension.
        /// </summary>
        /// <param name="optionsBuilder">The options builder.</param>
        /// <param name="seedAssembly">The seed assembly.</param>
        /// <returns></returns>
        private static PostgresSeedDbContextOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder optionsBuilder, string seedAssembly) => optionsBuilder.Options.FindExtension<PostgresSeedDbContextOptionsExtension>() ?? new PostgresSeedDbContextOptionsExtension(seedAssembly);
    }
}
