namespace E5R.Architecture.Business
{
    using Core;
    using Data.Abstractions;

    /// <summary>
    /// Business object
    /// </summary>
    /// <remarks>Base to implements business rules</remarks>
    /// <typeparam name="TObject">Type of a business object</typeparam>
    /// <typeparam name="TModule">Type of a data module</typeparam>
    public class BusinessObject<TObject, TModule>
        where TObject : class
        where TModule : IDataMouleSignature
    {
        protected TModule Module { get; private set; }

        public TObject Anchor(TModule module)
        {
            Checker.NotNullArgument(module, nameof(module));

            Module = module;

            return this as TObject;
        }
    }
}
