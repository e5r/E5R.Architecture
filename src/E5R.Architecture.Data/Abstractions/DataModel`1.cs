namespace E5R.Architecture.Data.Abstractions
{
    /// <summary>
    /// Data model (Entity) representation with identifiers
    /// </summary>
    /// <typeparam name="TBusinessModel">Type of business model</typeparam>
    public abstract class DataModel<TBusinessModel> : IDataModel
        where TBusinessModel : new()
    {
        protected DataModel() => Business = new TBusinessModel();

        protected DataModel(TBusinessModel businessModel) => Business = businessModel;

        protected TBusinessModel Business { get; private set; }

        public virtual object[] IdentifierValues { get; }
    }
}
