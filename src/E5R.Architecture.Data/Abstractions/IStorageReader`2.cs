using System.Collections.Generic;

namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageReader<out TImpl, TDataModel> : IStorageSignature, ITradableObject<TImpl>
        where TImpl : class
        where TDataModel : IDataModel
    {
        TDataModel Find(TDataModel data);
        DataLimiterResult<TDataModel> Get(DataLimiter<TDataModel> limiter);
        IEnumerable<TDataModel> Search(DataReducer<TDataModel> reducer);

        DataLimiterResult<TDataModel> LimitedSearch(DataReducer<TDataModel> reducer,
            DataLimiter<TDataModel> limiter);
    }
}
