namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorage<TModel, TIdentifier>
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
    }
}