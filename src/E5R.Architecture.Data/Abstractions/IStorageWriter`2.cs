namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageWriter<out TImpl, TModel> : ITradableObject<TImpl>
        where TImpl : class
        where TModel : DataModel<TModel>
    {
        TModel Create(TModel data);
        TModel Replace(TModel data);
        void Remove(TModel data);
    }
}
