using System;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Data model (Entity) representation with identifier
    /// </summary>
    /// <typeparam name="TIdentifier">Identifier type of model</typeparam>
    public class DataModel<TIdentifier> where TIdentifier : struct
    {
        public DataModel()
        {
        }

        public DataModel(TIdentifier identifier)
        {
            Identifier = identifier;
        }

        public TIdentifier Identifier { get; private set; }
    }
}
