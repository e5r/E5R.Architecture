// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Threading.Tasks;

namespace E5R.Architecture.Business
{
    /// <summary>
    /// Abstract business feature without input or output
    /// </summary>
    public abstract class BusinessFeature : IBusinessFeatureSignature
    {
        protected abstract Task ExecActionAsync();

        public Task ExecAsync() => ExecActionAsync();
    }
}
