//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

namespace Authfix.EntityFrameworkCore.Seed.Script
{
    public class SeedRow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeedRow"/> class.
        /// </summary>
        /// <param name="seedId">The seed identifier.</param>
        /// <param name="productVersion">The product version.</param>
        public SeedRow(string seedId, string productVersion)
        {
            SeedId = seedId;
            ProductVersion = productVersion;
        }

        /// <summary>
        /// Gets the seed identifier.
        /// </summary>
        /// <value>
        /// The seed identifier.
        /// </value>
        public virtual string SeedId { get; }

        /// <summary>
        /// Gets the product version.
        /// </summary>
        /// <value>
        /// The product version.
        /// </value>
        public virtual string ProductVersion { get; }

    }
}
