namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageWriter<TDataModel> : IStorageSignature, ITradableObject
        where TDataModel : IDataModel
    {
        TDataModel Create(TDataModel data);
        TDataModel Replace(TDataModel data);
        void Remove(TDataModel data);
    }
}
