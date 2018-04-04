using System;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Is a #entity
    /// </summary>
    /// <typeparam name="TIdentifier">Identifier type of model</typeparam>
    public class DataModel<TIdentifier> where TIdentifier : struct
    {
        private readonly TIdentifier? _identifier;

        public DataModel(TIdentifier? identifier)
        {
            _identifier = identifier;
        }
    }
}