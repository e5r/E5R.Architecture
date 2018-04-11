namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageWriter<out TImpl, TModel, in TIdentifier> : ITradableObject<TImpl>
        where TImpl : class
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
        TModel Create(TModel data);
        TModel Replace(TModel data);
        void Remove(TIdentifier id);
    }
}
