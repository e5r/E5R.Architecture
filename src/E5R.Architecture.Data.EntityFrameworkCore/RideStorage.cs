// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using System.Linq;
using E5R.Architecture.Core;
using E5R.Architecture.Core.Exceptions;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public class RideStorage<TDataModel> : StorageBase<TDataModel>, IStorageReader<TDataModel>
        where TDataModel : class, IDataModel
    {
        private readonly IQueryable<TDataModel> _query;

        public RideStorage(IQueryable<TDataModel> query)
        {
            Checker.NotNullArgument(query, nameof(query));

            _query = query;
        }

        #region IStorageReader

        public TDataModel Find(object identifier, IDataProjection<TDataModel> projection = null)
        {
            throw new DataLayerException($"{this.GetType().Name} not implement {nameof(Find)}(identifier)!");
        }

        public TDataModel Find(object[] identifiers, IDataProjection<TDataModel> projection = null)
        {
            throw new DataLayerException($"{this.GetType().Name} not implement {nameof(Find)}(identifiers)!");
        }

        public TDataModel Find(TDataModel data, IDataProjection<TDataModel> projection = null)
        {
            throw new DataLayerException($"{this.GetType().Name} not implement {nameof(Find)}(data)!");
        }

        public DataLimiterResult<TDataModel> Get(IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel> projection)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));

            return QueryLimitResult(limiter, _query, projection);
        }

        public IEnumerable<TDataModel> Search(IDataFilter<TDataModel> filter, IDataProjection<TDataModel> projection)
        {
            Checker.NotNullArgument(filter, nameof(filter));

            return QuerySearch(_query, filter, projection);
        }

        public DataLimiterResult<TDataModel> LimitedSearch(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel> projection)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(limiter, nameof(limiter));

            var result = QuerySearch(_query, filter, projection);

            // A projeção já foi aplicada em QuerySearch(), por isso não precisa
            // ser repassada aqui
            return QueryLimitResult(limiter, result, null);
        }

        #endregion
    }
}
