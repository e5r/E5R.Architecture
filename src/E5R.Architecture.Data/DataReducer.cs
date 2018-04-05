using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Data reducer (Where) for data model without identifier
    /// </summary>
    public class DataReducer : DataReducer<DataModel, VoidIdentifier>
    {
        /// <summary>
        /// Get a reducer expression (Where)
        /// </summary>
        /// <returns>List of reducer expression</returns>
        /// <exception cref="NotImplementedException">If not implemented</exception>
        protected virtual IEnumerable<Expression<Func<DataModel, object>>> GetReducer()
        {
            throw new NotImplementedException();
        }
    }
}
