// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using E5R.Architecture.Infrastructure.Abstractions;

namespace E5R.Architecture.Infrastructure.Extensions
{
    public static class IDIContainerExtensions
    {
        private static void Register<TImplementation>(this IDIContainer container,
            DILifetime lifetime)
            where TImplementation : class
            => container.Register(lifetime, typeof(TImplementation));

        private static void Register<TBase, TImplementation>(this IDIContainer container,
            DILifetime lifetime)
            where TBase : class
            where TImplementation : class, TBase
            => container.Register(lifetime, typeof(TBase), typeof(TImplementation));

        private static void Register<TBase>(this IDIContainer container,
            DILifetime lifetime,
            Func<TBase> factory)
            where TBase : class
            => container.Register(lifetime, typeof(TBase), factory);

        public static void RegisterTransient<TImplementation>(this IDIContainer container)
            where TImplementation : class
            => container.Register(DILifetime.Transient, typeof(TImplementation));

        public static void RegisterTransient<TBase, TImplementation>(this IDIContainer container)
            where TBase : class
            where TImplementation : class, TBase
            => container.Register(DILifetime.Transient, typeof(TBase), typeof(TImplementation));

        public static void RegisterTransient<TBase>(this IDIContainer container,
            Func<TBase> factory)
            where TBase : class
            => container.Register(DILifetime.Transient, typeof(TBase), factory);

        public static void RegisterScoped<TImplementation>(this IDIContainer container)
            where TImplementation : class
            => container.Register(DILifetime.Scoped, typeof(TImplementation));

        public static void RegisterScoped<TBase, TImplementation>(this IDIContainer container)
            where TBase : class
            where TImplementation : class, TBase
            => container.Register(DILifetime.Scoped, typeof(TBase), typeof(TImplementation));

        public static void RegisterScoped<TBase>(this IDIContainer container,
            Func<TBase> factory)
            where TBase : class
            => container.Register(DILifetime.Scoped, typeof(TBase), factory);

        public static void RegisterSingleton<TImplementation>(this IDIContainer container)
            where TImplementation : class
            => container.Register(DILifetime.Singleton, typeof(TImplementation));

        public static void RegisterSingleton<TBase, TImplementation>(this IDIContainer container)
            where TBase : class
            where TImplementation : class, TBase
            => container.Register(DILifetime.Singleton, typeof(TBase), typeof(TImplementation));

        public static void RegisterSingleton<TBase>(this IDIContainer container,
            Func<TBase> factory)
            where TBase : class
            => container.Register(DILifetime.Singleton, typeof(TBase), factory);
    }
}
