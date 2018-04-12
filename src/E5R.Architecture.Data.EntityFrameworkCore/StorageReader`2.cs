using System.Collections.Generic;
using System.Linq;
using E5R.Architecture.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public class
        StorageReader<TModel, TIdentifier> : IStorageReader<StorageReader<TModel, TIdentifier>,
            TModel, TIdentifier>
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
        private DbContext _context;
        private IQueryable<TModel> _query;

        public StorageReader<TModel, TIdentifier> ConfigureSession(UnderlyingSession session)
        {
            Checker.NotNullArgument(session, nameof(session));

            _context = session.Get<DbContext>();
            _query = _context.Set<TModel>().AsNoTracking();

            return this;
        }

        public TModel Find(TIdentifier id)
        {
            return _query.SingleOrDefault(w => w.Identifier.Equals(id));
        }

        public IEnumerable<TModel> Get(DataLimiter<TModel, TIdentifier> limiter)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TModel> Search(DataReducer<TModel, TIdentifier> reducer)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TModel> LimitedSearch(DataReducer<TModel, TIdentifier> reducer,
            DataLimiter<TModel, TIdentifier> limiter)
        {
            throw new System.NotImplementedException();
        }
    }
}
