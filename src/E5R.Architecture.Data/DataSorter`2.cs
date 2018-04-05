using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Data sorter (OrderBy) for data model with identifier
    /// </summary>
    /// <typeparam name="TModel">Data model type</typeparam>
    /// <typeparam name="TIdentifier">Data model identifier</typeparam>
    public class DataSorter<TModel, TIdentifier>
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
        /// <summary>
        /// Get a sorter expression (OrderBy)
        /// </summary>
        /// <returns>List of sorter expression</returns>
        /// <exception cref="NotImplementedException">If not implemented</exception>
        protected virtual IEnumerable<Expression<Func<TModel, object>>> GetSorter()
        {
            throw new NotImplementedException();
        }
    }
}
