// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    public interface
        IStorageBulkWriter<TUowProperty, TDataModel> :
            IBulkCreatableStorage<TUowProperty, TDataModel>,
            IBulkRemovableStorage<TUowProperty, TDataModel>,
            IBulkReplaceableStorage<TUowProperty, TDataModel>,
            IBulkUpdatableStorage<TUowProperty, TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    public interface IStorageBulkWriter<TDataModel> : IBulkCreatableStorage<TDataModel>,
        IBulkRemovableStorage<TDataModel>, IBulkReplaceableStorage<TDataModel>,
        IBulkUpdatableStorage<TDataModel>
        where TDataModel : IIdentifiable
    {
    }
}
