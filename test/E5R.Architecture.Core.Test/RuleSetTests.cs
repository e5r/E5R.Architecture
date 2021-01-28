// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using Xunit;

namespace E5R.Architecture.Core.Test
{
    public class RuleSetTests
    {
        [Fact]
        public void Requires_ServiceProvider_Or_RuleList()
        {
            var ex1 =
                Assert.Throws<ArgumentNullException>(() =>
                    new RuleSet<FakeModel>(serviceProvider: null));

            Assert.NotNull(ex1);
            Assert.Equal("serviceProvider", ex1.ParamName);
        }

        #region Fakes

        class FakeModel
        {
        }

        #endregion
    }
}
