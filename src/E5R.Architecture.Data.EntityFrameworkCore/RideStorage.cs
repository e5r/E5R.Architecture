// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using E5R.Architecture.Core;
using E5R.Architecture.Core.Exceptions;
using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Infrastructure;
using Microsoft.EntityFrameworkCore;

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

        public TDataModel Find(TDataModel data)
        {
            throw new DataLayerException($"{nameof(RideStorage<TDataModel>)} not implement {nameof(Find)} method!");
        }

        public DataLimiterResult<TDataModel> Get(IDataLimiter<TDataModel> limiter, IEnumerable<IDataProjection> projections)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));

            return QueryLimitResult(limiter, _query, projections);
        }

        public IEnumerable<TDataModel> Search(IDataFilter<TDataModel> filter, IEnumerable<IDataProjection> projections)
        {
            Checker.NotNullArgument(filter, nameof(filter));

            return QuerySearch(_query, filter, projections);
        }

        public DataLimiterResult<TDataModel> LimitedSearch(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IEnumerable<IDataProjection> projections)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(limiter, nameof(limiter));

            var result = QuerySearch(_query, filter, projections);

            // A projeção já foi aplicada em QuerySearch(), por isso não precisa
            // ser repassada aqui
            return QueryLimitResult(limiter, result, null);
        }

        #endregion
    }
}
