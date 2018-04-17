using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    using Core;
    using Abstractions;

    internal class FullStorage<TModel> : TradableStorage<FullStorage<TModel>>
        where TModel : DataModel<TModel>
    {
        public IQueryable<TModel> Read { get; private set; }
        public WriterDelegate Write { get; private set; }

        public override FullStorage<TModel> Configure(UnderlyingSession session)
        {
            base.Configure(session);

            Read = Context.Set<TModel>().AsNoTracking();
            Write = Context.ChangeTracker.TrackGraph;

            return this;
        }

        #region IStorageReader

        public TModel Find(TModel data)
        {
            Checker.NotNullArgument(data, nameof(data));

            return Read.SingleOrDefault(data.GetIdenifierCriteria());
        }

        public DataLimiterResult<TModel> Get(DataLimiter<TModel> limiter)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));

            return QueryLimitResult(limiter, Read);
        }

        public IEnumerable<TModel> Search(DataReducer<TModel> reducer)
        {
            Checker.NotNullArgument(reducer, nameof(reducer));

            return QuerySearch(reducer);
        }

        public DataLimiterResult<TModel> LimitedSearch(DataReducer<TModel> reducer,
            DataLimiter<TModel> limiter)
        {
            Checker.NotNullArgument(reducer, nameof(reducer));
            Checker.NotNullArgument(limiter, nameof(limiter));

            var result = QuerySearch(reducer);

            return QueryLimitResult(limiter, result);
        }

        #endregion

        #region IStorageWriter

        public TModel Create(TModel data)
        {
            Checker.NotNullArgument(data, nameof(data));

            // TODO: Implementar validação

            Write(data, node => node.Entry.State = EntityState.Added);
            Context.SaveChanges();

            return data;
        }

        public TModel Replace(TModel data)
        {
            Checker.NotNullArgument(data, nameof(data));

            // TODO: Implementar validação

            Write(data, node => node.Entry.State = EntityState.Modified);
            Context.SaveChanges();

            return data;
        }

        public void Remove(TModel data)
        {
            Checker.NotNullArgument(data, nameof(data));

            // TODO: Implementar validação

            Write(data, node => node.Entry.State = EntityState.Deleted);
            Context.SaveChanges();
        }

        #endregion

        public IEnumerable<TModel> BulkCreate(IEnumerable<TModel> data)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TModel> BulkReplace(IEnumerable<TModel> data)
        {
            throw new System.NotImplementedException();
        }

        public void BulkRemove(IEnumerable<TModel> data)
        {
            throw new System.NotImplementedException();
        }

        public void BulkRemoveFromSearch(DataReducer<TModel> reducer)
        {
            throw new System.NotImplementedException();
        }

        #region Auxiliary methods

        private IQueryable<TModel> QuerySearch(DataReducer<TModel> reducer)
        {
            Checker.NotNullArgument(reducer, nameof(reducer));

            var reducerList = reducer.GetReducer();

            Checker.NotNullObject(reducerList, $"reducer.{nameof(reducer.GetReducer)}()");

            return reducerList.Aggregate(Read, (q, w) => q.Where(w));
        }

        private DataLimiterResult<TModel> QueryLimitResult(DataLimiter<TModel> limiter,
            IQueryable<TModel> origin)
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

            return new DataLimiterResult<TModel>(result, count);
        }

        #endregion
    }
}
