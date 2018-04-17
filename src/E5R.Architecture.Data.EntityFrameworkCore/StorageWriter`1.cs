using E5R.Architecture.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public partial class StorageWriter<TModel> : TradableStorage<StorageWriter<TModel>>,
        IStorageWriter<StorageWriter<TModel>, TModel>
        where TModel : DataModel<TModel>
    {
        protected WriterDelegate Write { get; private set; }

        public override StorageWriter<TModel> Configure(UnderlyingSession session)
        {
            base.Configure(session);

            Write = Context.ChangeTracker.TrackGraph;

            return this;
        }

        public TModel Create(TModel data)
        {
            // TODO: Implementar validação

            Write(data, node => node.Entry.State = EntityState.Added);
            Context.SaveChanges();

            return data;
        }

        public TModel Replace(TModel data)
        {
            // TODO: Implementar validação

            Write(data, node => node.Entry.State = EntityState.Modified);
            Context.SaveChanges();

            return data;
        }

        public void Remove(TModel data)
        {
            // TODO: Implementar validação

            Write(data, node => node.Entry.State = EntityState.Deleted);
            Context.SaveChanges();
        }
    }
}
