// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public class FullStorageByProperty<TDbContext, TDataModel>
        where TDataModel : class, IDataModel
        where TDbContext : DbContext
    {
        protected TDbContext Context { get; private set; }
        protected DbSet<TDataModel> Set { get; private set; }
        protected IQueryable<TDataModel> Query { get; private set; }
        protected WriterDelegate Write { get; private set; }

        public FullStorageByProperty(UnitOfWorkProperty<TDbContext> context)
        {
            Checker.NotNullArgument(context, nameof(context));

            Context = context;

            Set = Context.Set<TDataModel>();
            Query = Set.AsNoTracking();
            Write = Context.ChangeTracker.TrackGraph;
        }

        #region IStorageReader

        public TDataModel Find(TDataModel data)
        {
            Checker.NotNullArgument(data, nameof(data));

            return Set.Find(data.IdentifierValues);
        }

        public DataLimiterResult<TDataModel> Get(IDataLimiter<TDataModel> limiter, IEnumerable<IDataProjection> projections)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));

            return QueryLimitResult(limiter, Query, projections);
        }

        public IEnumerable<TDataModel> Search(IDataFilter<TDataModel> filter, IEnumerable<IDataProjection> projections)
        {
            Checker.NotNullArgument(filter, nameof(filter));

            return QuerySearch(filter, projections);
        }

        public DataLimiterResult<TDataModel> LimitedSearch(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IEnumerable<IDataProjection> projections)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(limiter, nameof(limiter));

            var result = QuerySearch(filter, projections);

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

        #region Auxiliary methods

        IQueryable<TDataModel> QuerySearch(IDataFilter<TDataModel> filter, IEnumerable<IDataProjection> projections)
        {
            Checker.NotNullArgument(filter, nameof(filter));

            var filterList = filter.GetFilter();

            Checker.NotNullObject(filterList, $"filter.{nameof(filter.GetFilter)}()");

            var query = filterList.Aggregate(Query, (q, w) => q.Where(w));

            return TryApplyProjections(query, projections);
        }

        DataLimiterResult<TDataModel> QueryLimitResult(IDataLimiter<TDataModel> limiter,
            IQueryable<TDataModel> origin, IEnumerable<IDataProjection> projections)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));

            // Ensures offset range
            if (limiter.OffsetLimit.HasValue &&
                (limiter.OffsetLimit.Value == 0 || limiter.OffsetLimit.Value > int.MaxValue))
            {
                throw new ArgumentOutOfRangeException($"limiter.{limiter.OffsetLimit}");
            }

            var result = TryApplyProjections(origin, projections);

            IOrderedQueryable<TDataModel> orderBy = null;

            foreach (var sorter in limiter.GetSorters())
            {
                if (orderBy == null)
                {
                    if (sorter.Descending)
                        orderBy = result.OrderByDescending(sorter.Sorter);
                    else
                        orderBy = result.OrderBy(sorter.Sorter);
                }
                else
                {
                    if (sorter.Descending)
                        orderBy = orderBy.ThenByDescending(sorter.Sorter);
                    else
                        orderBy = orderBy.ThenBy(sorter.Sorter);
                }
            }

            result = orderBy ?? result;

            uint offset = 0;
            uint limit = 0;
            int total = result.Count();

            if (limiter.OffsetBegin.HasValue)
            {
                offset = limiter.OffsetBegin.Value;
                result = result.Skip(Convert.ToInt32(offset));
            }

            if (limiter.OffsetLimit.HasValue)
            {
                limit = limiter.OffsetLimit.Value;
                result = result.Take(Convert.ToInt32(limit));
            }
            else
            {
                limit = Convert.ToUInt32(total);
            }

            return new DataLimiterResult<TDataModel>(result, offset, limit, total);
        }

        IQueryable<TDataModel> TryApplyProjections(IQueryable<TDataModel> query, IEnumerable<IDataProjection> projections)
        {
            foreach (var projection in (projections ?? Enumerable.Empty<IDataProjection>()))
            {
                query = query.Include(projection.GetProjectionString());
            }

            return query;
        }

        #endregion
    }
}
