// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using System.Reflection;

namespace E5R.Architecture.Infrastructure.Extensions
{
    using Core;
    using Abstractions;

    public static class AssemblyExtensions
    {
        public static void IoCRegistrar(this Assembly assembly, IIoCContainer container)
        {
            Checker.NotNullArgument(assembly, nameof(assembly));
            Checker.NotNullArgument(container, nameof(container));

            assembly.DefinedTypes
                .Where(t => t.ImplementedInterfaces.Contains(typeof(IIoCRegistrar)))
                .Select(t => Activate(t.AsType()))
                .ToList()
                .ForEach(f => f.Register(container));
        }

        private static IIoCRegistrar Activate(Type type)
        {
            Checker.NotNullArgument(type, nameof(type));

            return Activator.CreateInstance(type, new { }) as IIoCRegistrar;
        }
    }
}
