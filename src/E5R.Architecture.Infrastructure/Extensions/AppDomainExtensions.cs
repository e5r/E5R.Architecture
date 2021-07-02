// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using E5R.Architecture.Core;
using E5R.Architecture.Infrastructure.Abstractions;
using Microsoft.Extensions.Configuration;
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

        public static void DIRegistrar(this AppDomain appDomain, IServiceCollection services, IConfiguration configuration)
        {
            Checker.NotNullArgument(appDomain, nameof(appDomain));
            Checker.NotNullArgument(services, nameof(services));
            Checker.NotNullArgument(configuration, nameof(configuration));

            appDomain.GetAssemblies()
                .Where(a =>
                    a.DefinedTypes.Any(t => t.ImplementedInterfaces.Contains(typeof(ICrossCuttingRegistrar))))
                .ToList()
                .ForEach(a => a.DIRegistrar(services, configuration));
        }

        public static void AddAllNotificationDispatchers(this AppDomain appDomain,
            IServiceCollection services)
        {
            Checker.NotNullArgument(appDomain, nameof(appDomain));
            Checker.NotNullArgument(services, nameof(services));

            appDomain
                .GetNonSystemAssemblies()
                .ToList()
                .ForEach(a => a.AddAllNotificationDispatchers(services));
        }

        public static void AddAllTransformers(this AppDomain appDomain, IServiceCollection services)
        {
            Checker.NotNullArgument(appDomain, nameof(appDomain));
            Checker.NotNullArgument(services, nameof(services));

            appDomain
                .GetNonSystemAssemblies()
                .ToList()
                .ForEach(a => a.AddAllTransformers(services));
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

        public static void AddAllLazyGroups(this AppDomain appDomain, IServiceCollection services)
        {
            Checker.NotNullArgument(appDomain, nameof(appDomain));
            Checker.NotNullArgument(services, nameof(services));

            appDomain
                .GetNonSystemAssemblies()
                .ToList()
                .ForEach(a => a.AddAllLazyGroups(services));
        }
    }
}
