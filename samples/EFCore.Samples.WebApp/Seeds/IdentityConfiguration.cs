using System;

namespace EFCore.Samples.WebApp.Seeds
{
    public class IdentityConfiguration
    {
        public IdentityConfiguration(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
