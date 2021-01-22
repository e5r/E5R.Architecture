// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Core
{
    /// <summary>
    /// Abstract data transformer
    /// </summary>
    /// <typeparam name="TFrom">From data type</typeparam>
    /// <typeparam name="TTo">To data type</typeparam>
    /// <typeparam name="TOperation">The operation enum</typeparam>
    public interface ITransformer<TFrom, TTo, TOperation> where TTo : new() where TOperation : Enum
    {
        /// <summary>
        /// Create new instance of <see cref="TTo"/> based on <see cref="TFrom"/> instance and <see cref="TOperation"/> value.
        /// </summary>
        /// <param name="from">Origin data</param>
        /// <param name="operation">The operation reference</param>
        /// <returns>Instance of <see cref="TTo"/></returns>
        TTo Transform(TFrom from, TOperation operation);
    }
}
