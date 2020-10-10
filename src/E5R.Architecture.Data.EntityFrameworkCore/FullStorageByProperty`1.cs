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

        public DataLimiterResult<TDataModel> Get(DataLimiter<TDataModel> limiter)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));

            return QueryLimitResult(limiter, Query);
        }

        public IEnumerable<TDataModel> Search(DataFilter<TDataModel> filter)
        {
            Checker.NotNullArgument(filter, nameof(filter));

            return QuerySearch(filter);
        }

        public DataLimiterResult<TDataModel> LimitedSearch(DataFilter<TDataModel> filter,
            DataLimiter<TDataModel> limiter)
        {
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(limiter, nameof(limiter));

            var result = QuerySearch(filter);

            return QueryLimitResult(limiter, result);
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

        public void BulkRemoveFromSearch(DataFilter<TDataModel> filter)
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

        public IQueryable<TDataModel> QuerySearch(DataFilter<TDataModel> filter)
        {
            Checker.NotNullArgument(filter, nameof(filter));

            var filterList = filter.GetFilter();

            Checker.NotNullObject(filterList, $"filter.{nameof(filter.GetFilter)}()");

            return filterList.Aggregate(Query, (q, w) => q.Where(w));
        }

        public DataLimiterResult<TDataModel> QueryLimitResult(DataLimiter<TDataModel> limiter,
            IQueryable<TDataModel> origin)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));

            // Ensures offset range
            if (0 > limiter.OffsetBegin)
            {
                throw new ArgumentOutOfRangeException($"limiter.{limiter.OffsetBegin}");
            }

            if (0 > limiter.OffsetEnd)
            {
                throw new ArgumentOutOfRangeException($"limiter.{limiter.OffsetEnd}");
            }

            if (0 > limiter.OffsetEnd - limiter.OffsetBegin)
            {
                throw new ArgumentOutOfRangeException(
                    $"limiter.{limiter.OffsetEnd} - limiter.{limiter.OffsetBegin}");
            }

            var sorter = limiter.GetSorter();

            Checker.NotNullObject(sorter, $"limiter.{nameof(limiter.GetSorter)}()");

            var ordered = !limiter.Descending
                ? origin.OrderBy(sorter)
                : origin.OrderByDescending(sorter);

            var count = ordered.Count();
            var result = ordered
                .Skip(limiter.OffsetBegin)
                .Take(limiter.OffsetEnd - limiter.OffsetBegin);

            return new DataLimiterResult<TDataModel>(result, count);
        }

        #endregion
    }
}
