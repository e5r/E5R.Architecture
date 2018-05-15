﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

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
