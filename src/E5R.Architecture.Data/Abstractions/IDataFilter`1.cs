// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    /// <summary>
    /// Data filter for storage items
    /// </summary>
    /// <typeparam name="TDataModel">Model type</typeparam>
    public interface IDataFilter<TDataModel>
        where TDataModel : IIdentifiable
    {
        /// <summary>
        /// Get a filter expression list
        /// </summary>
        /// <returns>List of reducer expression</returns>
        IEnumerable<Expression<Func<TDataModel, bool>>> GetExpressions();

        /// <summary>
        /// Get a filter object list
        /// </summary>
        /// <returns>List of a reducer object</returns>
        IEnumerable<object> GetObjects();
    }
}
