//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Entities;
using System.Collections.Generic;

namespace Authfix.EntityFrameworkCore.Seed.Repositories
{
    public interface ISeedRepository
    {
        /// <summary>
        /// Gets a value indicating whether this instance is in memory.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is in memory; otherwise, <c>false</c>.
        /// </value>
        bool IsInMemory { get; }

        /// <summary>
        /// Check if the repository exists
        /// </summary>
        /// <returns></returns>
        bool Exists();

        /// <summary>
        /// Gets the create script.
        /// </summary>
        /// <returns></returns>
        string GetCreateScript();

        /// <summary>
        /// Gets the applied seeds.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<SeedRow> GetAppliedSeeds();

        /// <summary>
        /// Gets the insert script.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        string GetInsertScript(SeedRow row);
    }
}
