// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;
using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageWriter<TUowProperty, TDataModel> : IStorageWriter<TDataModel>
        where TDataModel : IIdentifiable
    { }

    public interface IStorageWriter<TDataModel> : IStorageSignature
        where TDataModel : IIdentifiable
    {
        /// <summary>
        /// Creates a new stored object
        /// </summary>
        /// <param name="data">Object data</param>
        /// <returns>Created object instance</returns>
        TDataModel Create(TDataModel data);

        /// <summary>
        /// Replaces a stored object
        /// </summary>
        /// <remarks>
        /// The object to be replaced is defined according to the value of <see cref="TDataModel.IdentifierValues" />.
        /// </remarks
        /// <param name="data">Object data</param>
        /// <returns>Replaced object instance</returns>
        TDataModel Replace(TDataModel data);

        /// <summary>
        /// Removes a stored object
        /// </summary>
        /// <param name="identifier">Object identifier</param>
        void Remove(object identifier);

        /// <summary>
        /// Removes a stored object
        /// </summary>
        /// <param name="identifiers">Object identifiers</param>
        void Remove(object[] identifiers);

        /// <summary>
        /// Removes a stored object
        /// </summary>
        /// <remarks>
        /// The object to be replaced is defined according to the value of <see cref="TDataModel.IdentifierValues" />.
        /// </remarks
        /// <param name="data">Object data</param>
        void Remove(TDataModel data);

        /// <summary>
        /// Updates data from a stored object
        /// </summary>
        /// <param name="identifier">Object identifier</param>
        /// <param name="updated">Updated object data</param>
        /// <typeparam name="TUpdated">Type for the updated data. Usually an anonymous type</typeparam>
        /// <returns>Updated object instance</returns>
        TDataModel Update<TUpdated>(object identifier, TUpdated updated);

        /// <summary>
        /// Updates data from a stored object
        /// </summary>
        /// <param name="identifier">Object identifier</param>
        /// <param name="updateExpression">Update expression</param>
        /// <typeparam name="TUpdated">Type for the updated data. Usually an anonymous type</typeparam>
        /// <returns>Updated object instance</returns>
        TDataModel Update<TUpdated>(object identifier, Expression<Func<TDataModel, TUpdated>> updateExpression);

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
        TDataModel Update<TUpdated>(object[] identifiers, Expression<Func<TDataModel, TUpdated>> updateExpression);
    }
}
