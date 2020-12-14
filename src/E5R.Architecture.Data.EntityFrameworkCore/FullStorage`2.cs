// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public class FullStorage<TDbContext, TDataModel> : StorageBase<TDataModel>
        where TDataModel : class, IDataModel
        where TDbContext : DbContext
    {
        protected TDbContext Context { get; private set; }
        protected DbSet<TDataModel> Set { get; private set; }
        protected IQueryable<TDataModel> Query { get; private set; }
        protected WriterDelegate Write { get; private set; }

        public FullStorage(TDbContext context)
        {
            Checker.NotNullArgument(context, nameof(context));

            Context = context;

            Set = Context.Set<TDataModel>();
            Query = Set.AsNoTracking();
            Write = Context.ChangeTracker.TrackGraph;
        }

        #region IStorageReader for TDataModel

        public TDataModel Find(TDataModel data, IDataProjection projection)
        {
            Checker.NotNullArgument(data, nameof(data));

            return Find(data.IdentifierValues, projection);
        }

        public TDataModel Find(object identifier, IDataProjection projection)
            => Find(new object[] { identifier }, projection);

        public TDataModel Find(object[] identifiers, IDataProjection projection)
        {
            Checker.NotNullArgument(identifiers, nameof(identifiers));

            var entityType = Context.Model.FindEntityType(typeof(TDataModel));

            return QueryFind(entityType, Query, identifiers, projection)
                .FirstOrDefault();
        }

        public IEnumerable<TDataModel> GetAll(IDataProjection projection) => TryApplyProjection(Query, projection);

        public PaginatedResult<TDataModel> LimitedGet(IDataLimiter<TDataModel> limiter, IDataProjection projection)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));

            var (offset, limit, total, result) = QueryPreLimitResult(Query, limiter, projection);

            return new PaginatedResult<TDataModel>(
                result,
                offset,
                limit,
                total
            );
        }

        public IEnumerable<TDataModel> Search(IDataFilter<TDataModel> filter, IDataProjection projection)
        {
            Checker.NotNullArgument(filter, nameof(filter));

            return QuerySearch(Query, filter, projection);
        }

        public PaginatedResult<TDataModel> LimitedSearch(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataProjection projection)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(limiter, nameof(limiter));

            var query = QuerySearch(Query, filter, projection);

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
            Checker.NotNullArgument(data, nameof(data));

            return Find(data.IdentifierValues, projection);
        }

        public TSelect Find<TSelect>(object identifier, IDataProjection<TDataModel, TSelect> projection)
            => Find(new object[] { identifier }, projection);

        public TSelect Find<TSelect>(object[] identifiers, IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(identifiers, nameof(identifiers));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Select, $"{nameof(projection)}.{nameof(projection.Select)}");

            var entityType = Context.Model.FindEntityType(typeof(TDataModel));

            return QueryFind(entityType, Query, identifiers, projection)
                .Select(projection.Select)
                .FirstOrDefault();
        }

        public IEnumerable<TSelect> GetAll<TSelect>(IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Select, $"{nameof(projection)}.{nameof(projection.Select)}");

            return TryApplyProjection(Query, projection).Select(projection.Select);
        }

        public PaginatedResult<TSelect> LimitedGet<TSelect>(IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Select, $"{nameof(projection)}.{nameof(projection.Select)}");

            var (offset, limit, total, result) = QueryPreLimitResult(Query, limiter, projection);

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

            return QuerySearch(Query, filter, projection)
                .Select(projection.Select);
        }

        public PaginatedResult<TSelect> LimitedSearch<TSelect>(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(limiter, nameof(limiter));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Select, $"{nameof(projection)}.{nameof(projection.Select)}");

            var query = QuerySearch(Query, filter, projection);

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

        #region IStorageWriter

        public TDataModel Create(TDataModel data)
        {
            Checker.NotNullArgument(data, nameof(data));

            // TODO: Implementar validação

            Write(data, node => node.Entry.State = EntityState.Added);
            Context.SaveChanges();

            return data;
        }

        public TDataModel Replace(TDataModel data)
        {
            Checker.NotNullArgument(data, nameof(data));

            // TODO: Implementar validação

            Write(data, node => node.Entry.State = EntityState.Modified);
            Context.SaveChanges();

            return data;
        }

        public void Remove(TDataModel data)
        {
            Checker.NotNullArgument(data, nameof(data));

            // TODO: Implementar validação

            Write(data, node => node.Entry.State = EntityState.Deleted);
            Context.SaveChanges();
        }

        #endregion

        #region IBulkStorageWriter

        public IEnumerable<TDataModel> BulkCreate(IEnumerable<TDataModel> data)
        {
            Checker.NotNullArgument(data, nameof(data));

            // TODO: Implementar validação

            // TODO: Utilizar Bulk com alguma biblioteca ao invés dessa iteração
            foreach (var d in data)
            {
                Write(d, node => node.Entry.State = EntityState.Added);
            }

            Context.SaveChanges();

            return data;
        }

        public IEnumerable<TDataModel> BulkReplace(IEnumerable<TDataModel> data)
        {
            Checker.NotNullArgument(data, nameof(data));

            // TODO: Implementar validação

            // TODO: Utilizar Bulk com alguma biblioteca ao invés dessa iteração
            foreach (var d in data)
            {
                Write(d, node => node.Entry.State = EntityState.Modified);
            }

            Context.SaveChanges();

            return data;
        }

        public void BulkRemove(IEnumerable<TDataModel> data)
        {
            Checker.NotNullArgument(data, nameof(data));

            // TODO: Implementar validação

            // TODO: Utilizar Bulk com alguma biblioteca ao invés dessa iteração
            foreach (var d in data)
            {
                Write(d, node => node.Entry.State = EntityState.Deleted);
            }

            Context.SaveChanges();
        }

        public void BulkRemove(IDataFilter<TDataModel> filter)
        {
            Checker.NotNullArgument(filter, nameof(filter));

            // TODO: Implementar validação

            // TODO: Refatorar com [StorageReader/QuerySearch]
            var filterList = filter.GetFilter();

            Checker.NotNullObject(filterList, $"filter.{nameof(filter.GetFilter)}()");

            var search = filterList.Aggregate(Query, (q, w) => q.Where(w));

            foreach (var d in search)
            {
                Write(d, node => node.Entry.State = EntityState.Deleted);
            }

            Context.SaveChanges();
        }

        #endregion
    }
}
