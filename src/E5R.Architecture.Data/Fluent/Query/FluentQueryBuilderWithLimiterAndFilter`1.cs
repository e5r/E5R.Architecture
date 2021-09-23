// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.Fluent.Query
{
    public class FluentQueryBuilderWithLimiterAndFilter<TDataModel> : FluentQueryBuilderElements<TDataModel>
        where TDataModel : IIdentifiable
    {
        internal FluentQueryBuilderWithLimiterAndFilter(IStorageReader<TDataModel> storage,
            DataFilter<TDataModel> filter,
            DataLimiter<TDataModel> limiter,
            DataIncludes<TDataModel> includes)
            : base(storage, filter, limiter, includes)
        { }

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel> Filter(Expression<Func<TDataModel, bool>> filter)
        {
            _filter.AddFilter(filter);

            return this;
        }

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel> Sort(Expression<Func<TDataModel, object>> sorter)
        {
            _limiter.Sort(sorter);

            return this;
        }

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel> SortDescending(Expression<Func<TDataModel, object>> sorter)
        {
            _limiter.SortDescending(sorter);

            return this;
        }

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel> OffsetBegin(uint offset)
        {
            _limiter.BeginOffset(offset);

            return this;
        }

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel> OffsetLimit(uint offsetLimit)
        {
            _limiter.LimitOffset(offsetLimit);

            return this;
        }

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel> Paginate(uint currentPage, uint limitPerPage)
        {
            _limiter.Paginate(currentPage, limitPerPage);

            return this;
        }

        #region Storage Actions

        public PaginatedResult<TDataModel> LimitedSearch() => _storage.LimitedSearch(_filter, _limiter, _includes);

        #endregion
    }
}
