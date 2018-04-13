using System.Collections.Generic;
using System.Linq;
using E5R.Architecture.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public class StorageReader<TModel> : IStorageReader<StorageReader<TModel>, TModel>
        where TModel : DataModel<TModel>
    {
        private DbContext _context;
        private IQueryable<TModel> _query;

        public StorageReader<TModel> ConfigureSession(UnderlyingSession session)
        {
            Checker.NotNullArgument(session, nameof(session));

            _context = session.Get<DbContext>();
            _query = _context.Set<TModel>().AsNoTracking();

            return this;
        }

        public TModel Find(TModel data)
        {
            return _query.SingleOrDefault(data.GetIdenifierCriteria());
        }

        public IEnumerable<TModel> Get(DataLimiter<TModel> limiter)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TModel> Search(DataReducer<TModel> reducer)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TModel> LimitedSearch(DataReducer<TModel> reducer,
            DataLimiter<TModel> limiter)
        {
            throw new System.NotImplementedException();
        }
    }
}
