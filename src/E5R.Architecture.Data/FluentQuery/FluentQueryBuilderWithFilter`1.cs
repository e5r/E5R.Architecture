﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.FluentQuery
{
    public class FluentQueryBuilderWithFilter<TDataModel> : FluentQueryBuilderElements<TDataModel>
        where TDataModel : IDataModel
    {
        internal FluentQueryBuilderWithFilter(IStorageReader<TDataModel> storage,
            DataFilter<TDataModel> filter,
            DataLimiter<TDataModel> limiter,
            DataIncludes<TDataModel> includes)
            : base(storage, filter, limiter, includes)
        { }

        public FluentQueryBuilderWithFilter<TDataModel> Filter(Expression<Func<TDataModel, bool>> filter)
        {
            _filter.AddFilter(filter);

            return this;
        }

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel> Sort(Expression<Func<TDataModel, object>> sorter)
            => new FluentQueryBuilderWithLimiterAndFilter<TDataModel>(_storage, _filter, _limiter, _includes)
                .Sort(sorter);

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel> SortDescending(Expression<Func<TDataModel, object>> sorter)
            => new FluentQueryBuilderWithLimiterAndFilter<TDataModel>(_storage, _filter, _limiter, _includes)
                .SortDescending(sorter);

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel> OffsetBegin(uint offset)
            => new FluentQueryBuilderWithLimiterAndFilter<TDataModel>(_storage, _filter, _limiter, _includes)
                .OffsetBegin(offset);

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel> OffsetLimit(uint offsetLimit)
            => new FluentQueryBuilderWithLimiterAndFilter<TDataModel>(_storage, _filter, _limiter, _includes)
                .OffsetLimit(offsetLimit);

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel> Paginate(uint currentPage, uint limitPerPage)
            => new FluentQueryBuilderWithLimiterAndFilter<TDataModel>(_storage, _filter, _limiter, _includes)
                .Paginate(currentPage, limitPerPage);

        #region Storage Actions

        public IEnumerable<TDataModel> Search() => _storage.Search(_filter, _includes);

        #endregion
    }
}
