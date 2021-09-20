// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    public static class RemovableStorageExtensions
    {
        /// <summary>
        /// Removes a stored object
        /// </summary>
        /// <param name="identifier">Object identifier</param>
        public static void Remove<TDataModel>(this IRemovableStorage<TDataModel> storage, object identifier)
            where TDataModel : IIdentifiable
        {
            storage.Remove(new[] { identifier });
        }

        /// <summary>
        /// Removes a stored object
        /// </summary>
        /// <remarks>
        /// The object to be replaced is defined according to the value of <see cref="IIdentifiable.Identifiers" />.
        /// </remarks
        /// <param name="data">Object data</param>
        public static void Remove<TDataModel>(this IRemovableStorage<TDataModel> storage, TDataModel data)
            where TDataModel : IIdentifiable
        {
            storage.Remove(data.Identifiers);
        }
    }
}
