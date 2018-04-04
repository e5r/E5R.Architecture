namespace E5R.Architecture.Data.Abstractions
{
    public interface IReadingWritingStorage<TModel, TIdentifier>
        : IReadingStorage<TModel, TIdentifier>, IWritingStorage<TModel, TIdentifier>
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
    }
}