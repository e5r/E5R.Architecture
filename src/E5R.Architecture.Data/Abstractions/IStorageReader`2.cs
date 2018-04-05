using System.Collections.Generic;

namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageReader<TModel, TIdentifier>
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
        TModel Find(TIdentifier id);
        IEnumerable<TModel> Get(DataLimiter<TModel, TIdentifier> limiter);
        IEnumerable<TModel> Search(DataReducer<TModel, TIdentifier> reducer);

        IEnumerable<TModel> LimitedSearch(DataReducer<TModel, TIdentifier> reducer,
            DataLimiter<TModel, TIdentifier> limiter);
    }
}
