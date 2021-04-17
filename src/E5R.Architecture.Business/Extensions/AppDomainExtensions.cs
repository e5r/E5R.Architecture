// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using E5R.Architecture.Core;
using E5R.Architecture.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace E5R.Architecture.Business.Extensions
{
    public static class AppDomainExtensions
    {
        public static void AddAllBusinessFeatures(this AppDomain appDomain, IServiceCollection services)
        {
            Checker.NotNullArgument(appDomain, nameof(appDomain));
            Checker.NotNullArgument(services, nameof(services));

            appDomain
                .GetNonSystemAssemblies()
                .ToList()
                .ForEach(a => a.AddAllBusinessFeatures(services));
        }
    }
}
