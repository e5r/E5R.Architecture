// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.Query
{
    public class QueryBuilderWithProjection<TDataModel, TSelect> : QueryBuilderElements<TDataModel>
        where TDataModel : IDataModel
    {
        private new readonly LinqDataProjection<TDataModel, TSelect> _projection;

        internal QueryBuilderWithProjection(IStorageReader<TDataModel> storage,
            LinqDataFilter<TDataModel> filter,
            LinqDataLimiter<TDataModel> limiter,
            LinqDataProjection<TDataModel> projection,
            Expression<Func<TDataModel, TSelect>> select)
            : base(storage, filter, limiter, projection)
        {
            _projection = new LinqDataProjection<TDataModel, TSelect>(projection._includes, select);
        }

        #region Storage Actions

        public TSelect Find(object identifier) => _storage.Find<TSelect>(identifier, _projection);

        public TSelect Find(object[] identifiers) => _storage.Find<TSelect>(identifiers, _projection);

        public TSelect Find(TDataModel data) => _storage.Find<TSelect>(data, _projection);

        #endregion
    }
}
