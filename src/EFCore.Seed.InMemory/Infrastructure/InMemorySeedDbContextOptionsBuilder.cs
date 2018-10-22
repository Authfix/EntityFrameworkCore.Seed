//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Infrastructure;
using Authfix.EntityFrameworkCore.Seed.InMemory.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authfix.EntityFrameworkCore.Seed.InMemory.Infrastructure
{
    public class InMemorySeedDbContextOptionsBuilder : SeedDbContextOptionsBuilder<InMemorySeedDbContextOptionsBuilder, InMemorySeedOptionsExtension>
    {
        /// <summary>
        /// Initialize <see cref="InMemorySeedDbContextOptionsBuilder"/>
        /// </summary>
        /// <param name="optionsBuilder">The existing default <see cref="DbContextOptionsBuilder"/></param>
        public InMemorySeedDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) : base(optionsBuilder)
        {
        }
    }
}
