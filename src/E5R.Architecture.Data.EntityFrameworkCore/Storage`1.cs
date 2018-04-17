using System.Collections.Generic;
using System.Linq;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    using Abstractions;

    public class Storage<TModel> : IStorage<Storage<TModel>, TModel>
        where TModel : DataModel<TModel>
    {
        private readonly FullStorage<TModel> _base;

        public Storage()
        {
            _base = new FullStorage<TModel>();
        }

        protected IQueryable<TModel> Read => _base.Read;
        protected WriterDelegate Write => _base.Write;

        public Storage<TModel> Configure(UnderlyingSession session)
        {
            _base.Configure(session);

            return this;
        }

        public TModel Find(TModel data) => _base.Find(data);

        public DataLimiterResult<TModel> Get(DataLimiter<TModel> limiter) => _base.Get(limiter);

        public IEnumerable<TModel> Search(DataReducer<TModel> reducer) => _base.Search(reducer);

        public DataLimiterResult<TModel> LimitedSearch(DataReducer<TModel> reducer,
            DataLimiter<TModel> limiter) => _base.LimitedSearch(reducer, limiter);

        public TModel Create(TModel data) => _base.Create(data);

        public TModel Replace(TModel data) => _base.Replace(data);

        public void Remove(TModel data) => _base.Remove(data);

        public IEnumerable<TModel> BulkCreate(IEnumerable<TModel> data) => _base.BulkCreate(data);

        public IEnumerable<TModel> BulkReplace(IEnumerable<TModel> data) => _base.BulkReplace(data);

        public void BulkRemove(IEnumerable<TModel> data) => _base.BulkRemove(data);

        public void BulkRemoveFromSearch(DataReducer<TModel> reducer) =>
            _base.BulkRemoveFromSearch(reducer);
    }
}
