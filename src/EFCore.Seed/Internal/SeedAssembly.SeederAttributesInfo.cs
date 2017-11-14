//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Attributes;
using System;

namespace Authfix.EntityFrameworkCore.Seed.Internal
{
    internal class SeederAttributesInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeederAttributesInfo"/> class.
        /// </summary>
        /// <param name="seederAttribute">The seeder attribute.</param>
        public SeederAttributesInfo(Type concreteType, SeedAttribute seederAttribute)
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
        public SeedAttribute SeederAttribute { get; private set; }
    }
}