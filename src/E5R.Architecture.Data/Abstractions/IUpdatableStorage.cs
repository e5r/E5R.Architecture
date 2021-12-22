// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;
using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    // TODO: Renomear todos os X<TUowProperty, TDataModel> para X<TUow...>
    public interface IUpdatableStorage<TUowProperty, TDataModel> : IUpdatableStorage<TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    public interface IUpdatableStorage<TDataModel> : IStorageSignature
        where TDataModel : IIdentifiable
    {
        /// <summary>
        /// Updates data from a stored object
        /// </summary>
        /// <param name="identifiers">Object identifiers</param>
        /// <param name="updated">Updated object data</param>
        /// <typeparam name="TUpdated">Type for the updated data. Usually an anonymous type</typeparam>
        /// <returns>Updated object instance</returns>
        TDataModel Update<TUpdated>(object[] identifiers, TUpdated updated);

        /// <summary>
        /// Updates data from a stored object
        /// </summary>
        /// <param name="identifiers">Object identifiers</param>
        /// <param name="updateExpression">Update expression</param>
        /// <typeparam name="TUpdated">Type for the updated data. Usually an anonymous type</typeparam>
        /// <returns>Updated object instance</returns>
        TDataModel Update<TUpdated>(object[] identifiers,
            Expression<Func<TDataModel, TUpdated>> updateExpression);
    }
}
