using System;

namespace E5R.Architecture.Data
{
    public class DataModel<TIdentifier> where TIdentifier : struct
    {
        private readonly TIdentifier? _identifier;

        public DataModel(TIdentifier? identifier)
        {
            _identifier = identifier;
        }
    }
}