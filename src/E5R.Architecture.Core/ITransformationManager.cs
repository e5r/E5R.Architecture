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
        /// Create a new instance of <typeparamref name="TTo"/> automatically based on the
        /// <typeparamref name="TFrom"/> instance.
        /// </summary>
        /// <remarks>
        /// If there is an <see cref="ITransformer{TFrom, TTo}"/> from <typeparamref name="TFrom"/> to
        /// <typeparamref name="TTo"/> it will be used, in another case a new instance of
        /// <typeparamref name="TTo"/> will be created and the property values will be copied from
        /// <typeparamref name="TFrom"/> to <typeparamref name="TTo"/> using
        /// <see cref="Extensions.ObjectExtensions.CopyPropertyValuesTo{TFrom, TTo}(TFrom, TTo)"/>.
        /// </remarks>
        /// <param name="from">Origin data</param>
        /// <returns>Instance of <typeparamref name="TTo"/></returns>
        TTo AutoTransform<TFrom, TTo>(TFrom from) where TTo : new();

        /// <summary>
        /// Create a new list of <typeparamref name="TTo"/> based on list of <typeparamref name="TFrom"/>.
        /// </summary>
        /// <param name="from">List of origin data</param>
        /// <returns>List of <typeparamref name="TTo"/></returns>
        IEnumerable<TTo> Transform<TFrom, TTo>(IEnumerable<TFrom> from);

        /// <summary>
        /// Create a new list of <typeparamref name="TTo"/> automatically based on list of 
        /// <typeparamref name="TFrom"/> instance.
        /// </summary>
        /// <remarks>
        /// If there is an <see cref="ITransformer{TFrom, TTo}"/> from <typeparamref name="TFrom"/> to
        /// <typeparamref name="TTo"/> it will be used, in another case a new instance of
        /// <typeparamref name="TTo"/> will be created and the property values will be copied from
        /// <typeparamref name="TFrom"/> to <typeparamref name="TTo"/> using
        /// <see cref="Extensions.ObjectExtensions.CopyPropertyValuesTo{TFrom, TTo}(TFrom, TTo)"/>.
        /// </remarks>
        /// <param name="from">List of origin data</param>
        /// <returns>List of <typeparamref name="TTo"/></returns>
        IEnumerable<TTo> AutoTransform<TFrom, TTo>(IEnumerable<TFrom> from) where TTo : new();

        /// <summary>
        /// Create a new paginated list of <typeparamref name="TTo"/> based on paginated list 
        /// of <typeparamref name="TFrom"/>.
        /// </summary>
        /// <param name="from">Paginated list of origin data</param>
        /// <returns>Paginated list of <typeparamref name="TTo"/></returns>
        PaginatedResult<TTo> Transform<TFrom, TTo>(PaginatedResult<TFrom> from);

        /// <summary>
        /// Create a new paginated list of <typeparamref name="TTo"/> automatically based on
        /// paginated list of <typeparamref name="TFrom"/>.
        /// </summary>
        /// <remarks>
        /// If there is an <see cref="ITransformer{TFrom, TTo}"/> from <typeparamref name="TFrom"/> to
        /// <typeparamref name="TTo"/> it will be used, in another case a new instance of
        /// <typeparamref name="TTo"/> will be created and the property values will be copied from
        /// <typeparamref name="TFrom"/> to <typeparamref name="TTo"/> using
        /// <see cref="Extensions.ObjectExtensions.CopyPropertyValuesTo{TFrom, TTo}(TFrom, TTo)"/>.
        /// </remarks>
        /// <param name="from">Paginated list of origin data</param>
        /// <returns>Paginated list of <typeparamref name="TTo"/></returns>
        PaginatedResult<TTo> AutoTransform<TFrom, TTo>(PaginatedResult<TFrom> from) where TTo : new();

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
