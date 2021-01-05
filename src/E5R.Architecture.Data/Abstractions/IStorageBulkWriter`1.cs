// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;

namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageBulkWriter<TUowProperty, TDataModel> : IStorageBulkWriter<TDataModel>
        where TDataModel : IDataModel
    { }

    public interface IStorageBulkWriter<TDataModel> : IStorageSignature
        where TDataModel : IDataModel
    {
        /// <summary>
        /// Creates new stored objects in bulk
        /// </summary>
        /// <param name="data">Object data collection</param>
        /// <returns>Created object instances</returns>
        IEnumerable<TDataModel> BulkCreate(IEnumerable<TDataModel> data);

        /// <summary>
        /// Replaces stored objects in bulk
        /// </summary>
        /// <param name="data">Object data collection</param>
        /// <returns>Replaced object instances</returns>
        IEnumerable<TDataModel> BulkReplace(IEnumerable<TDataModel> data);

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
