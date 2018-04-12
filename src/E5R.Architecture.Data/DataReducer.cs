using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace E5R.Architecture.Data
{
    /// <inheritdoc />
    public class DataReducer : DataReducer<DataModel, VoidIdentifier>
    {
        /// <inheritdoc />
        protected override IEnumerable<Expression<Func<DataModel, object>>> GetReducer()
        {
            throw new NotImplementedException();
        }
    }
}
