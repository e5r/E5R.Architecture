// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

namespace E5R.Architecture.Core
{
    /// <summary>
    /// Implements fake lazy loading for development purposes
    /// </summary>
    /// <typeparam name="TTarget">Target type</typeparam>
    public class LazyResolverDevelopment<TTarget> : ILazy<TTarget>
    {
        public LazyResolverDevelopment(TTarget target)
        {
            Checker.NotNullArgument(target, nameof(target));
            
            Value = target;
        }
        
        public TTarget Value { get; }
    }
}
