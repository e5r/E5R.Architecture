// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Threading.Tasks;

namespace E5R.Architecture.Business
{
    /// <summary>
    /// Abstract business feature with output only
    /// </summary>
    /// <typeparam name="TOutput">The output type</typeparam>
    public abstract class OutputOnlyBusinessFeature<TOutput>
    {
        protected abstract Task<TOutput> ExecActionAsync();

        public Task<TOutput> ExecAsync() => ExecActionAsync();
    }
}
