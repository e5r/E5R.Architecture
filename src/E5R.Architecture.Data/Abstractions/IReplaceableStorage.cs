// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    // TODO: Renomear todos os X<TUowProperty, TDataModel> para X<TUow...>
    public interface IReplaceableStorage<TUowProperty, TDataModel> : IReplaceableStorage<TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    public interface IReplaceableStorage<TDataModel> : IStorageSignature
        where TDataModel : IIdentifiable
    {
        /// <summary>
        /// Replaces a stored object
        /// </summary>
        /// <remarks>
        /// The object to be replaced is defined according to the value of <see cref="IIdentifiable.Identifiers" />.
        /// </remarks
        /// <param name="data">Object data</param>
        /// <returns>Replaced object instance</returns>
        TDataModel Replace(TDataModel data);
    }
}
