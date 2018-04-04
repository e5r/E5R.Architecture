using System.Collections.Generic;

namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageReader<TModel, TIdentifier>
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
        TModel Find(TIdentifier id);
        IEnumerable<TModel> Get(DataLimiter limiter);
        IEnumerable<TModel> Search(DataReducer<TModel, TIdentifier> reducer);
    }
}