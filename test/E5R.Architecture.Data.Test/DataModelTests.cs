// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

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
