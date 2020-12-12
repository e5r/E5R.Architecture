// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.Query
{
    public class QueryBuilderWithProjection<TDataModel> : QueryBuilderElements<TDataModel>
        where TDataModel : IDataModel
    {
        internal QueryBuilderWithProjection(IStorageReader<TDataModel> storage,
            LinqDataFilter<TDataModel> filter,
            LinqDataLimiter<TDataModel> limiter,
            LinqDataProjection<TDataModel> projection)
            : base(storage, filter, limiter, projection)
        { }

        public QueryBuilderWithProjection<TDataModel> Include(Expression<Func<TDataModel, object>> expression)
            => new ProjectionBuilder<TDataModel>(_storage, _filter, _limiter, _projection)
                .Include(expression)
                .Project();

        #region Storage Actions

        public TDataModel Find(object identifier) => _storage.Find(identifier, _projection);

        public TDataModel Find(object[] identifiers) => _storage.Find(identifiers, _projection);

        public TDataModel Find(TDataModel data) => _storage.Find(data, _projection);

        #endregion
    }
}
