using System;
using System.Linq.Expressions;
using Xunit;

namespace E5R.Architecture.Data.Test
{
    using Abstractions;

    public class DataLimiterTests
    {
        [Fact]
        public void Must_Instantiate_For_Model_Without_Identifier()
        {
            // Act
            var instance = new EmptyDataLimiter();

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void Must_Instantiate_For_Model_Without_Identifier_With_Properties()
        {
            // Act
            var instance = new EmptyDataLimiter
            {
                OffsetBegin = 1,
                OffsetEnd = 2
            };

            // Assert
            Assert.NotNull(instance);
            Assert.Equal(1, instance.OffsetBegin);
            Assert.Equal(2, instance.OffsetEnd);
        }

        #region Mocks

        class EmptyDataLimiter : DataLimiter<DataModel<object>>
        {
            public override Expression<Func<DataModel<object>, object>> GetSorter()
                => null;
        }

        #endregion
    }
}
