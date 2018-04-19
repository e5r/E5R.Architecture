namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorage<out TImpl, TDataModel> :
        IStorageReader<TImpl, TDataModel>,
        IStorageWriter<TImpl, TDataModel>,
        IBulkStorageWriter<TImpl, TDataModel>
        where TImpl : class
        where TDataModel : IDataModel
    {
    }
}
