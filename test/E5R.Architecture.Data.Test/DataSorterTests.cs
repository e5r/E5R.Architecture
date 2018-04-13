using Xunit;

namespace E5R.Architecture.Data.Test
{
    using Mocks;

    public class DataSorterTests
    {
        [Fact]
        public void Must_Instantiate_For_Model_Without_Identifier()
        {
            // Act
            var instance = new DataSorter<ObjectDataModelMock>();

            // Assert
            Assert.NotNull(instance);
        }
    }
}
