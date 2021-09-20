// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;
using E5R.Architecture.Data.Fluent.Query;
using E5R.Architecture.Data.Fluent.Writer;

namespace E5R.Architecture.Data.Abstractions
{
    public static class StorageExtensions
    {
        #region IStorageReader, IStorageWriter e IStorageBulkWriter
        /// <summary>
        /// Create fluent API object for query
        /// </summary>
        /// <typeparam name="TDataModel">Data type</typeparam>
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
        /// <typeparam name="TDataModel">Data type</typeparam>
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
        /// <typeparam name="TDataModel">Data type</typeparam>
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

        #region IFindableStorage
        /// <summary>
        /// Find an item stored by identifiers
        /// </summary>
        /// <typeparam name="TDataModel">Data type</typeparam>
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
        /// <typeparam name="TDataModel">Data type</typeparam>
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
        #endregion
    }
}
