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
        private readonly IStorageReader<TDataModel> _storage;

        public LinqStorageQueryBuilder(IStorageReader<TDataModel> storage)
        {
            Checker.NotNullArgument(storage, nameof(storage));

            _storage = storage;

            Filter = new LinqDataFilter<TDataModel>();
            Limiter = new LinqDataLimiter<TDataModel>();
        }

        private LinqDataFilter<TDataModel> Filter { get; set; }
        private LinqDataLimiter<TDataModel> Limiter { get; set; }

        #region Configuration   
        public LinqStorageQueryBuilder<TDataModel> AddFilter(Expression<Func<TDataModel, bool>> filter)
        {
            Filter.AddFilter(filter);

            return this;
        }

        public LinqStorageQueryBuilder<TDataModel> Sort(Expression<Func<TDataModel, object>> sorter)
        {
            Limiter.Sort(sorter);

            return this;
        }

        public LinqStorageQueryBuilder<TDataModel> OffsetBegin(uint offset)
        {
            Limiter.Begin(offset);

            return this;
        }

        public LinqStorageQueryBuilder<TDataModel> OffsetLimit(uint offsetLimit)
        {
            Limiter.Limit(offsetLimit);

            return this;
        }

        public LinqStorageQueryBuilder<TDataModel> Descending()
        {
            Limiter.Desc();

            return this;
        }
        #endregion

        #region Actions
        public DataLimiterResult<TDataModel> Get() => _storage.Get(Limiter);

        public IEnumerable<TDataModel> Search() => _storage.Search(Filter);

        public DataLimiterResult<TDataModel> LimitedSearch() => _storage.LimitedSearch(Filter, Limiter);
        #endregion
    }
}
