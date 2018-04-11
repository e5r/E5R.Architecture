namespace E5R.Architecture.Data.Abstractions
{
    public interface
        IBulkStorageWriter<out TImpl, TModel> : IBulkStorageWriter<TImpl, TModel, VoidIdentifier>
        where TImpl : class
        where TModel : DataModel<VoidIdentifier>
    {
    }
}
