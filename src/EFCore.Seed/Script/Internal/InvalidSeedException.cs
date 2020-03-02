//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using System;

namespace Authfix.EntityFrameworkCore.Seed.Script.Internal
{
    internal class InvalidSeedException : Exception
    {
        /// <summary>
        /// Initialize <see cref="InvalidSeedException"/>
        /// </summary>
        public InvalidSeedException()
        {
        }

        /// <summary>
        /// Initialize <see cref="InvalidSeedException"/>
        /// </summary>
        /// <param name="message">The error message</param>
        public InvalidSeedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initialize <see cref="InvalidSeedException"/>
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="innerException">The exception</param>
        public InvalidSeedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
