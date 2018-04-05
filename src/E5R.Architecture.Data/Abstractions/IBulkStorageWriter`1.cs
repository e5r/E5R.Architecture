namespace E5R.Architecture.Data.Abstractions
{
    public interface IBulkStorageWriter<TModel> : IBulkStorageWriter<TModel, VoidIdentifier>
        where TModel : DataModel<VoidIdentifier>
    {
    }
}
