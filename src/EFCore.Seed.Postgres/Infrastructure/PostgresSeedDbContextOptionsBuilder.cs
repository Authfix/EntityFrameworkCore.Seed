//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Infrastructure;
using Authfix.EntityFrameworkCore.Seed.Postgres.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;

namespace Authfix.EntityFrameworkCore.Seed.Postgres.Infrastructure
{
    public class PostgresSeedDbContextOptionsBuilder : SeedDbContextOptionsBuilder<PostgresSeedDbContextOptionsBuilder, PostgresSeedOptionsExtension>
    {
        /// <summary>
        /// Initialize <see cref="PostgresSeedDbContextOptionsBuilder"/>
        /// </summary>
        /// <param name="optionsBuilder">The existing default <see cref="DbContextOptionsBuilder"/></param>
        public PostgresSeedDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) : base(optionsBuilder)
        {
        }
    }
}
