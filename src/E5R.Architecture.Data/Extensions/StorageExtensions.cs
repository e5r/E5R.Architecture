// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;
using E5R.Architecture.Data.Fluent.Query;
using E5R.Architecture.Data.Fluent.Writer;
using System;
using System.Linq.Expressions;

namespace E5R.Architecture.Data.Abstractions
{
    public static class StorageExtensions
    {
        #region IStorageReader, IStorageWriter e IStorageBulkWriter
        /// <summary>
        /// Create fluent API object for query
        /// </summary>
        /// <param name="storage">Storage object</param>
        /// <returns>Instance of <see cref="FluentQueryBuilder{TDataModel}"/></returns>
        public static FluentQueryBuilder<TDataModel> AsFluentQuery<TDataModel>(
            this IStorageReader<TDataModel> storage)
            where TDataModel : IIdentifiable
        {
            return new FluentQueryBuilder<TDataModel>(storage);
        }

        /// <summary>
        /// Create fluent API object for writing
        /// </summary>
        /// <param name="storage">Storage object</param>
        /// <returns>Instance of <see cref="FluentWriterBuilder{TDataModel}"/></returns>
        public static FluentWriterBuilder<TDataModel> AsFluentWriter<TDataModel>(
            this IStorageWriter<TDataModel> storage)
            where TDataModel : IIdentifiable
        {
            return new FluentWriterBuilder<TDataModel>(storage);
        }

        /// <summary>
        /// Create fluent API object for bulk writing
        /// </summary>
        /// <param name="storage">Storage object</param>
        /// <returns>Instance of <see cref="FluentBulkWriterBuilder{TDataModel}"/></returns>
        public static FluentBulkWriterBuilder<TDataModel> AsFluentBulkWriter<TDataModel>(
            this IStorageBulkWriter<TDataModel> storage)
            where TDataModel : IIdentifiable
        {
            return new FluentBulkWriterBuilder<TDataModel>(storage);
        }
        #endregion

        #region IRemovableStorage
        /// <summary>
        /// Removes a stored object
        /// </summary>
        /// <param name="storage">Storage object</param>
        /// <param name="identifier">Object identifier</param>
        public static void Remove<TDataModel>(this IRemovableStorage<TDataModel> storage, object identifier)
            where TDataModel : IIdentifiable
        {
            Checker.NotNullArgument(storage, nameof(storage));

            storage.Remove(new[] { identifier });
        }

        /// <summary>
        /// Removes a stored object
        /// </summary>
        /// <remarks>
        /// The object to be replaced is defined according to the value of <see cref="IIdentifiable.Identifiers" />.
        /// </remarks
        /// <param name="storage">Storage object</param>
        /// <param name="data">Object data</param>
        public static void Remove<TDataModel>(this IRemovableStorage<TDataModel> storage, TDataModel data)
            where TDataModel : IIdentifiable
        {
            Checker.NotNullArgument(storage, nameof(storage));

            storage.Remove(data.Identifiers);
        }
        #endregion

        #region IFindableStorage, IFindableStorageWithSelector
        /// <summary>
        /// Find an item stored by a data instance
        /// </summary>
        /// <param name="storage">Storage object</param>
        /// <param name="data">Data item</param>
        /// <param name="includes">Linked data to include</param>
        /// <returns>Instance of <see cref="TDataModel"/>, or null when not found</returns>
        public static TDataModel Find<TDataModel>(this IFindableStorage<TDataModel> storage, TDataModel data, IDataIncludes includes = null)
            where TDataModel : IIdentifiable
        {
            Checker.NotNullArgument(storage, nameof(storage));
            Checker.NotNullArgument(data, nameof(data));

            return storage.Find(data.Identifiers, includes);
        }

        /// <summary>
        /// Find an item stored by identifier
        /// </summary>
        /// <param name="storage">Storage object</param>
        /// <param name="identifier">Item identifier</param>
        /// <param name="includes">Linked data to include</param>
        /// <returns>Instance of <see cref="TDataModel"/>, or null when not found</returns>
        public static TDataModel Find<TDataModel>(this IFindableStorage<TDataModel> storage, object identifier, IDataIncludes includes = null)
            where TDataModel : IIdentifiable
        {
            Checker.NotNullArgument(storage, nameof(storage));
            Checker.NotNullArgument(identifier, nameof(identifier));

            return storage.Find(new object[] { identifier }, includes);
        }

        /// <summary>
        /// Find an item stored by a data instance
        /// </summary>
        /// <param name="storage">Storage object</param>
        /// <param name="data">Data item</param>
        /// <param name="projection">Data projection</param>
        /// <returns>Instance of <typeparamref name="TSelect"/>, or null when not found</returns>
        public static TSelect Find<TDataModel, TSelect>(this IFindableStorageWithSelector<TDataModel> storage, TDataModel data, IDataProjection<TDataModel, TSelect> projection)
            where TDataModel : IIdentifiable
        {
            Checker.NotNullArgument(storage, nameof(storage));
            Checker.NotNullArgument(data, nameof(data));
            Checker.NotNullArgument(projection, nameof(projection));

            return storage.Find(data.Identifiers, projection);
        }

        /// <summary>
        /// Find an item stored by identifier
        /// </summary>
        /// <param name="storage">Storage object</param>
        /// <param name="identifier">Item identifier</param>
        /// <param name="projection">Data projection</param>
        /// <returns>Instance of <typeparamref name="TSelect"/>, or null when not found</returns>
        public static TSelect Find<TDataModel, TSelect>(this IFindableStorageWithSelector<TDataModel> storage, object identifier, IDataProjection<TDataModel, TSelect> projection)
            where TDataModel : IIdentifiable
        {
            Checker.NotNullArgument(storage, nameof(storage));
            Checker.NotNullArgument(identifier, nameof(identifier));
            Checker.NotNullArgument(projection, nameof(projection));

            return storage.Find(new object[] { identifier }, projection);
        }
        #endregion

        #region IUpdatableStorage
        /// <summary>
        /// Updates data from a stored object
        /// </summary>
        /// <param name="identifier">Object identifier</param>
        /// <param name="updated">Updated object data</param>
        /// <returns>Updated object instance</returns>
        public static TDataModel Update<TDataModel, TUpdated>(this IUpdatableStorage<TDataModel> storage,
            object identifier, TUpdated updated)
            where TDataModel : IIdentifiable
        {
            Checker.NotNullArgument(storage, nameof(storage));
            Checker.NotNullArgument(identifier, nameof(identifier));
            Checker.NotNullArgument(updated, nameof(updated));

            return storage.Update<TUpdated>(new[] { identifier }, _ => updated);
        }

        /// <summary>
        /// Updates data from a stored object
        /// </summary>
        /// <param name="identifier">Object identifier</param>
        /// <param name="updateExpression">Update expression</param>
        /// <typeparam name="TUpdated">Type for the updated data. Usually an anonymous type</typeparam>
        /// <returns>Updated object instance</returns>
        public static TDataModel Update<TDataModel, TUpdated>(this IUpdatableStorage<TDataModel> storage,
            object identifier,
            Expression<Func<TDataModel, TUpdated>> updateExpression)
            where TDataModel : IIdentifiable
        {
            Checker.NotNullArgument(storage, nameof(storage));
            Checker.NotNullArgument(identifier, nameof(identifier));
            Checker.NotNullArgument(updateExpression, nameof(updateExpression));

            return storage.Update(new[] { identifier }, updateExpression);
        }
        #endregion
    }
}
