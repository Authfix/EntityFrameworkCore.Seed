namespace Authfix.EntityFrameworkCore.Seed.Configuration
{
    public class SeedConfiguration
    {
        /// <summary>
        /// Initialize a <see cref="SeedConfiguration"/>
        /// </summary>
        /// <param name="isInMemory">Value indicating if the seed is used with an in memory provider</param>
        public SeedConfiguration(bool isInMemory = false)
        {
            IsInMemory = isInMemory;
        }

        /// <summary>
        /// Gets value indicating if the seed used an in memory provider
        /// </summary>
        public bool IsInMemory { get; }
    }
}
