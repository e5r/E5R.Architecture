using System;
using System.Linq;
using System.Reflection;
using E5R.Architecture.Core.Abstractions;

namespace E5R.Architecture.Core.Extensions
{
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
