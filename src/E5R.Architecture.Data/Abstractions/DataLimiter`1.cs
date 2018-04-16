using System;
using System.Linq.Expressions;

namespace E5R.Architecture.Data.Abstractions
{
    /// <inheritdoc />
    public abstract class DataLimiter<TModel> : IDataSorter<TModel>
        where TModel : DataModel<TModel>
    {
        public int OffsetBegin { get; set; }
        public int OffsetEnd { get; set; }
        public bool Descending { get; set; }
        public abstract Expression<Func<TModel, object>> GetSorter();
    }
}
