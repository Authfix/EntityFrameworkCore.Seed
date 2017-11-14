//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

namespace Authfix.EntityFrameworkCore.Seed
{
    public interface ISeeder
    {
        /// <summary>
        /// Seeds data.
        /// </summary>
        void Seed();
    }
}
