using Daztane.EntityFrameworkCore.Seed.Entities;

using System.Collections.Generic;

namespace Daztane.EntityFrameworkCore.Seed.Repositories
{
    public interface ISeedRepository
    {
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
