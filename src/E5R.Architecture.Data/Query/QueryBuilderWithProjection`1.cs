// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

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

        #region Storage Actions

        public TDataModel Find(object identifier) => _storage.Find(identifier, _projection);

        public TDataModel Find(object[] identifiers) => _storage.Find(identifiers, _projection);

        public TDataModel Find(TDataModel data) => _storage.Find(data, _projection);

        #endregion
    }
}
