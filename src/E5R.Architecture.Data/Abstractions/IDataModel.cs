namespace E5R.Architecture.Data.Abstractions
{
    public interface IDataModel
    {
        /// <summary>
        /// Get a identifiers values
        /// </summary>
        /// <returns>List of identifiers values</returns>
        object[] IdentifierValues { get; }
    }
}