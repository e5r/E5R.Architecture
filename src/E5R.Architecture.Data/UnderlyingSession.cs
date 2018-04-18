using System;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// A underlying session representation of Unit of Work 
    /// </summary>
    public class UnderlyingSession
    {
        private readonly object _session;

        public UnderlyingSession(object session)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        /// <summary>
        /// Converts internal object to expected type
        /// </summary>
        /// <returns>Reference to internal object converted to expected type</returns>
        /// <exception cref="InvalidCastException">When internal type does not match</exception>
        public TExpected Get<TExpected>() where TExpected : class
        {
            return (TExpected) _session;
        }
    }
}
