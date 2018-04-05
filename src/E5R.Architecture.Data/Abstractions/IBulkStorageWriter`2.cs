using System.Collections.Generic;

namespace E5R.Architecture.Data.Abstractions
{
    public interface IBulkStorageWriter<TModel, TIdentifier>
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
        IEnumerable<TModel> BulkCreate(IEnumerable<TModel> data);
        IEnumerable<TModel> BulkReplace(IEnumerable<TModel> models);
        void BulkRemove(IEnumerable<TIdentifier> ids);
        void BulkRemoveFromSearch(DataReducer<TModel, TIdentifier> reducer);
    }
}
