// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data
{
    public class LinqStorageQueryBuilder<TDataModel>
        where TDataModel : IDataModel
    {
        internal readonly IStorageReader<TDataModel> _storage;

        public LinqStorageQueryBuilder(IStorageReader<TDataModel> storage)
        {
            Checker.NotNullArgument(storage, nameof(storage));

            _storage = storage;

            Filter = new LinqDataFilter<TDataModel>();
            Limiter = new LinqDataLimiter<TDataModel>();
            Projection = new LinqDataProjection<TDataModel>();
        }

        internal LinqDataFilter<TDataModel> Filter { get; set; }
        internal LinqDataLimiter<TDataModel> Limiter { get; set; }
        internal LinqDataProjection<TDataModel> Projection { get; set; }

        #region Configuration   
        public LinqStorageQueryBuilder<TDataModel> AddFilter(Expression<Func<TDataModel, bool>> filter)
        {
            Filter.AddFilter(filter);

            return this;
        }

        public LinqDataProjectionQueryBuilder<TDataModel> AddProjection()
        {
            return new LinqDataProjectionQueryBuilder<TDataModel>(this);
        }

        public LinqStorageQueryBuilder<TDataModel> Sort(Expression<Func<TDataModel, object>> sorter)
        {
            Limiter.Sort(sorter);

            return this;
        }

        public LinqStorageQueryBuilder<TDataModel> SortDescending(Expression<Func<TDataModel, object>> sorter)
        {
            Limiter.SortDescending(sorter);

            return this;
        }

        public LinqStorageQueryBuilder<TDataModel> OffsetBegin(uint offset)
        {
            Limiter.BeginOffset(offset);

            return this;
        }

        public LinqStorageQueryBuilder<TDataModel> OffsetLimit(uint offsetLimit)
        {
            Limiter.LimitOffset(offsetLimit);

            return this;
        }

        public LinqStorageQueryBuilder<TDataModel> Paginate(uint currentPage, uint limitPerPage)
        {
            Limiter.Paginate(currentPage, limitPerPage);

            return this;
        }
        #endregion

        #region Actions
        public TDataModel Find(object identifier) => _storage.Find(identifier, Projection);

        public TDataModel Find(object[] identifiers) => _storage.Find(identifiers, Projection);

        public TDataModel Find(TDataModel data) => _storage.Find(data, Projection);

        public PaginatedResult<TDataModel> Get() => _storage.Get(Limiter, Projection);

        public IEnumerable<TDataModel> Search() => _storage.Search(Filter, Projection);

        public PaginatedResult<TDataModel> LimitedSearch() => _storage.LimitedSearch(Filter, Limiter, Projection);
        #endregion
    }
}
