namespace E5R.Architecture.Data
{
    /// <inheritdoc />
    public class DataLimiter<TModel> : DataSorter<TModel>
        where TModel : DataModel<TModel>
    {
        public int OffsetBegin { get; set; }
        public int OffsetEnd { get; set; }
        public int OffsetCount { get; set; }
    }
}
