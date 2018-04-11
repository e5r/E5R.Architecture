namespace E5R.Architecture.Data.Abstractions
{
    public interface
        IStorageReader<out TImpl, TModel> : IStorageReader<TImpl, TModel, VoidIdentifier>
        where TImpl : class
        where TModel : DataModel<VoidIdentifier>
    {
    }
}
