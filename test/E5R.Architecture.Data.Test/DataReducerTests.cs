using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace E5R.Architecture.Data.Test
{
    using Abstractions;
    using Mocks;

    public class DataReducerTests
    {
        [Fact]
        public void Must_Instantiate_For_Model_Without_Identifier()
        {
            // Act
            var instance = new EmptyDataReducer();

            // Assert
            Assert.NotNull(instance);
        }

        #region Mocks

        class EmptyDataReducer : DataReducer<ObjectDataModelMock>
        {
            public override IEnumerable<Expression<Func<ObjectDataModelMock, bool>>> GetReducer()
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
