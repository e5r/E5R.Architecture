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

        #region IStorageReader

        public TDataModel Find(TDataModel data, IDataProjection<TDataModel> projection)
        {
            Checker.NotNullArgument(data, nameof(data));

            return Find(data.IdentifierValues, projection);
        }

        public TDataModel Find(object identifier, IDataProjection<TDataModel> projection)
            => Find(new object[] { identifier }, projection);

        public TDataModel Find(object[] identifiers, IDataProjection<TDataModel> projection)
        {
            Checker.NotNullArgument(identifiers, nameof(identifiers));

            var primaryKeys = Context.Model.FindEntityType(typeof(TDataModel))
                .FindPrimaryKey()
                .Properties;

            if (primaryKeys.Count() != identifiers.Count())
            {
                // TODO: Aplicar localização
                throw new InvalidOperationException($"Quantidade de chaves primárias configuradas em {typeof(TDataModel)} diferente do esperado.");
            }

            var filter = new LinqDataFilter<TDataModel>();
            var param = Expression.Parameter(typeof(TDataModel), "e");

            foreach (var (pk, idx) in primaryKeys.Select((pk, idx) => (pk, idx)))
            {
                var predicate = Expression.Lambda<Func<TDataModel, bool>>(
                    Expression.Equal(
                        Expression.PropertyOrField(param, pk.Name),
                        Expression.Constant(identifiers[idx])
                    ),
                    param
                );

                filter.AddFilter(predicate);
            }

            return QuerySearch(Query, filter, projection).FirstOrDefault();
        }

        public DataLimiterResult<TDataModel> Get(IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel> projection)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));

            return QueryLimitResult(limiter, Query, projection);
        }

        public IEnumerable<TDataModel> Search(IDataFilter<TDataModel> filter, IDataProjection<TDataModel> projection)
        {
            Checker.NotNullArgument(filter, nameof(filter));

            return QuerySearch(Query, filter, projection);
        }

        public DataLimiterResult<TDataModel> LimitedSearch(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel> projection)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(limiter, nameof(limiter));

            var result = QuerySearch(Query, filter, projection);

            // A projeção já foi aplicada em QuerySearch(), por isso não precisa
            // ser repassada aqui
            return QueryLimitResult(limiter, result, null);
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
