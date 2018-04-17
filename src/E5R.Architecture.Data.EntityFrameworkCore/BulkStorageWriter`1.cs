using System.Collections.Generic;
using System.Linq;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public class BulkStorageWriter<TModel> : TradableStorage<BulkStorageWriter<TModel>>,
        IBulkStorageWriter<BulkStorageWriter<TModel>, TModel>
        where TModel : DataModel<TModel>
    {
        private IQueryable<TModel> _read;

        protected WriterDelegate Write { get; private set; }

        public override BulkStorageWriter<TModel> Configure(UnderlyingSession session)
        {
            base.Configure(session);

            Write = Context.ChangeTracker.TrackGraph;
            _read = Context.Set<TModel>().AsNoTracking();

            return this;
        }

        public IEnumerable<TModel> BulkCreate(IEnumerable<TModel> data)
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

        public IEnumerable<TModel> BulkReplace(IEnumerable<TModel> data)
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

        public void BulkRemove(IEnumerable<TModel> data)
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

        public void BulkRemoveFromSearch(DataReducer<TModel> reducer)
        {
            Checker.NotNullArgument(reducer, nameof(reducer));

            // TODO: Implementar validação

            // TODO: Refatorar com [StorageReader/QuerySearch]
            var reducerList = reducer.GetReducer();

            Checker.NotNullObject(reducerList, $"reducer.{nameof(reducer.GetReducer)}()");

            var search = reducerList.Aggregate(_read, (q, w) => q.Where(w));

            foreach (var d in search)
            {
                Write(d, node => node.Entry.State = EntityState.Deleted);
            }

            Context.SaveChanges();
        }
    }
}
