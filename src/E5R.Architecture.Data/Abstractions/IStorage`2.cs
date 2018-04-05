namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorage<TModel, TIdentifier> :
        IStorageReader<TModel, TIdentifier>,
        IStorageWriter<TModel, TIdentifier>,
        IBulkStorageWriter<TModel, TIdentifier>
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
    }
}