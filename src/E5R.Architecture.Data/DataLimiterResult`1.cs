using System.Collections.Generic;

namespace E5R.Architecture.Data
{
    using Abstractions;

    /// <summary>
    /// Result of a search with <see cref="DataLimiter{TModel}"/>
    /// </summary>
    /// <typeparam name="TModel">Data model type</typeparam>
    public class DataLimiterResult<TModel>
        where TModel : DataModel<TModel>
    {
        public DataLimiterResult(IEnumerable<TModel> result, int count)
        {
            Result = result;
            Count = count;
        }

        public int Count { get; private set; }
        public IEnumerable<TModel> Result { get; private set; }
    }
}
