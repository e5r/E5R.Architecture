namespace E5R.Architecture.Data.EntityFrameworkCore
{
    using Abstractions;

    public class StorageWriter<TModel> : IStorageWriter<StorageWriter<TModel>, TModel>
        where TModel : DataModel<TModel>
    {
        private readonly FullStorage<TModel> _base;

        public StorageWriter()
        {
            _base = new FullStorage<TModel>();
        }

        protected WriterDelegate Write => _base.Write;

        public StorageWriter<TModel> Configure(UnderlyingSession session)
        {
            _base.Configure(session);

            return this;
        }

        public TModel Create(TModel data) => _base.Create(data);

        public TModel Replace(TModel data) => _base.Replace(data);

        public void Remove(TModel data) => _base.Remove(data);
    }
}
