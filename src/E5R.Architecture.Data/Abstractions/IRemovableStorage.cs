// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    // TODO: Renomear todos os X<TUowProperty, TDataModel> para X<TUow...>
    public interface IRemovableStorage<TUowProperty, TDataModel> : IRemovableStorage<TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    public interface IRemovableStorage<TDataModel> : IStorageSignature
        where TDataModel : IIdentifiable
    {
        /// <summary>
        /// Removes a stored object
        /// </summary>
        /// <param name="identifiers">Object identifiers</param>
        void Remove(object[] identifiers);
    }
}
