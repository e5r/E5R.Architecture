namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageReader<out TImpl> : IStorageReader<TImpl, DataModel, VoidIdentifier>
        where TImpl : class
    {
    }
}
