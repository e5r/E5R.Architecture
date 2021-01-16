// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

namespace E5R.Architecture.Core
{
    /// <summary>
    /// Abstracts lazy loading for objects that need to be resolved by reversing control
    /// </summary>
    /// <typeparam name="TTarget">Target type</typeparam>
    public interface ILazy<TTarget>
    {
        TTarget Value { get; }
    }
}
