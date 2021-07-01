// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    // TODO: Renomear todos os X<TUowProperty, TDataModel> para X<TUow...>
    public interface
        IBulkRemovableStorage<TUowProperty, TDataModel> : IBulkRemovableStorage<TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    public interface IBulkRemovableStorage<TDataModel> : IStorageSignature
        where TDataModel : IIdentifiable
    {
        /// <summary>
        /// Removes stored objects in bulk
        /// </summary>
        /// <param name="data">Object data collection</param>
        void BulkRemove(IEnumerable<TDataModel> data);

        /// <summary>
        /// Removes stored objects in bulk
        /// </summary>
        /// <remarks>
        /// The objects to be removed are defined according to the result of a search with the <see cref="ITDataFilter" />.
        /// </remarks
        /// <param name="filter">Data filter</param>
        void BulkRemove(IDataFilter<TDataModel> filter);
    }
}
