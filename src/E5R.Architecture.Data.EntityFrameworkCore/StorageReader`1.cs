using System.Collections.Generic;
using System.Linq;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    using Abstractions;

    public class StorageReader<TModel> : IStorageReader<StorageReader<TModel>, TModel>
        where TModel : DataModel<TModel>
    {
        private readonly FullStorage<TModel> _base;

        public StorageReader()
        {
            _base = new FullStorage<TModel>();
        }

        protected IQueryable<TModel> Read => _base.Read;

        public StorageReader<TModel> Configure(UnderlyingSession session)
        {
            _base.Configure(session);

            return this;
        }

        public TModel Find(TModel data) => _base.Find(data);

        public DataLimiterResult<TModel> Get(DataLimiter<TModel> limiter) => _base.Get(limiter);

        public IEnumerable<TModel> Search(DataReducer<TModel> reducer) => _base.Search(reducer);

        public DataLimiterResult<TModel> LimitedSearch(DataReducer<TModel> reducer,
            DataLimiter<TModel> limiter) => _base.LimitedSearch(reducer, limiter);
    }
}
