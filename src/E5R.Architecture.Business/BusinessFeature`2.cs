// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Threading.Tasks;
using E5R.Architecture.Core;

namespace E5R.Architecture.Business
{
    /// <summary>
    /// Abstract business feature with input and output
    /// </summary>
    /// <typeparam name="TInput">The input type</typeparam>
    /// <typeparam name="TOutput">The output type</typeparam>
    public abstract class BusinessFeature<TInput, TOutput> : InputBasedBusinessFeature<TInput>,
        IBusinessFeatureSignature
    {
        public BusinessFeature(ILazy<ITransformationManager> transformer) : base(transformer)
        {
        }

        protected abstract Task<TOutput> ExecActionAsync(TInput input);

        public async Task<TOutput> ExecFromAsync<TFrom>(TFrom @from)
        {
            var input = TransformInput(@from);

            return await ExecActionAsync(input);
        }

        public async Task<TOutput> ExecAsync(TInput input)
        {
            Checker.NotNullArgument(input, nameof(input));

            return await ExecActionAsync(input);
        }
    }
}
