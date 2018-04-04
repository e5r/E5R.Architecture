using System;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Data model (Entity) representation with identifier
    /// </summary>
    /// <typeparam name="TIdentifier">Identifier type of model</typeparam>
    public class DataModel<TIdentifier> where TIdentifier : struct
    {
        public DataModel() : this(null)
        {
        }

        protected DataModel(TIdentifier? identifier)
        {
            Identifier = identifier;
        }

        protected TIdentifier? Identifier { get; set; }
    }
}