//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.InMemory.Infrastructure;
using Authfix.EntityFrameworkCore.Seed.InMemory.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Authfix.EntityFrameworkCore.Seed.InMemory.Extensions
{
    public static class InMemorySeedDbContextOptionsExtension
    {
        /// <summary>
        /// Specify usage of the in memory seed
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <param name="seedAssemblyName"></param>
        /// <param name="inMemorySeedOptionsAction"></param>
        /// <returns></returns>
        public static DbContextOptionsBuilder UseInMemorySeed(this DbContextOptionsBuilder optionsBuilder, string seedAssemblyName, Action<InMemorySeedDbContextOptionsBuilder> inMemorySeedOptionsAction = null)
        {
            var extension = (InMemorySeedOptionsExtension)GetOrCreateExtension(optionsBuilder).WithSeedAssemblyName(seedAssemblyName);
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            inMemorySeedOptionsAction?.Invoke(new InMemorySeedDbContextOptionsBuilder(optionsBuilder));

            return optionsBuilder;
        }

        /// <summary>
        /// Returns an existing instance of <see cref="InMemorySeedOptionsExtension"/>, or a new instance if one does not exist.
        /// </summary>
        /// <param name="optionsBuilder">The <see cref="DbContextOptionsBuilder"/> to search.</param>
        /// <returns>
        /// An existing instance of <see cref="InMemorySeedOptionsExtension"/>, or a new instance if one does not exist.
        /// </returns>
        private static InMemorySeedOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.Options.FindExtension<InMemorySeedOptionsExtension>() is InMemorySeedOptionsExtension existing
                ? new InMemorySeedOptionsExtension(existing)
                : new InMemorySeedOptionsExtension();
    }
}
