using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    using Abstractions;

    public class StorageReader<TDataModel> : IStorageReader<StorageReader<TDataModel>, TDataModel>
        where TDataModel : class, IDataModel
    {
        private readonly FullStorage<TDataModel> _base;

        public StorageReader()
        {
            _base = new FullStorage<TDataModel>();
        }

        protected DbSet<TDataModel> Set => _base.Set;
        protected IQueryable<TDataModel> Query => _base.Query;

        public StorageReader<TDataModel> Configure(UnderlyingSession session)
        {
            _base.Configure(session);

            return this;
        }

        public TDataModel Find(TDataModel data) => _base.Find(data);

        public DataLimiterResult<TDataModel> Get(DataLimiter<TDataModel> limiter) => _base.Get(limiter);

        public IEnumerable<TDataModel> Search(DataReducer<TDataModel> reducer) => _base.Search(reducer);

        public DataLimiterResult<TDataModel> LimitedSearch(DataReducer<TDataModel> reducer,
            DataLimiter<TDataModel> limiter) => _base.LimitedSearch(reducer, limiter);
    }
}
