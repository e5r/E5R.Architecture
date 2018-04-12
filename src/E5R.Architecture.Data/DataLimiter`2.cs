namespace E5R.Architecture.Data
{
    /// <inheritdoc />
    public class DataLimiter<TModel, TIdentifier> : DataSorter<TModel, TIdentifier>
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
        public int OffsetBegin { get; set; }
        public int OffsetEnd { get; set; }
        public int OffsetCount { get; set; }
    }
}
