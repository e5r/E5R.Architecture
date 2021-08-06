// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    public interface
        IStorageReader<TUowProperty, TDataModel> : IFindableStorage<TUowProperty, TDataModel>,
            IFindableStorageWithSelector<TUowProperty, TDataModel>,
            ICountableStorage<TUowProperty, TDataModel>,
            IAcquirableStorage<TUowProperty, TDataModel>,
            IAcquirableStorageWithGrouping<TUowProperty, TDataModel>,
            IAcquirableStorageWithSelector<TUowProperty, TDataModel>,
            ISearchableStorage<TUowProperty, TDataModel>,
            ISearchableStorageWithGrouping<TUowProperty, TDataModel>,
            ISearchableStorageWithSelector<TUowProperty, TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    public interface IStorageReader<TDataModel> : IFindableStorage<TDataModel>,
        IFindableStorageWithSelector<TDataModel>,
        ICountableStorage<TDataModel>, IAcquirableStorage<TDataModel>,
        IAcquirableStorageWithGrouping<TDataModel>,
        IAcquirableStorageWithSelector<TDataModel>,
        ISearchableStorage<TDataModel>, ISearchableStorageWithGrouping<TDataModel>,
        ISearchableStorageWithSelector<TDataModel>
        where TDataModel : IIdentifiable
    {
    }
}
