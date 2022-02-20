// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    // TODO: Renomear todos os X<TUowProperty, TDataModel> para X<TUow...>
    public interface ICountableStorage<TUowProperty, TDataModel> : ICountableStorage<TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    public interface ICountableStorage<TDataModel> where TDataModel : IIdentifiable
    {
        /// <summary>
        /// Count all stored records
        /// </summary>
        /// <returns>Total number of all records</returns>
        int CountAll();

        /// <summary>
        /// Count records stored according to the filter entered
        /// </summary>
        /// <param name="filter">Data filter</param>
        /// <returns>Total number of records</returns>
        int Count(IDataFilter<TDataModel> filter);
    }
}
