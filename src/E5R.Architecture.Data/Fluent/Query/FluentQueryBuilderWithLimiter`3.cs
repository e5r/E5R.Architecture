// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.Fluent.Query
{
    public class FluentQueryBuilderWithLimiter<TDataModel, TGroup, TSelect> : FluentQueryBuilderElements<TDataModel>
        where TDataModel : IIdentifiable
    {
        private readonly DataProjection<TDataModel, TGroup, TSelect> _projection;

        internal FluentQueryBuilderWithLimiter(IStorageReader<TDataModel> storage,
            DataFilter<TDataModel> filter,
            DataLimiter<TDataModel> limiter,
            DataProjection<TDataModel, TGroup, TSelect> projection)
            : base(storage, filter, limiter, projection.GetDataIncludes())
        {
            _projection = projection;
        }

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel, TGroup, TSelect> Filter(Expression<Func<TDataModel, bool>> filterExpression)
            => new FluentQueryBuilderWithLimiterAndFilter<TDataModel, TGroup, TSelect>(_storage, _filter, _limiter, _projection)
                .Filter(filterExpression);

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel, TGroup, TSelect> Filter(IIdentifiableExpressionMaker<TDataModel> filterMaker)
            => new FluentQueryBuilderWithLimiterAndFilter<TDataModel, TGroup, TSelect>(_storage, _filter, _limiter, _projection)
                .Filter(filterMaker);

        public FluentQueryBuilderWithLimiter<TDataModel, TGroup, TSelect> Sort(Expression<Func<TDataModel, object>> sorter)
        {
            _limiter.Sort(sorter);

            return this;
        }

        public FluentQueryBuilderWithLimiter<TDataModel, TGroup, TSelect> SortDescending(Expression<Func<TDataModel, object>> sorter)
        {
            _limiter.SortDescending(sorter);

            return this;
        }

        public FluentQueryBuilderWithLimiter<TDataModel, TGroup, TSelect> OffsetBegin(uint offset)
        {
            _limiter.BeginOffset(offset);

            return this;
        }

        public FluentQueryBuilderWithLimiter<TDataModel, TGroup, TSelect> OffsetLimit(uint offsetLimit)
        {
            _limiter.LimitOffset(offsetLimit);

            return this;
        }

        public FluentQueryBuilderWithLimiter<TDataModel, TGroup, TSelect> Paginate(uint currentPage, uint limitPerPage)
        {
            _limiter.Paginate(currentPage, limitPerPage);

            return this;
        }

        #region Storage Actions

        public PaginatedResult<TSelect> LimitedGet() => _storage.LimitedGet<TGroup, TSelect>(_limiter, _projection);

        #endregion
    }
}
