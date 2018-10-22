//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

namespace Authfix.EntityFrameworkCore.Seed.Script
{
    public interface ISeedConfiguration
    {
        /// <summary>
        /// Gets registered seed configuration
        /// </summary>
        /// <typeparam name="TConfiguration">The configuration type</typeparam>
        /// <returns></returns>
        TConfiguration Get<TConfiguration>();
    }
}
