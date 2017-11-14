//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using System.Collections.Generic;
using System.Reflection;

namespace Authfix.EntityFrameworkCore.Seed.Internal
{
    internal interface ISeedAssembly
    {
        /// <summary>
        /// Gets the assembly.
        /// </summary>
        /// <value>
        /// The assembly.
        /// </value>
        Assembly Assembly { get; }

        /// <summary>
        /// Gets the available seeds.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        IEnumerable<SeederAttributesInfo> GetAvailableSeeds();
    }
}
