// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageWriter<TUowProperty, TDataModel> : IStorageWriter<TDataModel>,
        ICreatableStorage<TUowProperty, TDataModel>, IReplaceableStorage<TUowProperty, TDataModel>,
        IRemovableStorage<TUowProperty, TDataModel>, IUpdatableStorage<TUowProperty, TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    public interface IStorageWriter<TDataModel> : IStorageSignature, ICreatableStorage<TDataModel>,
        IReplaceableStorage<TDataModel>, IRemovableStorage<TDataModel>,
        IUpdatableStorage<TDataModel>
        where TDataModel : IIdentifiable

    {
    }
}
