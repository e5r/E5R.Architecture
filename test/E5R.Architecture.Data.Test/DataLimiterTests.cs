using Xunit;

namespace E5R.Architecture.Data.Test
{
    using Mocks;

    public class DataLimiterTests
    {
        [Fact]
        public void Must_Instantiate_For_Model_Without_Identifier()
        {
            // Act
            var instance = new DataLimiter<ObjectDataModelMock>();

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void Must_Instantiate_For_Model_Without_Identifier_With_Properties()
        {
            // Act
            var instance = new DataLimiter<ObjectDataModelMock>
            {
                OffsetBegin = 1,
                OffsetEnd = 2,
                OffsetCount = 3
            };

            // Assert
            Assert.NotNull(instance);
            Assert.Equal(1, instance.OffsetBegin);
            Assert.Equal(2, instance.OffsetEnd);
            Assert.Equal(3, instance.OffsetCount);
        }
    }
}
