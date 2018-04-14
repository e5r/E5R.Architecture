using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Data sorter (OrderBy) for data model with identifier
    /// </summary>
    /// <typeparam name="TModel">Data model type</typeparam>
    public class DataSorter<TModel>
        where TModel : DataModel<TModel>
    {
        /// <summary>
        /// Get a sorter expression (OrderBy)
        /// </summary>
        /// <returns>List of sorter expression</returns>
        public virtual Expression<Func<TModel, object>> GetSorter()
        {
            throw new MissingMethodException();
        }
    }
}
