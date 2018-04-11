namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorage<out TImpl> : IStorageReader<TImpl>, IStorageWriter<TImpl>,
        IBulkStorageWriter<TImpl>
        where TImpl : class
    {
    }
}
