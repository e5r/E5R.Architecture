// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.Query
{
    public class QueryBuilder<TDataModel> : QueryBuilderElements<TDataModel>
        where TDataModel : IDataModel
    {
        public QueryBuilder(IStorageReader<TDataModel> storage)
            : base(storage,
                  new LinqDataFilter<TDataModel>(),
                  new LinqDataLimiter<TDataModel>(),
                  new LinqDataProjection<TDataModel>())
        { }

        internal QueryBuilder(
            IStorageReader<TDataModel> storage,
            LinqDataFilter<TDataModel> filter,
            LinqDataLimiter<TDataModel> limiter,
            LinqDataProjection<TDataModel> projection)
            : base(storage, filter, limiter, projection)
        { }

        #region Makers

        public ProjectionRootBuilder<TDataModel> Projection()
            => new ProjectionRootBuilder<TDataModel>(_storage, _filter, _limiter, _projection);

        #endregion

        #region Storage Actions

        public TDataModel Find(object identifier) => _storage.Find(identifier, null);

        public TDataModel Find(object[] identifiers) => _storage.Find(identifiers, null);

        public TDataModel Find(TDataModel data) => _storage.Find(data, null);

        #endregion
    }
}
