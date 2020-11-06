// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using Xunit;

namespace E5R.Architecture.Core.Test
{
    public class CheckerTests
    {
        [Fact]
        public void Raises_Exception_When_Parameter_IsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => Checker.NotNullArgument(null, "name"));

            Assert.Equal("name", exception.ParamName);
        }

        [Fact]
        public void Nothing_Occurs_When_Parameter_IsNotNull()
        {
            Checker.NotNullArgument(new object(), "nothing");
        }

        [Fact]
        public void Raises_Exception_When_Object_IsNull()
        {
            var exception = Assert.Throws<NullReferenceException>(() => Checker.NotNullObject(null, "objectName"));

            Assert.Equal("Object objectName can not be null.", exception.Message);
        }

        [Fact]
        public void Nothing_Occurs_When_Object_IsNotNull()
        {
            Checker.NotNullObject(new object(), "nothing");
        }
    }
}
