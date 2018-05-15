// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;

namespace E5R.Architecture.Data.Abstractions
{
    /// <inheritdoc />
    public abstract class DataLimiter<TDataModel> : IDataSorter<TDataModel>
        where TDataModel : IDataModel
    {
        public int OffsetBegin { get; set; }
        public int OffsetEnd { get; set; }
        public bool Descending { get; set; }
        public abstract Expression<Func<TDataModel, object>> GetSorter();
    }
}
