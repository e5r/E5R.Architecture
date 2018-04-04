namespace E5R.Architecture.Data.Abstractions
{
    public interface IWritingStorage<TModel, TIdentifier> : IStorage<TModel, TIdentifier>
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
        TModel Create(TModel model);
        TModel Replace(TModel model);
        void Remove(TIdentifier id);
    }
}