using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace E5R.Architecture.Data
{
    /// <inheritdoc />
    public class DataSorter : DataSorter<DataModel, VoidIdentifier>
    {
        /// <inheritdoc />
        protected override IEnumerable<Expression<Func<DataModel, object>>> GetSorter()
        {
            throw new NotImplementedException();
        }
    }
}
