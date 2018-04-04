namespace E5R.Architecture.Data
{
    /// <summary>
    /// Data sorter for data model with identifier
    /// </summary>
    /// <typeparam name="TModel">Data model type</typeparam>
    /// <typeparam name="TIdentifier">Data model identifier</typeparam>
    public class DataSorter<TModel, TIdentifier>
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
    }
}