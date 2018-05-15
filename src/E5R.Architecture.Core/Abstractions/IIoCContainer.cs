// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Core.Abstractions
{
    public interface IIoCContainer
    {
        void Register(IoCLifecycle lifecycle, Type implementationType);
        void Register(IoCLifecycle lifecycle, Type baseType, Type implementationType);
        void Register(IoCLifecycle lifecycle, Type baseType, Func<object> implementationFactory);
    }
}
