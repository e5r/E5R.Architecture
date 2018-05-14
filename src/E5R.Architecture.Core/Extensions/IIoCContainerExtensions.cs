using System;
using E5R.Architecture.Core.Abstractions;

namespace E5R.Architecture.Core.Extensions
{
    public static class IIoCContainerExtensions
    {
        private static void Register<TImplementation>(this IIoCContainer container,
            IoCLifecycle lifecycle)
            where TImplementation : class
            => container.Register(lifecycle, typeof(TImplementation));

        private static void Register<TBase, TImplementation>(this IIoCContainer container,
            IoCLifecycle lifecycle)
            where TBase : class
            where TImplementation : class, TBase
            => container.Register(lifecycle, typeof(TBase), typeof(TImplementation));

        private static void Register<TBase>(this IIoCContainer container,
            IoCLifecycle lifecycle,
            Func<TBase> factory)
            where TBase : class
            => container.Register(lifecycle, typeof(TBase), factory);

        public static void RegisterTransient<TImplementation>(this IIoCContainer container)
            where TImplementation : class
            => container.Register(IoCLifecycle.Transient, typeof(TImplementation));

        public static void RegisterTransient<TBase, TImplementation>(this IIoCContainer container)
            where TBase : class
            where TImplementation : class, TBase
            => container.Register(IoCLifecycle.Transient, typeof(TBase), typeof(TImplementation));

        public static void RegisterTransient<TBase>(this IIoCContainer container,
            Func<TBase> factory)
            where TBase : class
            => container.Register(IoCLifecycle.Transient, typeof(TBase), factory);

        public static void RegisterScoped<TImplementation>(this IIoCContainer container)
            where TImplementation : class
            => container.Register(IoCLifecycle.Scoped, typeof(TImplementation));

        public static void RegisterScoped<TBase, TImplementation>(this IIoCContainer container)
            where TBase : class
            where TImplementation : class, TBase
            => container.Register(IoCLifecycle.Scoped, typeof(TBase), typeof(TImplementation));

        public static void RegisterScoped<TBase>(this IIoCContainer container,
            Func<TBase> factory)
            where TBase : class
            => container.Register(IoCLifecycle.Scoped, typeof(TBase), factory);

        public static void RegisterSingleton<TImplementation>(this IIoCContainer container)
            where TImplementation : class
            => container.Register(IoCLifecycle.Singleton, typeof(TImplementation));

        public static void RegisterSingleton<TBase, TImplementation>(this IIoCContainer container)
            where TBase : class
            where TImplementation : class, TBase
            => container.Register(IoCLifecycle.Singleton, typeof(TBase), typeof(TImplementation));

        public static void RegisterSingleton<TBase>(this IIoCContainer container,
            Func<TBase> factory)
            where TBase : class
            => container.Register(IoCLifecycle.Singleton, typeof(TBase), factory);
    }
}
