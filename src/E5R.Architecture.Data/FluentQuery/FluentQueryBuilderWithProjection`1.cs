// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.FluentQuery
{
    public class FluentQueryBuilderWithProjection<TDataModel> : FluentQueryBuilderElements<TDataModel>
        where TDataModel : IDataModel
    {
        internal FluentQueryBuilderWithProjection(IStorageReader<TDataModel> storage,
            DataFilter<TDataModel> filter,
            DataLimiter<TDataModel> limiter,
            DataIncludes<TDataModel> includes)
            : base(storage, filter, limiter, includes)
        { }

        public FluentQueryBuilderWithFilter<TDataModel> Filter(Expression<Func<TDataModel, bool>> filterExpression)
            => new FluentQueryBuilderWithFilter<TDataModel>(_storage, _filter, _limiter, _includes)
                .Filter(filterExpression);

        public FluentQueryBuilderWithLimiter<TDataModel> Sort(Expression<Func<TDataModel, object>> sortExpression)
            => new FluentQueryBuilderWithLimiter<TDataModel>(_storage, _filter, _limiter, _includes)
                .Sort(sortExpression);

        public FluentQueryBuilderWithLimiter<TDataModel> SortDescending(Expression<Func<TDataModel, object>> sortExpression)
            => new FluentQueryBuilderWithLimiter<TDataModel>(_storage, _filter, _limiter, _includes)
                .SortDescending(sortExpression);

        public FluentQueryBuilderWithLimiter<TDataModel> OffsetBegin(uint offsetBegin)
            => new FluentQueryBuilderWithLimiter<TDataModel>(_storage, _filter, _limiter, _includes)
                .OffsetBegin(offsetBegin);

        public FluentQueryBuilderWithLimiter<TDataModel> OffsetLimit(uint offsetLimit)
            => new FluentQueryBuilderWithLimiter<TDataModel>(_storage, _filter, _limiter, _includes)
                .OffsetLimit(offsetLimit);

        public FluentQueryBuilderWithLimiter<TDataModel> Paginate(uint currentPage, uint limitPerPage)
            => new FluentQueryBuilderWithLimiter<TDataModel>(_storage, _filter, _limiter, _includes)
                .Paginate(currentPage, limitPerPage);

        #region Storage Actions

        public TDataModel Find(object identifier) => _storage.Find(identifier, _includes);

        public TDataModel Find(object[] identifiers) => _storage.Find(identifiers, _includes);

        public TDataModel Find(TDataModel data) => _storage.Find(data, _includes);

        public IEnumerable<TDataModel> GetAll() => _storage.GetAll(_includes);

        #endregion
    }
}
