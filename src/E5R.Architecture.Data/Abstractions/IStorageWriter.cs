namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageWriter<TModel, in TIdentifier>
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
        TModel Create(TModel data);
        TModel Replace(TModel data);
        void Remove(TIdentifier id);
    }
}