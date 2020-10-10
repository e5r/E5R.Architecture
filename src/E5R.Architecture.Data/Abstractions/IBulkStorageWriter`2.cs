// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;

namespace E5R.Architecture.Data.Abstractions
{
    public interface IBulkStorageWriter<TUowProperty, TDataModel> : IBulkStorageWriter<TDataModel>
        where TDataModel : IDataModel
    { }

    public interface IBulkStorageWriter<TDataModel> : IStorageSignature
        where TDataModel : IDataModel
    {
        IEnumerable<TDataModel> BulkCreate(IEnumerable<TDataModel> data);
        IEnumerable<TDataModel> BulkReplace(IEnumerable<TDataModel> data);
        void BulkRemove(IEnumerable<TDataModel> data);
        void BulkRemoveFromSearch(DataFilter<TDataModel> filter);
    }
}
