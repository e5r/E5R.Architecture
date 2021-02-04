// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using E5R.Architecture.Core;
using E5R.Architecture.Infrastructure.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace E5R.Architecture.Infrastructure.Extensions
{
    public static class AppDomainExtensions
    {
        public static IEnumerable<Assembly> GetNonSystemAssemblies(this AppDomain appDomain) =>
            appDomain.GetAssemblies().Where(a =>
            {
                var name = a.GetName().Name;

                return !name.StartsWith(nameof(System)) && !new[] {"netstandard"}.Contains(name);
            });

        public static void DIRegistrar(this AppDomain appDomain, IDIContainer container)
        {
            Checker.NotNullArgument(appDomain, nameof(appDomain));
            Checker.NotNullArgument(container, nameof(container));

            appDomain.GetAssemblies()
                .Where(a => a.DefinedTypes.Any(t => t.ImplementedInterfaces.Contains(typeof(IDIRegistrar))))
                .ToList()
                .ForEach(a => a.DIRegistrar(container));
        }
        
        public static void AddAllRules(this AppDomain appDomain, IServiceCollection services)
        {
            Checker.NotNullArgument(appDomain, nameof(appDomain));
            Checker.NotNullArgument(services, nameof(services));

            appDomain
                .GetNonSystemAssemblies()
                .ToList()
                .ForEach(a => a.AddAllRules(services));
        }
        
        public static void AddLazyGroups(this AppDomain appDomain, IServiceCollection services)
        {
            Checker.NotNullArgument(appDomain, nameof(appDomain));
            Checker.NotNullArgument(services, nameof(services));

            appDomain
                .GetNonSystemAssemblies()
                .ToList()
                .ForEach(a => a.AddLazyGroups(services));
        }
    }
}
