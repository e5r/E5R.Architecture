// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.Fluent.Query
{
    public class FluentQueryBuilderWithLimiter<TDataModel> : FluentQueryBuilderElements<TDataModel>
        where TDataModel : IIdentifiable
    {
        internal FluentQueryBuilderWithLimiter(IStorageReader<TDataModel> storage,
            DataFilter<TDataModel> filter,
            DataLimiter<TDataModel> limiter,
            DataIncludes<TDataModel> includes)
            : base(storage, filter, limiter, includes)
        { }

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel> Filter(Expression<Func<TDataModel, bool>> filterExpression)
            => new FluentQueryBuilderWithLimiterAndFilter<TDataModel>(_storage, _filter, _limiter, _includes)
                .Filter(filterExpression);

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel> Filter(IIdentifiableExpressionMaker<TDataModel> filterMaker)
            => new FluentQueryBuilderWithLimiterAndFilter<TDataModel>(_storage, _filter, _limiter, _includes)
                .Filter(filterMaker);

        public FluentQueryBuilderWithLimiter<TDataModel> Sort(Expression<Func<TDataModel, object>> sorter)
        {
            _limiter.Sort(sorter);

            return this;
        }

        public FluentQueryBuilderWithLimiter<TDataModel> SortDescending(Expression<Func<TDataModel, object>> sorter)
        {
            _limiter.SortDescending(sorter);

            return this;
        }

        public FluentQueryBuilderWithLimiter<TDataModel> OffsetBegin(uint offset)
        {
            _limiter.BeginOffset(offset);

            return this;
        }

        public FluentQueryBuilderWithLimiter<TDataModel> OffsetLimit(uint offsetLimit)
        {
            _limiter.LimitOffset(offsetLimit);

            return this;
        }

        public FluentQueryBuilderWithLimiter<TDataModel> Paginate(uint currentPage, uint limitPerPage)
        {
            _limiter.Paginate(currentPage, limitPerPage);

            return this;
        }

        #region Storage Actions

        public PaginatedResult<TDataModel> LimitedGet() => _storage.LimitedGet(_limiter, _includes);

        #endregion
    }
}
