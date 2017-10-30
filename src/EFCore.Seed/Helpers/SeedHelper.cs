using System;

namespace Daztane.EntityFrameworkCore.Seed.Helpers
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
