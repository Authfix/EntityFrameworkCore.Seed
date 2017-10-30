using System.Collections.Generic;
using System.Reflection;

namespace Daztane.EntityFrameworkCore.Seed.Internal
{
    public interface ISeedAssembly
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
