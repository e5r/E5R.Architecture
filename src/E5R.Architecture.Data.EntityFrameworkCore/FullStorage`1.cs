// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    using Core;
    using Abstractions;

    internal class FullStorage<TDataModel> : TradableStorage
        where TDataModel : class, IDataModel
    {
        public DbSet<TDataModel> Set { get; private set; }
        public IQueryable<TDataModel> Query { get; private set; }
        public WriterDelegate Write { get; private set; }

        public override void Configure(UnderlyingSession session)
        {
            base.Configure(session);

            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

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

        public IEnumerable<TDataModel> Search(DataReducer<TDataModel> reducer)
        {
            Checker.NotNullArgument(reducer, nameof(reducer));

            return QuerySearch(reducer);
        }

        public DataLimiterResult<TDataModel> LimitedSearch(DataReducer<TDataModel> reducer,
            DataLimiter<TDataModel> limiter)
        {
            Checker.NotNullArgument(reducer, nameof(reducer));
            Checker.NotNullArgument(limiter, nameof(limiter));

            var result = QuerySearch(reducer);

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

        public IEnumerable<TDataModel> BulkCreate(IEnumerable<TDataModel> data)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TDataModel> BulkReplace(IEnumerable<TDataModel> data)
        {
            throw new System.NotImplementedException();
        }

        public void BulkRemove(IEnumerable<TDataModel> data)
        {
            throw new System.NotImplementedException();
        }

        public void BulkRemoveFromSearch(DataReducer<TDataModel> reducer)
        {
            throw new System.NotImplementedException();
        }

        #region Auxiliary methods

        private IQueryable<TDataModel> QuerySearch(DataReducer<TDataModel> reducer)
        {
            Checker.NotNullArgument(reducer, nameof(reducer));

            var reducerList = reducer.GetReducer();

            Checker.NotNullObject(reducerList, $"reducer.{nameof(reducer.GetReducer)}()");

            return reducerList.Aggregate(Query, (q, w) => q.Where(w));
        }

        private DataLimiterResult<TDataModel> QueryLimitResult(DataLimiter<TDataModel> limiter,
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
