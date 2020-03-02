//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Authfix.EntityFrameworkCore.Seed.Script.Internal
{
    internal class SeedInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeedInfo"/> class.
        /// </summary>
        /// <param name="seederAttribute">The seeder attribute.</param>
        public SeedInfo(Type concreteType, SeedAttribute seederAttribute)
        {
            ConcreteType = concreteType;
            SeederAttribute = seederAttribute;
        }

        /// <summary>
        /// Gets the type of the concrete.
        /// </summary>
        /// <value>
        /// The type of the concrete.
        /// </value>
        public Type ConcreteType { get; }

        /// <summary>
        /// Gets or sets the seeder attribute.
        /// </summary>
        /// <value>
        /// The seeder attribute.
        /// </value>
        public SeedAttribute SeederAttribute { get; }
    }
}
