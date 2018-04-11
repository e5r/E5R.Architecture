namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorage<out TImpl, TModel, TIdentifier> :
        IStorageReader<TImpl, TModel, TIdentifier>,
        IStorageWriter<TImpl, TModel, TIdentifier>,
        IBulkStorageWriter<TImpl, TModel, TIdentifier>
        where TImpl : class
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
    }
}
