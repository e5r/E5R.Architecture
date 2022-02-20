// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    // TODO: Renomear todos os X<TUowProperty, TDataModel> para X<TUow...>
    public interface
        ISearchableStorageWithSelector<TUowProperty, TDataModel> : ISearchableStorageWithSelector<
            TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    public interface ISearchableStorageWithSelector<TDataModel> where TDataModel : IIdentifiable
    {
        /// <summary>
        /// Search for items stored according to filter entered
        /// </summary>
        /// <param name="filter">Data filter</param>
        /// <param name="projection">Data projection (Include, Group and Select) for data model</param>
        /// <returns>List of <see cref="IDataProjection{TDataModel, TSelect}.Select"/></returns>
        IEnumerable<TSelect> Search<TSelect>(IDataFilter<TDataModel> filter,
            IDataProjection<TDataModel, TSelect> projection);

        /// <summary>
        /// Search for stored items in a limited way according to filter entered
        /// </summary>
        /// <param name="filter">Data filter</param>
        /// <param name="limiter">Limitation data</param>
        /// <param name="projection">Data projection (Include, Group and Select) for data model</param>
        /// <returns>Paged list of <see cref="IDataProjection{TDataModel, TSelect}.Select"/></returns>
        PaginatedResult<TSelect> LimitedSearch<TSelect>(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel, TSelect> projection);
    }
}
