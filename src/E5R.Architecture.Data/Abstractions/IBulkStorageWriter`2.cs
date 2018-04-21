using System.Collections.Generic;

namespace E5R.Architecture.Data.Abstractions
{
    public interface IBulkStorageWriter<TDataModel> : IStorageSignature, ITradableObject
        where TDataModel : IDataModel
    {
        IEnumerable<TDataModel> BulkCreate(IEnumerable<TDataModel> data);
        IEnumerable<TDataModel> BulkReplace(IEnumerable<TDataModel> data);
        void BulkRemove(IEnumerable<TDataModel> data);
        void BulkRemoveFromSearch(DataReducer<TDataModel> reducer);
    }
}
