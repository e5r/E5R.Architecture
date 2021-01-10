// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using System.Reflection;
using E5R.Architecture.Core;
using E5R.Architecture.Infrastructure.Abstractions;

namespace E5R.Architecture.Infrastructure.Extensions
{
    public static class AssemblyExtensions
    {
        public static void DIRegistrar(this Assembly assembly, IDIContainer container)
        {
            Checker.NotNullArgument(assembly, nameof(assembly));
            Checker.NotNullArgument(container, nameof(container));

            assembly.DefinedTypes
                .Where(t => t.ImplementedInterfaces.Contains(typeof(IDIRegistrar)))
                .Select(t => Activate(t.AsType()))
                .ToList()
                .ForEach(f => f.Register(container));
        }

        private static IDIRegistrar Activate(Type type)
        {
            Checker.NotNullArgument(type, nameof(type));

            return Activator.CreateInstance(type) as IDIRegistrar;
        }
    }
}
