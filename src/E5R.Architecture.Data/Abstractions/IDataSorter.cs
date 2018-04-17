using System;
using System.Linq.Expressions;

namespace E5R.Architecture.Data.Abstractions
{
    /// <summary>
    /// Data sorter (OrderBy) for data model with identifier
    /// </summary>
    /// <typeparam name="TModel">Data model type</typeparam>
    public interface IDataSorter<TModel>
        where TModel : DataModel<TModel>
    {
        /// <summary>
        /// Descending order
        /// </summary>
        bool Descending { get; }

        /// <summary>
        /// Get a sorter expression (OrderBy)
        /// </summary>
        /// <returns>List of sorter expression</returns>
        Expression<Func<TModel, object>> GetSorter();
    }
}
