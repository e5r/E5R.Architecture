// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System.Reflection;

namespace E5R.Architecture.Infrastructure.Extensions
{
    using Core;
    using Abstractions;

    public static class TypeExtensions
    {
        public static void IoCRegistrar<T>(IIoCContainer container)
        {
            Checker.NotNullArgument(container, nameof(container));

            typeof(T).GetTypeInfo().Assembly.IoCRegistrar(container);
        }
    }
}
