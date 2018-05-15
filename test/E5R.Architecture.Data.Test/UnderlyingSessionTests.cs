// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using Xunit;

namespace E5R.Architecture.Data.Test
{
    public class UnderlyingSessionTests
    {
        [Fact]
        public void Does_Not_Accept_Null_Session_Object()
        {
            // Act
            var error = Assert.Throws<ArgumentNullException>(() => new UnderlyingSession(null));

            // Assert
            Assert.Equal("session", error.ParamName);
        }

        [Fact]
        public void Expected_Type_Is_Accepted()
        {
            // Arrange
            var session = new MySession1();

            // Act
            var underlyingSession = new UnderlyingSession(session);

            // Assert
            Assert.NotNull(underlyingSession.Get<MySession1>());
            Assert.Equal(session, underlyingSession.Get<MySession1>());
        }

        [Fact]
        public void Inherited_Types_Are_Accepted()
        {
            // Arrange
            var session1 = new MySession1();
            var session2 = new MySession2();

            // Act
            var underlyingSession1 = new UnderlyingSession(session1);
            var underlyingSession2 = new UnderlyingSession(session2);

            // Assert
            Assert.NotNull(underlyingSession1.Get<MySession1>());
            Assert.NotNull(underlyingSession2.Get<MySession2>());
            Assert.NotNull(underlyingSession2.Get<MySession1>());

            Assert.Equal(session1, underlyingSession1.Get<MySession1>());
            Assert.Equal(session2, underlyingSession2.Get<MySession2>());

            Assert.Equal(session2, underlyingSession2.Get<MySession1>());
            Assert.NotEqual(session1, underlyingSession2.Get<MySession1>());
        }

        [Fact]
        public void Invalid_Type_Throws_CastException()
        {
            // Arrange
            var session = new MySession1();

            // Act
            var underlyingSession = new UnderlyingSession(session);

            // Assert
            Assert.Throws<InvalidCastException>(() => underlyingSession.Get<MySession2>());
        }

        #region Mocks

        private class MySession1
        {
        }

        private class MySession2 : MySession1
        {
        }

        #endregion
    }
}
