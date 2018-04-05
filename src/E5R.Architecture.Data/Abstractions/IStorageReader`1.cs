namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageReader<TModel> : IStorageReader<TModel, VoidIdentifier>
        where TModel : DataModel<VoidIdentifier>
    {
    }
}
