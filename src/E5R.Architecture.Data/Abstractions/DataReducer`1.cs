// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace E5R.Architecture.Data.Abstractions
{
    /// <summary>
    /// Data reducer (Where) for data model with identifier
    /// </summary>
    /// <typeparam name="TDataModel">Model type</typeparam>
    public abstract class DataReducer<TDataModel>
        where TDataModel : IDataModel
    {
        /// <summary>
        /// Get a reducer expression (Where)
        /// </summary>
        /// <returns>List of reducer expression</returns>
        public abstract IEnumerable<Expression<Func<TDataModel, bool>>> GetReducer();
    }
}
