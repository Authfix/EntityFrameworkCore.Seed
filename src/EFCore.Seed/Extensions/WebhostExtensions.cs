﻿//  
// Copyright (c) Thomas Bailly. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.  
//

using Authfix.EntityFrameworkCore.Seed.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Authfix.EntityFrameworkCore.Seed.Extensions
{
    public static class WebHostExtensions
    {
        /// <summary>
        /// Seeds the data for a specific <see cref="DbContext"/>.
        /// </summary>
        /// <param name="webHost">The web host.</param>
        public static IWebHost SeedData<T>(this IWebHost webHost) where T : DbContext
        {
            using (var serviceScope = webHost.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var appContext = serviceScope.ServiceProvider.GetService<T>();

                appContext.Database.Seed();
            }

            return webHost;
        }
    }
}
