// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;

namespace E5R.Architecture.Data.Abstractions
{
    /// <summary>
    /// Data sorter (OrderBy) for data model with identifier
    /// </summary>
    /// <typeparam name="TDataModel">Data model type</typeparam>
    public interface IDataSorter<TDataModel>
        where TDataModel : IDataModel
    {
        /// <summary>
        /// Descending order
        /// </summary>
        bool Descending { get; }

        /// <summary>
        /// Get a sorter expression (OrderBy)
        /// </summary>
        /// <returns>List of sorter expression</returns>
        Expression<Func<TDataModel, object>> GetSorter();
    }
}
