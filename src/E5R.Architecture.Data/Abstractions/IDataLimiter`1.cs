// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;

namespace E5R.Architecture.Data.Abstractions
{
    /// <inheritdoc />
    public interface IDataLimiter<TDataModel> : IDataSorter<TDataModel>
        where TDataModel : IDataModel
    {
        uint? OffsetBegin { get; }
        uint? OffsetLimit { get; }
        bool Descending { get; }
        Expression<Func<TDataModel, object>> GetSorter();
    }
}
