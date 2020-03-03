//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.SqlServer.Infrastructure;
using Authfix.EntityFrameworkCore.Seed.SqlServer.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Authfix.EntityFrameworkCore.Seed.SqlServer.Extensions
{
    public static class SqlServerSeedDbContextOptionsExtension
    {
        // <summary>
        /// Specify usage of the in sql server seed
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <param name="seedAssemblyName"></param>
        /// <param name="inMemorySeedOptionsAction"></param>
        /// <returns></returns>
        public static DbContextOptionsBuilder UseNpgsqlSeed(this DbContextOptionsBuilder optionsBuilder, string seedAssemblyName, Action<SqlServerSeedDbContextOptionsBuilder> sqlServerSeedOptionsAction = null)
        {
            var extension = (SqlServerSeedOptionsExtension)GetOrCreateExtension(optionsBuilder).WithSeedAssemblyName(seedAssemblyName);
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            sqlServerSeedOptionsAction?.Invoke(new SqlServerSeedDbContextOptionsBuilder(optionsBuilder));

            return optionsBuilder;
        }

        /// <summary>
        /// Returns an existing instance of <see cref="SqlServerSeedOptionsExtension"/>, or a new instance if one does not exist.
        /// </summary>
        /// <param name="optionsBuilder">The <see cref="DbContextOptionsBuilder"/> to search.</param>
        /// <returns>
        /// An existing instance of <see cref="SqlServerSeedOptionsExtension"/>, or a new instance if one does not exist.
        /// </returns>
        private static SqlServerSeedOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.Options.FindExtension<SqlServerSeedOptionsExtension>() is SqlServerSeedOptionsExtension existing
                ? new SqlServerSeedOptionsExtension(existing)
                : new SqlServerSeedOptionsExtension();
    }
}
