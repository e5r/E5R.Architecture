// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using E5R.Architecture.Core;

namespace E5R.Architecture.Infrastructure
{
    public class InfrastructureOptions
    {
        private readonly List<string> _customServiceAssemblies = new List<string>();

        public IEnumerable<string> CustomServiceAssemblies => _customServiceAssemblies;
        public Type TransformationManagerType { get; set; } = typeof(TransformationManager);
        public Type RuleModelValidatorType { get; set; } = typeof(RuleModelValidator);
        public Type LazyResolverType { get; set; } = typeof(LazyResolver<>);
        public Type FileSystemType { get; set; }
        public Type SystemClockType { get; set; }
        public bool RegisterCrossCuttingAutomatically { get; set; } = true;
        public bool RegisterRulesAutomatically { get; set; } = true;
        public bool RegisterNotificationDispatchersAutomatically { get; set; } = true;
        public bool RegisterTransformersAutomatically { get; set; } = true;
        public bool RegisterRuleModelValidatorAutomatically { get; set; } = true;

        public InfrastructureOptions EnableDeveloperMode()
        {
            LazyResolverType = typeof(LazyResolverDevelopment<>);

            return this;
        }

        public InfrastructureOptions AddAssembly(string assemblyString) =>
            AddAssemblies(new string[] { assemblyString });

        public InfrastructureOptions AddAssemblies(string[] assemblyStrings)
        {
            Checker.NotNullOrEmptyArgument(assemblyStrings, nameof(assemblyStrings));

            foreach (var assemblyString in assemblyStrings.ToList())
            {
                if (!_customServiceAssemblies.Any(a =>
                    string.Equals(a, assemblyString, StringComparison.InvariantCultureIgnoreCase)))
                {
                    _customServiceAssemblies.Add(assemblyString);
                }
            }

            return this;
        }
    }
}
