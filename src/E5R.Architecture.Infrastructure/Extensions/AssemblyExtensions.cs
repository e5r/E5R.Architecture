// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using System.Reflection;
using E5R.Architecture.Core;
using E5R.Architecture.Infrastructure.Abstractions;
using Microsoft.Extensions.DependencyInjection;

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
        
        public static void AddAllRules(this Assembly assembly, IServiceCollection services)
        {
            Checker.NotNullArgument(assembly, nameof(assembly));
            Checker.NotNullArgument(services, nameof(services));
            
            var typeOfRuleFor = typeof(RuleFor<>);
            var typeOfIRuleFor = typeof(IRuleFor<>);

            assembly.DefinedTypes
                .Where(t =>
                    t.BaseType != null && t.BaseType.IsGenericType &&
                    t.BaseType.GetGenericTypeDefinition() == typeOfRuleFor
                    && t.GetInterfaces().Any(tt =>
                        tt.IsGenericType &&
                        tt.GetGenericTypeDefinition() == typeOfIRuleFor))
                .ToList()
                .ForEach(ruleType =>
                {
                    if (ruleType.BaseType is null) return;
                    var serviceType =
                        typeOfIRuleFor.MakeGenericType(
                            ruleType.BaseType.GetGenericArguments());

                    services.AddScoped(serviceType, ruleType);
                });
        }

        private static IDIRegistrar Activate(Type type)
        {
            Checker.NotNullArgument(type, nameof(type));

            return Activator.CreateInstance(type) as IDIRegistrar;
        }
    }
}
