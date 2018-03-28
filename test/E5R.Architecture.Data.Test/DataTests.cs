using System;
using Xunit;

namespace E5R.Architecture.Data.Test
{
    public class DataTests
    {
        [Fact]
        public void Must_Be_Instantiated()
        {
            Assert.NotNull(new Data());
        }
    }
}
