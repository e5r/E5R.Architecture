// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;

namespace E5R.Architecture.Core
{
    /// <summary>
    /// Abstract data transformation manager
    /// </summary>
    public interface ITransformationManager
    {
        /// <summary>
        /// Create new instance of <typeparamref name="TTo"/> based on <see cref="TFrom"/> instance.
        /// </summary>
        /// <param name="from">Origin data</param>
        /// <returns>Instance of <typeparamref name="TTo"/></returns>
        TTo Transform<TFrom, TTo>(TFrom from);

        /// <summary>
        /// Create a new list of <typeparamref name="TTo"/> based on list of <typeparamref name="TFrom"/>.
        /// </summary>
        /// <param name="from">List of origin data</param>
        /// <returns>List of <typeparamref name="TTo"/></returns>
        IEnumerable<TTo> Transform<TFrom, TTo>(IEnumerable<TFrom> from);

        /// <summary>
        /// Create a new paginated list of <typeparamref name="TTo"/> based on paginated list 
        /// of <typeparamref name="TFrom"/>.
        /// </summary>
        /// <param name="from">Paginated list of origin data</param>
        /// <returns>Paginated list of <typeparamref name="TTo"/></returns>
        PaginatedResult<TTo> Transform<TFrom, TTo>(PaginatedResult<TFrom> from);

        /// <summary>
        /// Create new instance of <typeparamref name="TTo"/> based on <typeparamref name="TFrom"/> instance
        /// and <typeparamref name="TOperation"/> value.
        /// </summary>
        /// <param name="from">Origin data</param>
        /// <param name="operation">The operation reference</param>
        /// <returns>Instance of <see cref="TTo"/></returns>
        TTo Transform<TFrom, TTo, TOperation>(TFrom from, TOperation operation)
            where TTo : new() where TOperation : Enum;

        /// <summary>
        /// Create a new list of <typeparamref name="TTo"/> based on list of <typeparamref name="TFrom"/> and
        /// <typeparamref name="TOperation"/> value.
        /// </summary>
        /// <param name="from">List of origin data</param>
        /// <param name="operation">The operation reference</param>
        /// <returns>List of <typeparamref name="TTo"/></returns>
        IEnumerable<TTo> Transform<TFrom, TTo, TOperation>(IEnumerable<TFrom> from, TOperation operation)
            where TTo : new() where TOperation : Enum;

        /// <summary>
        /// Create a new paginated list of <typeparamref name="TTo"/> based on list of 
        /// <typeparamref name="TFrom"/> and <typeparamref name="TOperation"/> value.
        /// </summary>
        /// <param name="from">Paginated list of origin data</param>
        /// <param name="operation">The operation reference</param>
        /// <returns>Paginated list of <typeparamref name="TTo"/></returns>
        PaginatedResult<TTo> Transform<TFrom, TTo, TOperation>(PaginatedResult<TFrom> from, TOperation operation)
            where TTo : new() where TOperation : Enum;
    }
}
