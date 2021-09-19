// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.Fluent.Query
{
    public class FluentQueryBuilderWithProjection<TDataModel, TGroup, TSelect> : FluentQueryBuilderElements<TDataModel>
        where TDataModel : IIdentifiable
    {
        private readonly DataProjection<TDataModel, TGroup, TSelect> _projection;

        internal FluentQueryBuilderWithProjection(IStorageReader<TDataModel> storage,
            ExpressionDataFilter<TDataModel> filter,
            DataLimiter<TDataModel> limiter,
            DataIncludes<TDataModel> includes,
            Expression<Func<TDataModel, TGroup>> group,
            Expression<Func<IGrouping<TGroup, TDataModel>, TSelect>> select)
            : base(storage, filter, limiter, includes)
        {
            _projection =
                new DataProjection<TDataModel, TGroup, TSelect>(includes._includes, group, select);
        }

        public FluentQueryBuilderWithFilter<TDataModel, TGroup, TSelect> Filter(Expression<Func<TDataModel, bool>> filterExpression)
            => new FluentQueryBuilderWithFilter<TDataModel, TGroup, TSelect>(_storage, _filter, _limiter, _projection)
                .Filter(filterExpression);

        public FluentQueryBuilderWithLimiter<TDataModel, TGroup, TSelect> Sort(Expression<Func<TDataModel, object>> sortExpression)
            => new FluentQueryBuilderWithLimiter<TDataModel, TGroup, TSelect>(_storage, _filter, _limiter, _projection)
                .Sort(sortExpression);

        public FluentQueryBuilderWithLimiter<TDataModel, TGroup, TSelect> SortDescending(Expression<Func<TDataModel, object>> sortExpression)
            => new FluentQueryBuilderWithLimiter<TDataModel, TGroup, TSelect>(_storage, _filter, _limiter, _projection)
                .SortDescending(sortExpression);

        public FluentQueryBuilderWithLimiter<TDataModel, TGroup, TSelect> OffsetBegin(uint offsetBegin)
            => new FluentQueryBuilderWithLimiter<TDataModel, TGroup, TSelect>(_storage, _filter, _limiter, _projection)
                .OffsetBegin(offsetBegin);

        public FluentQueryBuilderWithLimiter<TDataModel, TGroup, TSelect> OffsetLimit(uint offsetLimit)
            => new FluentQueryBuilderWithLimiter<TDataModel, TGroup, TSelect>(_storage, _filter, _limiter, _projection)
                .OffsetLimit(offsetLimit);

        public FluentQueryBuilderWithLimiter<TDataModel, TGroup, TSelect> Paginate(uint currentPage, uint limitPerPage)
            => new FluentQueryBuilderWithLimiter<TDataModel, TGroup, TSelect>(_storage, _filter, _limiter, _projection)
                .Paginate(currentPage, limitPerPage);

        #region Storage Actions

        public IEnumerable<TSelect> GetAll() => _storage.GetAll<TGroup, TSelect>(_projection);

        #endregion
    }
}
