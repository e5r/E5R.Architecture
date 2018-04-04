namespace E5R.Architecture.Data
{
    /// <summary>
    /// Is a #order-by
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    /// <typeparam name="TIdentifier">Identifier type of model</typeparam>
    public class DataSorter<TModel, TIdentifier>
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
    }
}