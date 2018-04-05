namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorage<TModel> : IStorageReader<TModel>, IStorageWriter<TModel>,
        IBulkStorageWriter<TModel>
        where TModel : DataModel<VoidIdentifier>
    {
    }
}
