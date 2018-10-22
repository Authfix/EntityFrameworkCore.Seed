//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using System.Collections.Generic;

namespace Authfix.EntityFrameworkCore.Seed.Script.Internal
{
    internal class SeedConfiguration : Dictionary<string, object>, ISeedConfiguration
    {
        public SeedConfiguration(Dictionary<string, object> parameters) : base(parameters)
        {

        }

        public TConfiguration Get<TConfiguration>()
        {
            var confType = typeof(TConfiguration).FullName;

            if (ContainsKey(confType))
            {
                return (TConfiguration)this[confType];
            }

            return default(TConfiguration);
        }
    }
}
