// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.Fluent.Query
{
    public class FluentQueryBuilder<TDataModel> : FluentQueryBuilderElements<TDataModel>
        where TDataModel : IIdentifiable
    {
        public FluentQueryBuilder(IStorageReader<TDataModel> storage)
            : base(storage,
                  new DataFilter<TDataModel>(),
                  new DataLimiter<TDataModel>(),
                  new DataIncludes<TDataModel>())
        { }

        internal FluentQueryBuilder(
            IStorageReader<TDataModel> storage,
            DataFilter<TDataModel> filter,
            DataLimiter<TDataModel> limiter,
            DataIncludes<TDataModel> includes)
            : base(storage, filter, limiter, includes)
        { }

        FluentQueryBuilderWithProjection<TDataModel> EmptyProjection() => Projection().Project();

        #region Makers

        public ProjectionRootBuilder<TDataModel> Projection()
            => new ProjectionRootBuilder<TDataModel>(_storage, _filter, _limiter, _includes);

        public FluentQueryBuilderWithFilter<TDataModel> Filter(Expression<Func<TDataModel, bool>> filterExpression)
            => EmptyProjection().Filter(filterExpression);

        public FluentQueryBuilderWithLimiter<TDataModel> Sort(Expression<Func<TDataModel, object>> sortExpression)
            => EmptyProjection().Sort(sortExpression);

        public FluentQueryBuilderWithLimiter<TDataModel> SortDescending(Expression<Func<TDataModel, object>> sortExpression)
            => EmptyProjection().SortDescending(sortExpression);

        public FluentQueryBuilderWithLimiter<TDataModel> OffsetBegin(uint offsetBegin)
            => EmptyProjection().OffsetBegin(offsetBegin);

        public FluentQueryBuilderWithLimiter<TDataModel> OffsetLimit(uint offsetLimit)
            => EmptyProjection().OffsetLimit(offsetLimit);

        public FluentQueryBuilderWithLimiter<TDataModel> Paginate(uint currentPage, uint limitPerPage)
            => EmptyProjection().Paginate(currentPage, limitPerPage);

        #endregion

        #region Storage Actions

        public TDataModel Find(object identifier) => _storage.Find(identifier, null);

        public TDataModel Find(object[] identifiers) => _storage.Find(identifiers, null);

        public TDataModel Find(TDataModel data) => _storage.Find(data, null);
        
        public int CountAll() => _storage.CountAll();

        public IEnumerable<TDataModel> GetAll() => _storage.GetAll(null);

        #endregion
    }
}
