// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using System.Reflection;
using E5R.Architecture.Core;
using E5R.Architecture.Infrastructure.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace E5R.Architecture.Business.Extensions
{
    public static class AssemblyExtensions
    {
        public static void AddBusinessFeatures(this Assembly assembly, IServiceCollection services)
        {
            Checker.NotNullArgument(assembly, nameof(assembly));
            Checker.NotNullArgument(services, nameof(services));

            var typeOfFeatureSignature = typeof(IBusinessFeatureSignature);

            assembly.DefinedTypes
                .Where(t => !t.IsAbstract && typeOfFeatureSignature.IsAssignableFrom(t))
                .ToList()
                .ForEach(services.TryAddScoped);
        }

        private static IDIRegistrar Activate(Type type)
        {
            Checker.NotNullArgument(type, nameof(type));

            return Activator.CreateInstance(type) as IDIRegistrar;
        }
    }
}
