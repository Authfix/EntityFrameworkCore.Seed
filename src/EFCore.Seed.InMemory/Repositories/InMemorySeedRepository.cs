//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using System.Collections.Generic;
using Authfix.EntityFrameworkCore.Seed.Entities;
using Authfix.EntityFrameworkCore.Seed.Repositories;

namespace Authfix.EntityFrameworkCore.Seed.InMemory.Repositories
{
    internal class InMemorySeedRepository : ISeedRepository
    {
        /// <summary>
        /// Gets a value indicating whether this instance is in memory.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is in memory; otherwise, <c>false</c>.
        /// </value>
        public bool IsInMemory => true;

        /// <summary>
        /// Check if the repository exists
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            return true;
        }

        /// <summary>
        /// Gets the applied seeds.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<SeedRow> GetAppliedSeeds()
        {
            return new List<SeedRow>();
        }

        /// <summary>
        /// Gets the create script.
        /// </summary>
        /// <returns></returns>
        public string GetCreateScript()
        {
            return string.Empty;
        }

        /// <summary>
        /// Gets the insert script.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public string GetInsertScript(SeedRow row)
        {
            return string.Empty;
        }
    }
}
