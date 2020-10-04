// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace E5R.Architecture.Data.Test
{
    using Abstractions;

    public class DataReducerTests
    {
        [Fact]
        public void Must_Instantiate_For_Model_Without_Identifier()
        {
            // Act
            var instance = new EmptyDataFilter();

            // Assert
            Assert.NotNull(instance);
        }

        #region Mocks

        class EmptyDataFilter : DataFilter<DataModel<object>>
        {
            public override IEnumerable<Expression<Func<DataModel<object>, bool>>> GetFilter()
                => null;
        }

        #endregion
    }
}
