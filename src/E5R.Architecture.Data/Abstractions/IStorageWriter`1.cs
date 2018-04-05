namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageWriter<TModel> : IStorageWriter<TModel, VoidIdentifier>
        where TModel : DataModel<VoidIdentifier>
    {
    }
}
