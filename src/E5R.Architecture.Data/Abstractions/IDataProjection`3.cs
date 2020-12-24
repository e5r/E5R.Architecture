// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using System.Linq.Expressions;

namespace E5R.Architecture.Data.Abstractions
{
    // <summary>
    /// Data projection (Include, Group and Select) for data model
    /// </summary>
    public interface IDataProjection<TDataModel, TGroup, TSelect> : IDataProjection
    {
        Expression<Func<IGrouping<TGroup, TDataModel>, TSelect>> Select { get; }
        Expression<Func<TDataModel, TGroup>> Group { get; }
    }
}
