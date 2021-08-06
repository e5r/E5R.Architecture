﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using System.Reflection;
using E5R.Architecture.Core;
using E5R.Architecture.Infrastructure.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace E5R.Architecture.Infrastructure.Extensions
{
    public static class AssemblyExtensions
    {
        public static void AddCrossCuttingRegistrar(this Assembly assembly, IServiceCollection services, IConfiguration configuration)
        {
            Checker.NotNullArgument(assembly, nameof(assembly));
            Checker.NotNullArgument(services, nameof(services));
            Checker.NotNullArgument(configuration, nameof(configuration));

            assembly.DefinedTypes
                .Where(t => t.ImplementedInterfaces.Contains(typeof(ICrossCuttingRegistrar)))
                .Select(t => Activate(t.AsType()))
                .ToList()
                .ForEach(f => f.Register(services, configuration));
        }

        public static void AddAllNotificationDispatchers(this Assembly assembly,
            IServiceCollection services)
        {
            Checker.NotNullArgument(assembly, nameof(assembly));
            Checker.NotNullArgument(services, nameof(services));

            var typeOfINotificationDispatcher = typeof(INotificationDispatcher<>);

            assembly.DefinedTypes
                .Where(t =>
                    t.GetInterfaces().Any(tt =>
                        tt.IsGenericType &&
                        tt.GetGenericTypeDefinition() == typeOfINotificationDispatcher
                    ))
                .ToList()
                .ForEach(dispatcherType =>
                {
                    var serviceType = dispatcherType.GetInterfaces().FirstOrDefault(t =>
                        t.IsGenericType && t.GetGenericTypeDefinition() ==
                        typeOfINotificationDispatcher);

                    if (services.Any(w =>
                        w.ServiceType == serviceType && w.ImplementationType == dispatcherType))
                        return;

                    services.AddScoped(serviceType, dispatcherType);
                });
        }

        public static void AddAllTransformers(this Assembly assembly,
            IServiceCollection services)
        {
            Checker.NotNullArgument(assembly, nameof(assembly));
            Checker.NotNullArgument(services, nameof(services));

            var transformerTypes = new[]
            {
                typeof(ITransformer<,>),
                typeof(ITransformer<,,>)
            };

            assembly.DefinedTypes
                .Where(t =>
                    t.GetInterfaces().Any(tt =>
                        tt.IsGenericType &&
                        transformerTypes.Contains(tt.GetGenericTypeDefinition())
                    ))
                .ToList()
                .ForEach(transformerType =>
                {
                    transformerType.GetInterfaces()
                        .Where(t =>
                            t.IsGenericType &&
                            transformerTypes.Contains(t.GetGenericTypeDefinition()))
                        .ToList()
                        .ForEach(serviceType =>
                        {
                            if (services.Any(w =>
                                w.ServiceType == serviceType &&
                                w.ImplementationType == transformerType))
                            {
                                return;
                            }

                            services.AddScoped(serviceType, transformerType);
                        });
                });
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

                    if (services.Any(w =>
                        w.ServiceType == serviceType && w.ImplementationType == ruleType)) return;

                    services.AddScoped(serviceType, ruleType);
                });
        }

        public static void AddAllLazyGroups(this Assembly assembly, IServiceCollection services)
        {
            Checker.NotNullArgument(assembly, nameof(assembly));
            Checker.NotNullArgument(services, nameof(services));

            var groupTypes = new[]
            {
                typeof(LazyGroup<,>),
                typeof(LazyGroup<,,>),
                typeof(LazyGroup<,,,>),
                typeof(LazyGroup<,,,,>),
                typeof(LazyGroup<,,,,,>),
                typeof(LazyGroup<,,,,,,>),
                typeof(LazyGroup<,,,,,,,>),
                typeof(LazyGroup<,,,,,,,,>),
                typeof(LazyGroup<,,,,,,,,,>),
            };

            assembly.DefinedTypes
                .Where(t =>
                    t.BaseType != null && t.BaseType.IsGenericType &&
                    groupTypes.Contains(t.BaseType.GetGenericTypeDefinition()))
                .ToList()
                .ForEach(services.TryAddScoped);
        }

        private static ICrossCuttingRegistrar Activate(Type type)
        {
            Checker.NotNullArgument(type, nameof(type));

            return Activator.CreateInstance(type) as ICrossCuttingRegistrar;
        }
    }
}
