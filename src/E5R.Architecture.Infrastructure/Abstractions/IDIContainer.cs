// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Infrastructure.Abstractions
{
    public interface IDIContainer
    {
        void Register(DILifetime lifetime, Type implementationType);
        void Register(DILifetime lifetime, Type baseType, Type implementationType);
        void Register(DILifetime lifetime, Type baseType, Func<object> implementationFactory);
    }
}
