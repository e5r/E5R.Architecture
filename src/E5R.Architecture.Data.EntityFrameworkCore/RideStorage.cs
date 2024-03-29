// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using System.Linq;
using E5R.Architecture.Core;
using E5R.Architecture.Core.Exceptions;
using E5R.Architecture.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public class RideStorage<TDataModel> : StorageBase<TDataModel>, IStorageReader<TDataModel>
        where TDataModel : class, IIdentifiable
    {
        private readonly DbContext _dbContext;
        private readonly IQueryable<TDataModel> _query;

        public RideStorage(DbContext dbContext, IQueryable<TDataModel> query)
        {
            Checker.NotNullArgument(dbContext, nameof(dbContext));
            Checker.NotNullArgument(query, nameof(query));

            _dbContext = dbContext;
            _query = query;
        }

        #region IStorageReader for TDataModel

        public TDataModel Find(object[] identifiers, IDataIncludes includes = null)
        {
            Checker.NotNullArgument(identifiers, nameof(identifiers));

            var entityType = _dbContext.Model.FindEntityType(typeof(TDataModel));

            return QueryFind(entityType, _query, identifiers, includes)
                .FirstOrDefault();
        }

        public int CountAll() => _query.Count();

        public int Count(IDataFilter<TDataModel> filter) =>
            QuerySearch(_query, filter, null).Count();

        public TDataModel GetFirst(IDataFilter<TDataModel> filter, IDataIncludes includes = null) =>
            QuerySearch(_query, filter, includes).FirstOrDefault();

        public IEnumerable<TDataModel> GetAll(IDataIncludes includes) =>
            TryApplyIncludes(_query, includes).ToList();

        public PaginatedResult<TDataModel> LimitedGet(IDataLimiter<TDataModel> limiter,
            IDataIncludes includes)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));

            var (offset, limit, total, result) = QueryPreLimitResult(_query, limiter, includes);

            return new PaginatedResult<TDataModel>(
                result.ToList(),
                offset,
                limit,
                total
            );
        }

        public IEnumerable<TDataModel> Search(IDataFilter<TDataModel> filter,
            IDataIncludes includes)
        {
            Checker.NotNullArgument(filter, nameof(filter));

            return QuerySearch(_query, filter, includes).ToList();
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
                result.ToList(),
                offset,
                limit,
                total
            );
        }

        #endregion

        #region IStorageReader for TSelect

        public TSelect Find<TSelect>(TDataModel data,
            IDataProjection<TDataModel, TSelect> projection)
        {
            throw new DataLayerException(
                $"{this.GetType().Name} not implement {nameof(Find)}(data)!");
        }

        public TSelect Find<TSelect>(object identifier,
            IDataProjection<TDataModel, TSelect> projection)
        {
            throw new DataLayerException(
                $"{this.GetType().Name} not implement {nameof(Find)}(identifier)!");
        }

        public TSelect Find<TSelect>(object[] identifiers,
            IDataProjection<TDataModel, TSelect> projection)
        {
            throw new DataLayerException(
                $"{this.GetType().Name} not implement {nameof(Find)}(identifiers)!");
        }

        public TSelect GetFirst<TSelect>(IDataFilter<TDataModel> filter,
            IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Select, () => projection.Select);

            return QuerySearch(_query, filter, projection).Select(projection.Select).FirstOrDefault();
        }

        public IEnumerable<TSelect> GetAll<TSelect>(IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Select,
                $"{nameof(projection)}.{nameof(projection.Select)}");

            return TryApplyIncludes(_query, projection).Select(projection.Select).ToList();
        }

        public PaginatedResult<TSelect> LimitedGet<TSelect>(IDataLimiter<TDataModel> limiter,
            IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Select,
                $"{nameof(projection)}.{nameof(projection.Select)}");

            var (offset, limit, total, result) = QueryPreLimitResult(_query, limiter, projection);

            return new PaginatedResult<TSelect>(
                result.Select(projection.Select).ToList(),
                offset,
                limit,
                total
            );
        }

        public IEnumerable<TSelect> Search<TSelect>(IDataFilter<TDataModel> filter,
            IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Select,
                $"{nameof(projection)}.{nameof(projection.Select)}");

            return QuerySearch(_query, filter, projection)
                .Select(projection.Select)
                .ToList();
        }

        public PaginatedResult<TSelect> LimitedSearch<TSelect>(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(limiter, nameof(limiter));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Select,
                $"{nameof(projection)}.{nameof(projection.Select)}");

            var query = QuerySearch(_query, filter, projection);

            // A projeção já foi aplicada em QuerySearch(),
            // por isso não precisa ser repassada aqui
            var (offset, limit, total, result) = QueryPreLimitResult(query, limiter, null);

            return new PaginatedResult<TSelect>(
                result.Select(projection.Select).ToList(),
                offset,
                limit,
                total
            );
        }

        #endregion

        #region IStorageReader for TGroup

        public IEnumerable<TSelect> GetAll<TGroup, TSelect>(
            IDataProjection<TDataModel, TGroup, TSelect> projection)
        {
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Group,
                $"{nameof(projection)}.{nameof(projection.Group)}");
            Checker.NotNullObject(projection.Select,
                $"{nameof(projection)}.{nameof(projection.Select)}");

            return TryApplyIncludes(_query, projection)
                .GroupBy(projection.Group)
                .Select(projection.Select)
                .ToList();
        }

        public PaginatedResult<TSelect> LimitedGet<TGroup, TSelect>(
            IDataLimiter<TDataModel> limiter,
            IDataProjection<TDataModel, TGroup, TSelect> projection)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Group,
                $"{nameof(projection)}.{nameof(projection.Group)}");
            Checker.NotNullObject(projection.Select,
                $"{nameof(projection)}.{nameof(projection.Select)}");

            var (offset, limit, total, result) = QueryPreLimitResult(_query, limiter, projection);

            return new PaginatedResult<TSelect>(
                result.GroupBy(projection.Group).Select(projection.Select).ToList(),
                offset,
                limit,
                total
            );
        }

        public IEnumerable<TSelect> Search<TGroup, TSelect>(IDataFilter<TDataModel> filter,
            IDataProjection<TDataModel, TGroup, TSelect> projection)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Group,
                $"{nameof(projection)}.{nameof(projection.Group)}");
            Checker.NotNullObject(projection.Select,
                $"{nameof(projection)}.{nameof(projection.Select)}");

            return QuerySearch(_query, filter, projection)
                .GroupBy(projection.Group)
                .Select(projection.Select)
                .ToList();
        }

        public PaginatedResult<TSelect> LimitedSearch<TGroup, TSelect>(
            IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter,
            IDataProjection<TDataModel, TGroup, TSelect> projection)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(limiter, nameof(limiter));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Group,
                $"{nameof(projection)}.{nameof(projection.Group)}");
            Checker.NotNullObject(projection.Select,
                $"{nameof(projection)}.{nameof(projection.Select)}");

            var query = QuerySearch(_query, filter, projection);

            // A projeção já foi aplicada em QuerySearch(),
            // por isso não precisa ser repassada aqui
            var (offset, limit, total, result) = QueryPreLimitResult(query, limiter, null);

            return new PaginatedResult<TSelect>(
                result.GroupBy(projection.Group).Select(projection.Select).ToList(),
                offset,
                limit,
                total
            );
        }

        #endregion
    }
}
