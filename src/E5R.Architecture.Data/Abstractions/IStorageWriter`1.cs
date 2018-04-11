namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageWriter<out TImpl> : IStorageWriter<TImpl, DataModel, VoidIdentifier>
        where TImpl : class
    {
    }
}
