using System.Linq.Expressions;
// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using System;

namespace E5R.Architecture.Data.Abstractions
{
    /// <summary>
    /// Data projection (Include) for data model
    /// </summary>
    public interface IDataProjection
    {
        /// <summary>
        /// A navigation property paths (separated by ".")
        /// </summary>
        IEnumerable<string> Includes { get; }
    }

    // <summary>
    /// Data projection (Include and Select) for data model
    /// </summary>
    public interface IDataProjection<TDataModel, TSelect> : IDataProjection
    {
        Expression<Func<TDataModel, TSelect>> Select { get; }
    }
}
