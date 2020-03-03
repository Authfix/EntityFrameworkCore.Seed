//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Infrastructure;
using Authfix.EntityFrameworkCore.Seed.SqlServer.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;

namespace Authfix.EntityFrameworkCore.Seed.SqlServer.Infrastructure
{
    public class SqlServerSeedDbContextOptionsBuilder : SeedDbContextOptionsBuilder<SqlServerSeedDbContextOptionsBuilder, SqlServerSeedOptionsExtension>
    {
        /// <summary>
        /// Initialize <see cref="SqlServerSeedDbContextOptionsBuilder"/>
        /// </summary>
        /// <param name="optionsBuilder">The existing default <see cref="DbContextOptionsBuilder"/></param>
        public SqlServerSeedDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) : base(optionsBuilder)
        {
        }
    }
}
