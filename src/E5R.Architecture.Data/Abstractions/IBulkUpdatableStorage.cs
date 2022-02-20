// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    // TODO: Renomear todos os X<TUowProperty, TDataModel> para X<TUow...>
    public interface
        IBulkUpdatableStorage<TUowProperty, TDataModel> : IBulkUpdatableStorage<TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    public interface IBulkUpdatableStorage<TDataModel> where TDataModel : IIdentifiable
    {
        /// <summary>
        /// Updates data from stored objects in bulk
        /// </summary>
        /// <param name="filter">Data filter</param>
        /// <param name="updated">Updated object data</param>
        /// <typeparam name="TUpdated">Type for the updated data. Usually an anonymous type</typeparam>
        /// <returns>Updated object instances</returns>
        IEnumerable<TDataModel> BulkUpdate<TUpdated>(IDataFilter<TDataModel> filter,
            TUpdated updated);

        /// <summary>
        /// Updates data from stored objects in bulk
        /// </summary>
        /// <param name="filter">Data filter</param>
        /// <param name="updateExpression">Update data expression</param>
        /// <typeparam name="TUpdated">Type for the updated data. Usually an anonymous type</typeparam>
        /// <returns>Updated object instances</returns>
        IEnumerable<TDataModel> BulkUpdate<TUpdated>(IDataFilter<TDataModel> filter,
            Expression<Func<TDataModel, TUpdated>> updateExpression);
    }
}
