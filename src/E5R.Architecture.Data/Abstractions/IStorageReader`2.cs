using System.Collections.Generic;

namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageReader<out TImpl, TModel> : IStorageSignature, ITradableObject<TImpl>
        where TImpl : class
        where TModel : DataModel<TModel>
    {
        TModel Find(TModel data);
        IEnumerable<TModel> Get(DataLimiter<TModel> limiter);
        IEnumerable<TModel> Search(DataReducer<TModel> reducer);
        IEnumerable<TModel> LimitedSearch(DataReducer<TModel> reducer, DataLimiter<TModel> limiter);
    }
}
