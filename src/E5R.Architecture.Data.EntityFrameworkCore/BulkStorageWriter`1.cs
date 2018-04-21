using System.Collections.Generic;
using System.Linq;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public class BulkStorageWriter<TDataModel> : TradableStorage, IBulkStorageWriter<TDataModel>
        where TDataModel : class, IDataModel
    {
        private readonly FullStorage<TDataModel> _base;

        public BulkStorageWriter() => _base = new FullStorage<TDataModel>();

        protected WriterDelegate Write => _base.Write;
        private IQueryable<TDataModel> Query => _base.Query;

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

        public void BulkRemoveFromSearch(DataReducer<TDataModel> reducer)
        {
            Checker.NotNullArgument(reducer, nameof(reducer));

            // TODO: Implementar validação

            // TODO: Refatorar com [StorageReader/QuerySearch]
            var reducerList = reducer.GetReducer();

            Checker.NotNullObject(reducerList, $"reducer.{nameof(reducer.GetReducer)}()");

            var search = reducerList.Aggregate(Query, (q, w) => q.Where(w));

            foreach (var d in search)
            {
                Write(d, node => node.Entry.State = EntityState.Deleted);
            }

            Context.SaveChanges();
        }
    }
}
