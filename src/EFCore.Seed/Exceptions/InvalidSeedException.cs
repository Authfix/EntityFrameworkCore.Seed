using System;
using System.Runtime.Serialization;

namespace Authfix.EntityFrameworkCore.Seed.Exceptions
{
    internal class InvalidSeedException : Exception
    {
        public InvalidSeedException()
        {
        }

        public InvalidSeedException(string message) : base(message)
        {
        }

        public InvalidSeedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidSeedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
