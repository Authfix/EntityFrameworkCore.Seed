//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Attributes;
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
        public IEnumerable<SeederAttributesInfo> GetAvailableSeeds()
        {
            var availableSeeds = new Dictionary<Type, SeederAttributesInfo>();

            foreach (var type in Assembly.DefinedTypes)
            {
                var seederAttribute = type.GetCustomAttribute<SeedAttribute>(true);

                if (seederAttribute == null)
                {
                    continue;
                }

                var seederAttributeInfo = new SeederAttributesInfo(type, seederAttribute);

                yield return seederAttributeInfo;
            }
        }
    }
}
