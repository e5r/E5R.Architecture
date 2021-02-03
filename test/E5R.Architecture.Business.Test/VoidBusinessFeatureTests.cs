// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Threading.Tasks;
using Xunit;

namespace E5R.Architecture.Business.Test
{
    public class VoidBusinessFeatureTests
    {
        [Fact]
        async void ExecAsyncOnlyRedirects_ToExecActionAsync()
        {
            var feature = new OutputOnlyFeature();
            var exception = await Assert.ThrowsAsync<NotImplementedException>(() => feature.ExecAsync());

            Assert.Equal("Output passou por aqui!", exception.Message);
        }
    }

    #region Mocks

    public class OutputOnlyFeature : OutputOnlyBusinessFeature<int>
    {
        protected override Task<int> ExecActionAsync() =>
            throw new NotImplementedException("Output passou por aqui!");
    }

    #endregion
}