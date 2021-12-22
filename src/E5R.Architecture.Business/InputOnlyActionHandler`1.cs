// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Threading.Tasks;
using E5R.Architecture.Core;

namespace E5R.Architecture.Business
{
    /// <summary>
    /// Abstract action handler with input only
    /// </summary>
    /// <typeparam name="TInput">The input type</typeparam>
    public abstract class InputOnlyActionHandler<TInput> : IActionHandler
    {
        protected abstract Task ExecActionAsync(TInput input);

        public async Task ExecAsync(TInput input)
        {
            Checker.NotNullArgument(input, nameof(input));

            await ExecActionAsync(input);
        }
    }
}
