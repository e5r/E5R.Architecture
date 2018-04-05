using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Data sorter (OrderBy) for data model without identifier
    /// </summary>
    public class DataSorter : DataSorter<DataModel, VoidIdentifier>
    {
        /// <summary>
        /// Get a sorter expression (OrderBy)
        /// </summary>
        /// <returns>List of sorter expression</returns>
        /// <exception cref="NotImplementedException">If not implemented</exception>
        protected virtual IEnumerable<Expression<Func<DataModel, object>>> GetSorter()
        {
            throw new NotImplementedException();
        }
    }
}
