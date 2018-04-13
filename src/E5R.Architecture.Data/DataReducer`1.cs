using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Data reducer (Where) for data model with identifier
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    public class DataReducer<TModel>
        where TModel : DataModel<TModel>
    {
        /// <summary>
        /// Get a reducer expression (Where)
        /// </summary>
        /// <returns>List of reducer expression</returns>
        public virtual IEnumerable<Expression<Func<TModel, bool>>> GetReducer()
        {
            throw new MissingMethodException();
        }
    }
}
