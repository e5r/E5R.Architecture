// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Core.Exceptions;
using E5R.Architecture.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public class FullStorage<TDbContext, TDataModel> : StorageBase<TDataModel>
        where TDataModel : class, IIdentifiable
        where TDbContext : DbContext
    {
        protected TDbContext Context { get; private set; }
        protected DbSet<TDataModel> Set { get; private set; }
        protected IQueryable<TDataModel> Query { get; private set; }

        public FullStorage(TDbContext context)
        {
            Checker.NotNullArgument(context, nameof(context));

            Context = context;

            Set = Context.Set<TDataModel>();
            Query = Set.AsNoTracking();
        }

        #region IStorageReader for TDataModel

        public TDataModel Find(TDataModel data, IDataIncludes includes)
        {
            Checker.NotNullArgument(data, nameof(data));

            return Find(data.Identifiers, includes);
        }

        public TDataModel Find(object identifier, IDataIncludes includes)
            => Find(new object[] {identifier}, includes);

        public TDataModel Find(object[] identifiers, IDataIncludes includes)
        {
            Checker.NotNullArgument(identifiers, nameof(identifiers));

            var entityType = Context.Model.FindEntityType(typeof(TDataModel));

            return QueryFind(entityType, Query, identifiers, includes)
                .FirstOrDefault();
        }

        public int CountAll() => Query.Count();

        public int Count(IDataFilter<TDataModel> filter) =>
            QuerySearch(Query, filter, null).Count();

        public IEnumerable<TDataModel> GetAll(IDataIncludes includes) =>
            TryApplyIncludes(Query, includes);

        public PaginatedResult<TDataModel> LimitedGet(IDataLimiter<TDataModel> limiter,
            IDataIncludes includes)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));

            var (offset, limit, total, result) = QueryPreLimitResult(Query, limiter, includes);

            return new PaginatedResult<TDataModel>(
                result,
                offset,
                limit,
                total
            );
        }

        public IEnumerable<TDataModel> Search(IDataFilter<TDataModel> filter,
            IDataIncludes includes)
        {
            Checker.NotNullArgument(filter, nameof(filter));

            return QuerySearch(Query, filter, includes);
        }

        public PaginatedResult<TDataModel> LimitedSearch(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataIncludes includes)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(limiter, nameof(limiter));

            var query = QuerySearch(Query, filter, includes);

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

        public TSelect Find<TSelect>(TDataModel data,
            IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(data, nameof(data));

            return Find(data.Identifiers, projection);
        }

        public TSelect Find<TSelect>(object identifier,
            IDataProjection<TDataModel, TSelect> projection)
            => Find(new object[] {identifier}, projection);

        public TSelect Find<TSelect>(object[] identifiers,
            IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(identifiers, nameof(identifiers));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Select,
                $"{nameof(projection)}.{nameof(projection.Select)}");

            var entityType = Context.Model.FindEntityType(typeof(TDataModel));

            return QueryFind(entityType, Query, identifiers, projection)
                .Select(projection.Select)
                .FirstOrDefault();
        }

        public IEnumerable<TSelect> GetAll<TSelect>(IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Select,
                $"{nameof(projection)}.{nameof(projection.Select)}");

            return TryApplyIncludes(Query, projection).Select(projection.Select);
        }

        public PaginatedResult<TSelect> LimitedGet<TSelect>(IDataLimiter<TDataModel> limiter,
            IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Select,
                $"{nameof(projection)}.{nameof(projection.Select)}");

            var (offset, limit, total, result) = QueryPreLimitResult(Query, limiter, projection);

            return new PaginatedResult<TSelect>(
                result.Select(projection.Select),
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

            return QuerySearch(Query, filter, projection)
                .Select(projection.Select);
        }

        public PaginatedResult<TSelect> LimitedSearch<TSelect>(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel, TSelect> projection)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(limiter, nameof(limiter));
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Select,
                $"{nameof(projection)}.{nameof(projection.Select)}");

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

        #region IStorageReader for TGroup

        public IEnumerable<TSelect> GetAll<TGroup, TSelect>(
            IDataProjection<TDataModel, TGroup, TSelect> projection)
        {
            Checker.NotNullArgument(projection, nameof(projection));
            Checker.NotNullObject(projection.Group,
                $"{nameof(projection)}.{nameof(projection.Group)}");
            Checker.NotNullObject(projection.Select,
                $"{nameof(projection)}.{nameof(projection.Select)}");

            return TryApplyIncludes(Query, projection)
                .GroupBy(projection.Group)
                .Select(projection.Select);
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

            var (offset, limit, total, result) = QueryPreLimitResult(Query, limiter, projection);

            return new PaginatedResult<TSelect>(
                result.GroupBy(projection.Group).Select(projection.Select),
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

            return QuerySearch(Query, filter, projection)
                .GroupBy(projection.Group)
                .Select(projection.Select);
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

            var query = QuerySearch(Query, filter, projection);

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

        #region IStorageWriter

        public TDataModel Create(TDataModel data)
        {
            Checker.NotNullArgument(data, nameof(data));

            // TODO: Implementar validação

            Context.Entry(data).State = EntityState.Added;
            Context.SaveChanges();

            // Desanexamos o objeto do contexto após manipulação
            Context.Entry(data).State = EntityState.Detached;

            return data;
        }

        public TDataModel Replace(TDataModel data)
        {
            Checker.NotNullArgument(data, nameof(data));

            // TODO: Implementar validação

            Context.Entry(data).State = EntityState.Modified;
            Context.SaveChanges();

            // Desanexamos o objeto do contexto após manipulação
            Context.Entry(data).State = EntityState.Detached;

            return data;
        }

        public void Remove(object identifier)
        {
            Checker.NotNullArgument(identifier, nameof(identifier));

            var targetData = Find(identifier, null);

            if (targetData == null)
            {
                // TODO: Implementar i18n/l10n
                throw new DataLayerException("Object to remove not found in storage");
            }

            Remove(targetData);
        }

        public void Remove(object[] identifiers)
        {
            Checker.NotNullArgument(identifiers, nameof(identifiers));

            var targetData = Find(identifiers, null);

            if (targetData == null)
            {
                // TODO: Implementar i18n/l10n
                throw new DataLayerException("Object to remove not found in storage");
            }

            Remove(targetData);
        }

        public void Remove(TDataModel data)
        {
            Checker.NotNullArgument(data, nameof(data));

            // TODO: Implementar validação

            Context.Entry(data).State = EntityState.Deleted;
            Context.SaveChanges();

            // Desanexamos o objeto do contexto após manipulação
            Context.Entry(data).State = EntityState.Detached;
        }

        public TDataModel Update<TUpdated>(object identifier, TUpdated updated)
        {
            Checker.NotNullArgument(identifier, nameof(identifier));
            Checker.NotNullArgument(updated, nameof(updated));

            return Update<TUpdated>(new[] {identifier}, _ => updated);
        }

        public TDataModel Update<TUpdated>(object identifier,
            Expression<Func<TDataModel, TUpdated>> updateExpression)
        {
            Checker.NotNullArgument(identifier, nameof(identifier));
            Checker.NotNullArgument(updateExpression, nameof(updateExpression));

            return Update(new[] {identifier}, updateExpression);
        }

        public TDataModel Update<TUpdated>(object[] identifiers, TUpdated updated)
        {
            Checker.NotNullArgument(identifiers, nameof(identifiers));
            Checker.NotNullArgument(updated, nameof(updated));

            return Update(identifiers, _ => updated);
        }

        public TDataModel Update<TUpdated>(object[] identifiers,
            Expression<Func<TDataModel, TUpdated>> updateExpression)
        {
            Checker.NotNullArgument(identifiers, nameof(identifiers));
            Checker.NotNullArgument(updateExpression, nameof(updateExpression));

            var targetData = Find(identifiers, null);

            if (targetData == null)
            {
                // TODO: Implementar i18n/l10n
                throw new DataLayerException("Object to update not found in storage");
            }

            var updatedFactory = updateExpression.Compile();
            var updated = updatedFactory(targetData);

            CopyProperties(from: updated, to: targetData);

            Context.Entry(targetData).State = EntityState.Modified;
            Context.SaveChanges();

            // Desanexamos o objeto do contexto após manipulação
            Context.Entry(targetData).State = EntityState.Detached;

            return targetData;
        }

        #endregion

        #region IBulkStorageWriter

        public IEnumerable<TDataModel> BulkCreate(IEnumerable<TDataModel> data)
        {
            Checker.NotNullArgument(data, nameof(data));

            // TODO: Implementar validação

            var affectedObjects = new List<TDataModel>();

            // TODO: Utilizar Bulk com alguma biblioteca ao invés dessa iteração
            foreach (var d in data)
            {
                Context.Entry(d).State = EntityState.Added;
                affectedObjects.Add(d);
            }

            Context.SaveChanges();

            // Desanexamos os objetos do contexto após manipulação
            affectedObjects.ForEach(_ => Context.Entry(_).State = EntityState.Detached);

            return affectedObjects;
        }

        public IEnumerable<TDataModel> BulkReplace(IEnumerable<TDataModel> data)
        {
            Checker.NotNullArgument(data, nameof(data));

            // TODO: Implementar validação

            var affectedObjects = new List<TDataModel>();

            // TODO: Utilizar Bulk com alguma biblioteca ao invés dessa iteração
            foreach (var d in data)
            {
                Context.Entry(d).State = EntityState.Modified;
                affectedObjects.Add(d);
            }

            Context.SaveChanges();

            // Desanexamos os objetos do contexto após manipulação
            affectedObjects.ForEach(_ => Context.Entry(_).State = EntityState.Detached);

            return affectedObjects;
        }

        public void BulkRemove(IEnumerable<TDataModel> data)
        {
            Checker.NotNullArgument(data, nameof(data));

            // TODO: Implementar validação

            var affectedObjects = new List<TDataModel>();

            // TODO: Utilizar Bulk com alguma biblioteca ao invés dessa iteração
            foreach (var d in data)
            {
                Context.Entry(d).State = EntityState.Deleted;
                affectedObjects.Add(d);
            }

            Context.SaveChanges();

            // Desanexamos os objetos do contexto após manipulação
            affectedObjects.ForEach(_ => Context.Entry(_).State = EntityState.Detached);
        }

        public void BulkRemove(IDataFilter<TDataModel> filter)
            => BulkRemove(QuerySearch(Query, filter, null));

        public IEnumerable<TDataModel> BulkUpdate<TUpdated>(IDataFilter<TDataModel> filter,
            TUpdated updated)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(updated, nameof(updated));

            return BulkUpdate(filter, _ => updated);
        }

        public IEnumerable<TDataModel> BulkUpdate<TUpdated>(IDataFilter<TDataModel> filter,
            Expression<Func<TDataModel, TUpdated>> updateExpression)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(updateExpression, nameof(updateExpression));

            var searchResult = QuerySearch(Query, filter, null);

            if (!searchResult.Any())
            {
                // TODO: Implementar i18n/l10n
                throw new DataLayerException("No objects found in storage to update");
            }

            var affectedObjects = new List<TDataModel>();
            var updatedFactory = updateExpression.Compile();

            foreach (var targetData in searchResult)
            {
                // TODO: Usar uma biblioteca de terceiro para obter melhor performance.
                //       Exemplos de biblioteca de terceiro:
                //       - https://github.com/borisdj/EFCore.BulkExtensions, ou 
                //       - https://github.com/zzzprojects/EntityFramework-Plus
                var updated = updatedFactory(targetData);

                CopyProperties(from: updated, to: targetData);

                Context.Entry(targetData).State = EntityState.Modified;
                affectedObjects.Add(targetData);
            }

            Context.SaveChanges();

            // Desanexamos os objetos do contexto após manipulação
            affectedObjects.ForEach(_ => Context.Entry(_).State = EntityState.Detached);

            return affectedObjects;
        }

        #endregion
    }
}
