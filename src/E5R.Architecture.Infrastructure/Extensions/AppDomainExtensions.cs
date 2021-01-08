// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using E5R.Architecture.Core;
using E5R.Architecture.Infrastructure.Abstractions;

namespace E5R.Architecture.Infrastructure.Extensions
{
    public static class AppDomainExtensions
    {
        public static void DIRegistrar(this AppDomain appDomain, IDIContainer container)
        {
            Checker.NotNullArgument(appDomain, nameof(appDomain));
            Checker.NotNullArgument(container, nameof(container));

            appDomain.GetAssemblies()
                .Where(a => a.DefinedTypes.Any(t => t.ImplementedInterfaces.Contains(typeof(IDIRegistrar))))
                .ToList()
                .ForEach(a => a.DIRegistrar(container));
        }
    }
}
