using System;
using System.Linq.Expressions;

namespace E5R.Architecture.Data.Abstractions
{
    /// <inheritdoc />
    public abstract class DataLimiter<TDataModel> : IDataSorter<TDataModel>
        where TDataModel : IDataModel
    {
        public int OffsetBegin { get; set; }
        public int OffsetEnd { get; set; }
        public bool Descending { get; set; }
        public abstract Expression<Func<TDataModel, object>> GetSorter();
    }
}
