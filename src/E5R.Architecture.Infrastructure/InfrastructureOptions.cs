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
        public Type LazyResolverType { get; set; } = typeof(LazyResolver<>);
        public Type FileSystemType { get; set; }
        public Type SystemClockType { get; set; }

        public InfrastructureOptions EnableDeveloperMode()
        {
            LazyResolverType = typeof(LazyResolverDevelopment<>);

            return this;
        }

        public InfrastructureOptions AddAssembly(string assemblyString)
        {
            Checker.NotEmptyOrWhiteArgument(assemblyString, nameof(assemblyString));

            if (!_customServiceAssemblies.Any(a =>
                string.Equals(a, assemblyString, StringComparison.InvariantCultureIgnoreCase)))
            {
                _customServiceAssemblies.Add(assemblyString);
            }
            
            return this;
        }
    }
}
