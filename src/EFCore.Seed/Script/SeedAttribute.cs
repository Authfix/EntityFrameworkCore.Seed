//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Script.Internal;
using System;
using System.Text.RegularExpressions;

namespace Authfix.EntityFrameworkCore.Seed.Script
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SeedAttribute : Attribute
    {
        /// <summary>
        /// The date pattern
        /// </summary>
        private const string DatePattern = @"(^[0-9]{14})";

        /// <summary>
        /// The separator pattern
        /// </summary>
        private const string SeparatorPattern = "(_)";

        /// <summary>
        /// The name pattern
        /// </summary>
        private const string NamePattern = "((?:[a-z][a-z]+))";

        /// <summary>
        /// The seed regex
        /// </summary>
        private readonly Regex _seedRegex;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeederAttribute"/> class.
        /// </summary>
        /// <param name="seedName">Name of the seed.</param>
        /// <exception cref="System.FormatException">The seed name must have the format {yyyyMMddHHmmss}_{seed name} like 20170101000000_Whatever</exception>
        public SeedAttribute(string seedName)
        {
            _seedRegex = new Regex(DatePattern + SeparatorPattern + NamePattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            if (!IsSeedNameValid(seedName))
            {
                throw new FormatException("The seed name must have the format {yyyyMMddHHmmss}_{seed name} like 20170101000000_Whatever");
            }

            SeedName = seedName;
        }

        /// <summary>
        /// Gets the name of the seed.
        /// </summary>
        /// <value>
        /// The name of the seed.
        /// </value>
        public string SeedName { get; }

        /// <summary>
        /// Gets the seed date.
        /// </summary>
        /// <value>
        /// The seed date.
        /// </value>
        public DateTime SeedDate
        {
            get
            {
                var match = _seedRegex.Match(SeedName);

                return SeedHelper.ExtractDateFromSeedName(match.Groups[1].Value);
            }
        }

        /// <summary>
        /// Determines whether [is seed name valid] [the specified seed name].
        /// </summary>
        /// <param name="seedName">Name of the seed.</param>
        /// <returns>
        ///   <c>true</c> if [is seed name valid] [the specified seed name]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsSeedNameValid(string seedName)
        {
            var match = _seedRegex.Match(seedName);

            if (match.Success)
            {
                var dateTime = SeedHelper.ExtractDateFromSeedName(match.Groups[1].Value);
            }

            return match.Success;
        }
    }
}
