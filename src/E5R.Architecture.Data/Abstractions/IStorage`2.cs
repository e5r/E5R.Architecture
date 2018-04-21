namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorage<TDataModel> :
        IStorageReader<TDataModel>,
        IStorageWriter<TDataModel>,
        IBulkStorageWriter<TDataModel>
        where TDataModel : IDataModel
    {
    }
}
