using System.Collections.Generic;

namespace E5R.Architecture.Data
{
    using Abstractions;

    /// <summary>
    /// Result of a search with <see cref="DataLimiter{TDataModel}"/>
    /// </summary>
    /// <typeparam name="TDataModel">Data model type</typeparam>
    public class DataLimiterResult<TDataModel>
        where TDataModel : IDataModel
    {
        public DataLimiterResult(IEnumerable<TDataModel> result, int count)
        {
            Result = result;
            Count = count;
        }

        public int Count { get; private set; }
        public IEnumerable<TDataModel> Result { get; private set; }
    }
}
