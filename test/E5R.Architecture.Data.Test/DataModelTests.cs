using E5R.Architecture.Data.Abstractions;
using Xunit;

namespace E5R.Architecture.Data.Test
{
    public class DataModelTests
    {
        [Fact]
        public void Must_Instantiate_Without_Identifier()
        {
            // Act
            var instance = new DataModelMock();

            // Assert
            Assert.NotNull(instance);
        }
        
        internal class DataModelMock : DataModel<object>{}
    }
}
