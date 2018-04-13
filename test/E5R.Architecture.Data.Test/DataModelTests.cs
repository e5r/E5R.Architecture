using Xunit;

namespace E5R.Architecture.Data.Test
{
    using Mocks;

    public class DataModelTests
    {
        [Fact]
        public void Must_Instantiate_Without_Identifier()
        {
            // Act
            var instance = new ObjectDataModelMock();

            // Assert
            Assert.NotNull(instance);
        }
    }
}
