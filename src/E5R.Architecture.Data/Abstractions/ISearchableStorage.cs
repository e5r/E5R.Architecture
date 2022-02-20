// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    // TODO: Renomear todos os X<TUowProperty, TDataModel> para X<TUow...>
    public interface ISearchableStorage<TUowProperty, TDataModel> : ISearchableStorage<TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    public interface ISearchableStorage<TDataModel> where TDataModel : IIdentifiable
    {
        /// <summary>
        /// Search for items stored according to filter entered
        /// </summary>
        /// <param name="filter">Data filter</param>
        /// <param name="includes">Linked data to include</param>
        /// <returns>List of <see cref="TDataModel"/></returns>
        IEnumerable<TDataModel> Search(IDataFilter<TDataModel> filter,
            IDataIncludes includes = null);

        /// <summary>
        /// Search for stored items in a limited way according to filter entered
        /// </summary>
        /// <param name="filter">Data filter</param>
        /// <param name="limiter">Limitation data</param>
        /// <param name="includes">Linked data to include</param>
        /// <returns>Paged list of <see cref="TDataModel"/></returns>
        PaginatedResult<TDataModel> LimitedSearch(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataIncludes includes = null);
    }
}
