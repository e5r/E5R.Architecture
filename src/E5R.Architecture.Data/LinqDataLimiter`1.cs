// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;
using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    /// <inheritdoc />
    public class LinqDataLimiter<TDataModel> : IDataLimiter<TDataModel>
        where TDataModel : IDataModel
    {
        public uint? OffsetBegin { get; private set; }
        public uint? OffsetLimit { get; private set; }
        public bool Descending { get; private set; }
        public Expression<Func<TDataModel, object>> GetSorter() => _sorter;

        private Expression<Func<TDataModel, object>> _sorter = null;

        public LinqDataLimiter<TDataModel> Sort(Expression<Func<TDataModel, object>> sorter)
        {
            Checker.NotNullArgument(sorter, nameof(sorter));

            _sorter = sorter;

            return this;
        }

        public LinqDataLimiter<TDataModel> Begin(uint offset)
        {
            OffsetBegin = offset;

            return this;
        }

        public LinqDataLimiter<TDataModel> Limit(uint offsetLimit)
        {
            OffsetLimit = offsetLimit;

            return this;
        }

        public LinqDataLimiter<TDataModel> Desc()
        {
            Descending = true;

            return this;
        }
    }
}
