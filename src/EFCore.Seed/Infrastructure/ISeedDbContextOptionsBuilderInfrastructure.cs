//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Microsoft.EntityFrameworkCore;

namespace Authfix.EntityFrameworkCore.Seed.Infrastructure
{
    internal interface ISeedDbContextOptionsBuilderInfrastructure
    {
        /// <summary>
        /// Gets the <see cref="DbContextOptionsBuilder"/>
        /// </summary>
        DbContextOptionsBuilder OptionsBuilder { get; }
    }
}
