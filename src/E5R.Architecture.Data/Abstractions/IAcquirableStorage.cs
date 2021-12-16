// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    // TODO: Renomear todos os X<TUowProperty, TDataModel> para X<TUow...>
    public interface
        IAcquirableStorage<TUowProperty, TDataModel> : IAcquirableStorage<TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    /// <summary>
    /// Storage of acquirable items
    /// </summary>
    /// <typeparam name="TDataModel">Type of stored data</typeparam>
    public interface IAcquirableStorage<TDataModel> : IStorageSignature
        where TDataModel : IIdentifiable
    {
        /// <summary>
        /// Get the first item stored according to filter entered
        /// </summary>
        /// <param name="filter">Data filter</param>
        /// <param name="includes">Linked data to include</param>
        /// <returns>Instance of <see cref="TDataModel"/>, or null when not found</returns>
        TDataModel GetFirst(IDataFilter<TDataModel> filter, IDataIncludes includes = null);

        /// <summary>
        /// Get all stored items
        /// </summary>
        /// <param name="includes">Linked data to include</param>
        /// <returns>List of <see cref="TDataModel"/></returns>
        IEnumerable<TDataModel> GetAll(IDataIncludes includes = null);

        /// <summary>
        /// Gets the stored items in a limited way
        /// </summary>
        /// <param name="limiter">Limitation data</param>
        /// <param name="includes">Linked data to include</param>
        /// <returns>Paged list of <see cref="TDataModel"/></returns>
        PaginatedResult<TDataModel> LimitedGet(IDataLimiter<TDataModel> limiter,
            IDataIncludes includes = null);
    }
}
