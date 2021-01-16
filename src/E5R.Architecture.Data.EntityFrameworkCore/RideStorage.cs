// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
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

        #region IStorageReader for TDataModel

        public TDataModel Find(TDataModel data, IDataIncludes includes = null)
        {
            throw new DataLayerException($"{this.GetType().Name} not implement {nameof(Find)}(data)!");
        }

        public TDataModel Find(object identifier, IDataIncludes includes = null)
        {
            throw new DataLayerException($"{this.GetType().Name} not implement {nameof(Find)}(identifier)!");
        }

        public TDataModel Find(object[] identifiers, IDataIncludes includes = null)
        {
            throw new DataLayerException($"{this.GetType().Name} not implement {nameof(Find)}(identifiers)!");
        }

        public IEnumerable<TDataModel> GetAll(IDataIncludes includes) => TryApplyIncludes(_query, includes);

        public PaginatedResult<TDataModel> LimitedGet(IDataLimiter<TDataModel> limiter, IDataIncludes includes)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));

            var (offset, limit, total, result) = QueryPreLimitResult(_query, limiter, includes);

            return new PaginatedResult<TDataModel>(
                result,
                offset,
                limit,
                total
            );
        }

        public IEnumerable<TDataModel> Search(IDataFilter<TDataModel> filter, IDataIncludes includes)
        {
            Checker.NotNullArgument(filter, nameof(filter));

            return QuerySearch(_query, filter, includes);
        }

        public PaginatedResult<TDataModel> LimitedSearch(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataIncludes includes)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(limiter, nameof(limiter));

            var query = QuerySearch(_query, filter, includes);

            // A projeção já foi aplicada em QuerySearch(),
            // por isso não precisa ser repassada aqui
            var (offset, limit, total, result) = QueryPreLimitResult(query, limiter, null);

            return new PaginatedResult<TDataModel>(
                result,
                offset,
                limit,
                total
            );
        }

        #endregion

        #region IStorageReader for TSelect

        public TSelect Find<TSelect>(TDataModel data, IDataProjection<TDataModel, TSelect> projection)
        {
            throw new DataLayerException($"{this.GetType().Name} not implement {nameof(Find)}(data)!");
        }

        public TSelect Find<TSelect>(object identifier, IDataProjection<TDataModel, TSelect> projection)
        {
            throw new DataLayerException($"{this.GetType().Name} not implement {nameof(Find)}(identifier)!");
        }

        public TSelect Find<TSelect>(object[] identifiers, IDataProjection<TDataModel, TSelect> projection)
        {
            throw new DataLayerException($"{this.GetType().Name} not implement {nameof(Find)}(identifiers)!");
        }

        public IEnumerable<TSelect> GetAll<TSelect>(IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Select, $"{nameof(projection)}.{nameof(projection.Select)}");

            return TryApplyIncludes(_query, projection).Select(projection.Select);
        }

        public PaginatedResult<TSelect> LimitedGet<TSelect>(IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Select, $"{nameof(projection)}.{nameof(projection.Select)}");

            var (offset, limit, total, result) = QueryPreLimitResult(_query, limiter, projection);

            return new PaginatedResult<TSelect>(
                result.Select(projection.Select),
                offset,
                limit,
                total
            );
        }

        public IEnumerable<TSelect> Search<TSelect>(IDataFilter<TDataModel> filter, IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Select, $"{nameof(projection)}.{nameof(projection.Select)}");

            return QuerySearch(_query, filter, projection)
                .Select(projection.Select);
        }

        public PaginatedResult<TSelect> LimitedSearch<TSelect>(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(limiter, nameof(limiter));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Select, $"{nameof(projection)}.{nameof(projection.Select)}");

            var query = QuerySearch(_query, filter, projection);

            // A projeção já foi aplicada em QuerySearch(),
            // por isso não precisa ser repassada aqui
            var (offset, limit, total, result) = QueryPreLimitResult(query, limiter, null);

            return new PaginatedResult<TSelect>(
                result.Select(projection.Select),
                offset,
                limit,
                total
            );
        }

        #endregion

        #region IStorageReader for TGroup

        public IEnumerable<TSelect> GetAll<TGroup, TSelect>(IDataProjection<TDataModel, TGroup, TSelect> projection)
        {
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Group, $"{nameof(projection)}.{nameof(projection.Group)}");
            Checker.NotNullObject(projection.Select, $"{nameof(projection)}.{nameof(projection.Select)}");

            return TryApplyIncludes(_query, projection)
                .GroupBy(projection.Group)
                .Select(projection.Select);
        }

        public PaginatedResult<TSelect> LimitedGet<TGroup, TSelect>(IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel, TGroup, TSelect> projection)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Group, $"{nameof(projection)}.{nameof(projection.Group)}");
            Checker.NotNullObject(projection.Select, $"{nameof(projection)}.{nameof(projection.Select)}");

            var (offset, limit, total, result) = QueryPreLimitResult(_query, limiter, projection);

            return new PaginatedResult<TSelect>(
                result.GroupBy(projection.Group).Select(projection.Select),
                offset,
                limit,
                total
            );
        }

        public IEnumerable<TSelect> Search<TGroup, TSelect>(IDataFilter<TDataModel> filter, IDataProjection<TDataModel, TGroup, TSelect> projection)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Group, $"{nameof(projection)}.{nameof(projection.Group)}");
            Checker.NotNullObject(projection.Select, $"{nameof(projection)}.{nameof(projection.Select)}");

            return QuerySearch(_query, filter, projection)
                .GroupBy(projection.Group)
                .Select(projection.Select);
        }

        public PaginatedResult<TSelect> LimitedSearch<TGroup, TSelect>(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel, TGroup, TSelect> projection)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(limiter, nameof(limiter));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Group, $"{nameof(projection)}.{nameof(projection.Group)}");
            Checker.NotNullObject(projection.Select, $"{nameof(projection)}.{nameof(projection.Select)}");

            var query = QuerySearch(_query, filter, projection);

            // A projeção já foi aplicada em QuerySearch(),
            // por isso não precisa ser repassada aqui
            var (offset, limit, total, result) = QueryPreLimitResult(query, limiter, null);

            return new PaginatedResult<TSelect>(
                result.GroupBy(projection.Group).Select(projection.Select),
                offset,
                limit,
                total
            );
        }

        #endregion
    }
}
