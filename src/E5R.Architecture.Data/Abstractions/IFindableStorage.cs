// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    // TODO: Renomear todos os X<TUowProperty, TDataModel> para X<TUow...>
    public interface IFindableStorage<TUowProperty, TDataModel> : IFindableStorage<TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    public interface IFindableStorage<TDataModel> : IStorageSignature
        where TDataModel : IIdentifiable
    {
        /// <summary>
        /// Find an item stored by identifiers
        /// </summary>
        /// <param name="identifiers">Item identifiers</param>
        /// <param name="includes">Linked data to include</param>
        /// <returns>Instance of <see cref="TDataModel"/>, or null when not found</returns>
        TDataModel Find(object[] identifiers, IDataIncludes includes = null);
    }
}
