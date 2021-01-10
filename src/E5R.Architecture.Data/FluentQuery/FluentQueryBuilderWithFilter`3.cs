// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.FluentQuery
{
    public class FluentQueryBuilderWithFilter<TDataModel, TGroup, TSelect> : FluentQueryBuilderElements<TDataModel>
        where TDataModel : IDataModel
    {
        private readonly DataProjection<TDataModel, TGroup, TSelect> _projection;

        internal FluentQueryBuilderWithFilter(IStorageReader<TDataModel> storage,
            DataFilter<TDataModel> filter,
            DataLimiter<TDataModel> limiter,
            DataProjection<TDataModel, TGroup, TSelect> projection)
            : base(storage, filter, limiter, projection.GetDataIncludes())
        {
            _projection = projection;
        }

        public FluentQueryBuilderWithFilter<TDataModel, TGroup, TSelect> Filter(Expression<Func<TDataModel, bool>> filter)
        {
            _filter.AddFilter(filter);

            return this;
        }

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel, TGroup, TSelect> Sort(Expression<Func<TDataModel, object>> sorter)
            => new FluentQueryBuilderWithLimiterAndFilter<TDataModel, TGroup, TSelect>(_storage, _filter, _limiter, _projection)
                .Sort(sorter);

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel, TGroup, TSelect> SortDescending(Expression<Func<TDataModel, object>> sorter)
            => new FluentQueryBuilderWithLimiterAndFilter<TDataModel, TGroup, TSelect>(_storage, _filter, _limiter, _projection)
                .SortDescending(sorter);

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel, TGroup, TSelect> OffsetBegin(uint offset)
            => new FluentQueryBuilderWithLimiterAndFilter<TDataModel, TGroup, TSelect>(_storage, _filter, _limiter, _projection)
                .OffsetBegin(offset);

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel, TGroup, TSelect> OffsetLimit(uint offsetLimit)
            => new FluentQueryBuilderWithLimiterAndFilter<TDataModel, TGroup, TSelect>(_storage, _filter, _limiter, _projection)
                .OffsetLimit(offsetLimit);

        public FluentQueryBuilderWithLimiterAndFilter<TDataModel, TGroup, TSelect> Paginate(uint currentPage, uint limitPerPage)
            => new FluentQueryBuilderWithLimiterAndFilter<TDataModel, TGroup, TSelect>(_storage, _filter, _limiter, _projection)
                .Paginate(currentPage, limitPerPage);

        #region Storage Actions

        public IEnumerable<TSelect> Search() => _storage.Search<TGroup, TSelect>(_filter, _projection);

        #endregion
    }
}
