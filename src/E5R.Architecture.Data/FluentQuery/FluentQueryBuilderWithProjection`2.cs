// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.FluentQuery
{
    public class FluentQueryBuilderWithProjection<TDataModel, TSelect> : FluentQueryBuilderElements<TDataModel>
        where TDataModel : IDataModel
    {
        private readonly DataProjection<TDataModel, TSelect> _projection;

        internal FluentQueryBuilderWithProjection(IStorageReader<TDataModel> storage,
            DataFilter<TDataModel> filter,
            DataLimiter<TDataModel> limiter,
            DataIncludes<TDataModel> includes,
            Expression<Func<TDataModel, TSelect>> select)
            : base(storage, filter, limiter, includes)
        {
            _projection = new DataProjection<TDataModel, TSelect>(includes._includes, select);
        }

        public FluentQueryBuilderWithFilter<TDataModel, TSelect> Filter(Expression<Func<TDataModel, bool>> filterExpression)
            => new FluentQueryBuilderWithFilter<TDataModel, TSelect>(_storage, _filter, _limiter, _projection)
                .Filter(filterExpression);

        public FluentQueryBuilderWithLimiter<TDataModel, TSelect> Sort(Expression<Func<TDataModel, object>> sortExpression)
            => new FluentQueryBuilderWithLimiter<TDataModel, TSelect>(_storage, _filter, _limiter, _projection)
                .Sort(sortExpression);

        public FluentQueryBuilderWithLimiter<TDataModel, TSelect> SortDescending(Expression<Func<TDataModel, object>> sortExpression)
            => new FluentQueryBuilderWithLimiter<TDataModel, TSelect>(_storage, _filter, _limiter, _projection)
                .SortDescending(sortExpression);

        public FluentQueryBuilderWithLimiter<TDataModel, TSelect> OffsetBegin(uint offsetBegin)
            => new FluentQueryBuilderWithLimiter<TDataModel, TSelect>(_storage, _filter, _limiter, _projection)
                .OffsetBegin(offsetBegin);

        public FluentQueryBuilderWithLimiter<TDataModel, TSelect> OffsetLimit(uint offsetLimit)
            => new FluentQueryBuilderWithLimiter<TDataModel, TSelect>(_storage, _filter, _limiter, _projection)
                .OffsetLimit(offsetLimit);

        public FluentQueryBuilderWithLimiter<TDataModel, TSelect> Paginate(uint currentPage, uint limitPerPage)
            => new FluentQueryBuilderWithLimiter<TDataModel, TSelect>(_storage, _filter, _limiter, _projection)
                .Paginate(currentPage, limitPerPage);

        #region Storage Actions

        public TSelect Find(object identifier) => _storage.Find<TSelect>(identifier, _projection);

        public TSelect Find(object[] identifiers) => _storage.Find<TSelect>(identifiers, _projection);

        public TSelect Find(TDataModel data) => _storage.Find<TSelect>(data, _projection);

        public IEnumerable<TSelect> GetAll() => _storage.GetAll<TSelect>(_projection);

        #endregion
    }
}
