namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorage<out TImpl, TModel> :
        IStorageReader<TImpl, TModel>,
        IStorageWriter<TImpl, TModel>,
        IBulkStorageWriter<TImpl, TModel>
        where TImpl : class
        where TModel : DataModel<TModel>
    {
    }
}
