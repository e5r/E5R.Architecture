namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorage : IStorageReader, IStorageWriter, IBulkStorageWriter
    {
    }
}
