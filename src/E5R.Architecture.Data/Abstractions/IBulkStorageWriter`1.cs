namespace E5R.Architecture.Data.Abstractions
{
    public interface
        IBulkStorageWriter<out TImpl> : IBulkStorageWriter<TImpl, DataModel, VoidIdentifier>
        where TImpl : class
    {
    }
}
