namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageWriter<out TImpl, TModel> : IStorageWriter<TImpl, TModel, VoidIdentifier>
        where TImpl : class
        where TModel : DataModel<VoidIdentifier>
    {
    }
}
