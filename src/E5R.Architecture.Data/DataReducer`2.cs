using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Data reducer (Where) for data model with identifier
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    /// <typeparam name="TIdentifier">Identifier type of model</typeparam>
    public class DataReducer<TModel, TIdentifier>
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
        /// <summary>
        /// Get a reducer expression (Where)
        /// </summary>
        /// <returns>List of reducer expression</returns>
        /// <exception cref="NotImplementedException">If not implemented</exception>
        protected virtual IEnumerable<Expression<Func<TModel, object>>> GetReducer()
        {
            throw new NotImplementedException();
        }
    }
}
