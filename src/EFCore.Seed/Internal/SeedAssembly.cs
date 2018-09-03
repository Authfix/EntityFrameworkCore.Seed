//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Attributes;
using Authfix.EntityFrameworkCore.Seed.Exceptions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Authfix.EntityFrameworkCore.Seed.Internal
{
    internal partial class SeedAssembly : ISeedAssembly
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeedAssembly"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public SeedAssembly(string assembly)
        {
            Assembly = Assembly.Load(new AssemblyName(assembly));
        }

        /// <summary>
        /// Gets the assembly.
        /// </summary>
        /// <value>
        /// The assembly.
        /// </value>
        public Assembly Assembly { get; private set; }
        
        /// <summary>
        /// Gets the available seeds.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public IEnumerable<SeederAttributesInfo> GetAvailableSeeds(string dbContextType)
        {
            var availableSeeds = new Dictionary<Type, SeederAttributesInfo>();

            CheckSeedClassesIntegrity();

            foreach (var type in Assembly.DefinedTypes)
            {
                var seederAttribute = type.GetCustomAttribute<SeedAttribute>(true);

                if (seederAttribute == null)
                {
                    continue;
                }

                var dbContextAttribute = type.GetCustomAttribute<DbContextAttribute>(true);

                if(dbContextAttribute != null && dbContextAttribute.ContextType.Name != dbContextType)
                {
                    continue;
                }

                var seederAttributeInfo = new SeederAttributesInfo(type, seederAttribute);

                yield return seederAttributeInfo;
            }
        }

        /// <summary>
        /// Check seed classes integrity (if we use dbcontext attribute, all seeds need to have DbContextAttributes
        /// </summary>
        private void CheckSeedClassesIntegrity()
        {
            int seeds = 0;
            int seedsWithDbContextAttribute = 0;

            foreach (var type in Assembly.DefinedTypes)
            {
                var seederAttribute = type.GetCustomAttribute<SeedAttribute>(true);

                if (seederAttribute == null)
                {
                    continue;
                }

                seeds++;

                var dbContextAttribute = type.GetCustomAttribute<DbContextAttribute>(true);

                if (dbContextAttribute != null)
                {
                    seedsWithDbContextAttribute++;
                }

                ValidateSeedsIntegrity(seeds, seedsWithDbContextAttribute);
            }
        }

        private void ValidateSeedsIntegrity(int seeds, int seedsWithDbContextAttribute)
        {
            if (seedsWithDbContextAttribute == 0)
            {
                return;
            }

            if (seedsWithDbContextAttribute != seeds)
            {
                throw new InvalidSeedException("If you have at least one DbContext attribute on your seed class, all you seed class need to have the DbContext attribute");
            }
        }
    }
}
