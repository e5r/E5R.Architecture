// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    // TODO: Renomear todos os X<TUowProperty, TDataModel> para X<TUow...>
    public interface
        IAcquirableStorageWithSelector<TUowProperty, TDataModel> : IAcquirableStorageWithSelector<
            TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    public interface IAcquirableStorageWithSelector<TDataModel> : IStorageSignature
        where TDataModel : IIdentifiable
    {
        /// <summary>
        /// Get all stored items
        /// </summary>
        /// <param name="projection">Data projection (Include and Select) for data model</param>
        /// <returns>List of <see cref="IDataProjection{TDataModel, TSelect}.Select"/></returns>
        IEnumerable<TSelect> GetAll<TSelect>(IDataProjection<TDataModel, TSelect> projection);

        /// <summary>
        /// Gets the stored items in a limited way
        /// </summary>
        /// <param name="limiter">Limitation data</param>
        /// <param name="projection">Data projection (Include and Select) for data model</param>
        /// <returns>Paged list of <see cref="IDataProjection{TDataModel, TSelect}.Select"/></returns>
        PaginatedResult<TSelect> LimitedGet<TSelect>(IDataLimiter<TDataModel> limiter,
            IDataProjection<TDataModel, TSelect> projection);
    }
}
