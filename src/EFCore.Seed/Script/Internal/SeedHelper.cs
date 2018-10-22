//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Authfix.EntityFrameworkCore.Seed.Script.Internal
{
    internal class SeedHelper
    {
        /// <summary>
        /// Extract date from a seed name
        /// </summary>
        /// <param name="seedName">The seed name used to extract the date</param>
        /// <returns>The extract date</returns>
        public static DateTime ExtractDateFromSeedName(string seedName)
        {
            var dateTime = DateTime.ParseExact(seedName, "yyyyMMddHHmmss", null);

            return dateTime;
        }
    }
}
