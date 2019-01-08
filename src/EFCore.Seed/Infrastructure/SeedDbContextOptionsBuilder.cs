//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Authfix.EntityFrameworkCore.Seed.Infrastructure
{
    public abstract class SeedDbContextOptionsBuilder<TBuilder, TExtension> : ISeedDbContextOptionsBuilderInfrastructure
        where TBuilder : SeedDbContextOptionsBuilder<TBuilder, TExtension>
        where TExtension : SeedOptionsExtension, new()
    {
        /// <summary>
        /// Initialize a <see cref="SeedDbContextOptionsBuilder{TBuilder, TExtension}"/>
        /// </summary>
        /// <param name="optionsBuilder">The existing <see cref="DbContextOptionsBuilder"/></param>
        protected SeedDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder)
        {
            OptionsBuilder = optionsBuilder ?? throw new ArgumentNullException(nameof(optionsBuilder));
        }

        /// <summary>
        /// Gets the core options builder
        /// </summary>
        protected DbContextOptionsBuilder OptionsBuilder { get; }

        DbContextOptionsBuilder ISeedDbContextOptionsBuilderInfrastructure.OptionsBuilder => OptionsBuilder;

        /// <summary>
        /// Add parameter used for seed to the database options
        /// </summary>
        /// <typeparam name="TParameter">The parameter type.</typeparam>
        /// <param name="parameter">The parameter to add</param>
        /// <returns>The same builder for chaining purpose.</returns>
        public virtual TBuilder AddParameter<TParameter>(TParameter parameter)
            => WithOption(e => (TExtension)e.WithParameter(parameter));

        /// <summary>
        /// Add parameter used for seed to the database options
        /// </summary>
        /// <typeparam name="TParameter">The parameter type.</typeparam>
        /// <param name="parameter">The parameter to add</param>
        /// <returns>The same builder for chaining purpose.</returns>
        public virtual TBuilder AddParameter<TParameter>(Func<TParameter> parameter)
            => WithOption(e => (TExtension)e.WithParameter(parameter));

        /// <summary>
        /// Sets an option by cloning the extension used to store the settings. This ensures the builder
        /// does not modify options that are already in use elsewhere.
        /// </summary>
        /// <param name="setAction"> An action to set the option. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        protected virtual TBuilder WithOption(Func<TExtension, TExtension> setAction)
        {
            ((IDbContextOptionsBuilderInfrastructure)OptionsBuilder).AddOrUpdateExtension(setAction(OptionsBuilder.Options.FindExtension<TExtension>() ?? new TExtension()));

            return (TBuilder)this;
        }
    }
}
