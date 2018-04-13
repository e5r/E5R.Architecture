using Xunit;

namespace E5R.Architecture.Data.Test
{
    using Mocks;

    public class DataReducerTests
    {
        [Fact]
        public void Must_Instantiate_For_Model_Without_Identifier()
        {
            // Act
            var instance = new DataReducer<ObjectDataModelMock>();

            // Assert
            Assert.NotNull(instance);
        }
    }
}
