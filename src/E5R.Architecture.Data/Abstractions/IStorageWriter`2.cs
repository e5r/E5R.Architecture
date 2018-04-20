namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageWriter<out TImpl, TDataModel> : IStorageSignature, ITradableObject<TImpl>
        where TImpl : class
        where TDataModel : IDataModel
    {
        TDataModel Create(TDataModel data);
        TDataModel Replace(TDataModel data);
        void Remove(TDataModel data);
    }
}
