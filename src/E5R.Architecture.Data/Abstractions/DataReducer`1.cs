using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace E5R.Architecture.Data.Abstractions
{
    /// <summary>
    /// Data reducer (Where) for data model with identifier
    /// </summary>
    /// <typeparam name="TDataModel">Model type</typeparam>
    public abstract class DataReducer<TDataModel>
        where TDataModel : IDataModel
    {
        /// <summary>
        /// Get a reducer expression (Where)
        /// </summary>
        /// <returns>List of reducer expression</returns>
        public abstract IEnumerable<Expression<Func<TDataModel, bool>>> GetReducer();
    }
}
