// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageWriter<TUowProperty, TDataModel> : IStorageWriter<TDataModel>
        where TDataModel : IDataModel
    { }

    public interface IStorageWriter<TDataModel> : IStorageSignature
        where TDataModel : IDataModel
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
        /// <remarks>
        /// The object to be replaced is defined according to the value of <see cref="TDataModel.IdentifierValues" />.
        /// </remarks
        /// <param name="data">Object data</param>
        void Remove(TDataModel data);
    }
}
