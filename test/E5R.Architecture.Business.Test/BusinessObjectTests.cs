// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using Xunit;

namespace E5R.Architecture.Business.Test
{
    using Mocks;

    public class BusinessObjectTests
    {
        [Fact]
        public void Must_Instantiate()
        {
            // Act
            var instance = new EmptyBusinessObject();

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void Module_IsNull_When_NotAnchored()
        {
            // Act
            var instance = new EmptyBusinessObject();

            // Assert
            Assert.Null(instance.ExposeModule);
        }

        [Fact]
        public void Module_IsNotNull_When_Anchored()
        {
            // Arrange
            var module = new EmptyDataModule();

            // Act
            var instance = new EmptyBusinessObject()
                .Anchor(module);

            // Assert
            Assert.NotNull(instance.ExposeModule);
        }

        [Fact]
        public void The_Module_Itself_IsReturned_AtTheAnchor()
        {
            // Arrange
            var module = new EmptyDataModule();

            // Act
            var instance1 = new EmptyBusinessObject();
            var instance2 = instance1.Anchor(module);

            // Assert
            Assert.Equal(instance1, instance2);
        }

        [Fact]
        public void Can_Not_Anchor_NullModule()
        {
            // Act/Assert
            var error =
                Assert.Throws<ArgumentNullException>(
                    () => new EmptyBusinessObject().Anchor(null));

            Assert.Equal("module", error.ParamName);
        }
    }
}
