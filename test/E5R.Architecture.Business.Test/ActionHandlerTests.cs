// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Threading.Tasks;
using E5R.Architecture.Core;
using E5R.Architecture.Core.Exceptions;
using Moq;
using Xunit;

namespace E5R.Architecture.Business.Test
{
    public class ActionHandlerTests
    {
        [Fact]
        void RequiresITransformationManager_ToBe_Instantiated()
        {
            var transformerMock = new Mock<ILazy<ITransformationManager>>();
            var handler = new TenInputOutputHandler(transformerMock.Object);
            var exception =
                Assert.Throws<ArgumentNullException>(() => new TenInputOutputHandler(null));

            Assert.NotNull(handler);
            Assert.Equal("transformer", exception.ParamName);
        }

        [Fact]
        async void ExecRequires_NotNullInput()
        {
            var transformerMock = new Mock<ILazy<ITransformationManager>>();
            var handler = new TenInputOutputHandler(transformerMock.Object);
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    handler.ExecAsync((string) null));

            Assert.Equal("input", exception.ParamName);
        }

        [Fact]
        async void ExecFromRequires_NotNullInput()
        {
            var transformerMock = new Mock<ILazy<ITransformationManager>>();
            var handler = new TenInputOutputHandler(transformerMock.Object);
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    handler.ExecFromAsync<double?>(null));

            Assert.Equal("from", exception.ParamName);
        }

        [Fact]
        async void ExecFrom_DoesNotExecute_WhenInputIsAlreadyOfThe_ExpectedType()
        {
            var transformerMock = new Mock<ILazy<ITransformationManager>>();
            var handler = new TenInputOutputHandler(transformerMock.Object);
            var exception =
                await Assert.ThrowsAsync<BusinessLayerException>(() =>
                    handler.ExecFromAsync<string>("notnull"));

            Assert.Equal(
                "The input object is already of the expected type. You should use Exec() instead of Exec<>()",
                exception.Message);
        }

        [Fact]
        async void ExecFrom_RaisesException_WhenThenTransformer_ReturnsNull()
        {
            var transformerMock = new Mock<ILazy<ITransformationManager>>();

            transformerMock.Setup(s => s.Value.Transform<int, string>(It.IsAny<int>()))
                .Returns((string) null);

            var handler = new TenInputOutputHandler(transformerMock.Object);
            var exception =
                await Assert.ThrowsAsync<BusinessLayerException>(() =>
                    handler.ExecFromAsync(1));

            Assert.Equal("The input type cannot be transformed to the expected type properly",
                exception.Message);
        }

        [Fact]
        async void ExecFrom_WhenInputIsCorrectlyTransformed_ExecActionIsTriggered()
        {
            var transformerMock = new Mock<ILazy<ITransformationManager>>();

            transformerMock.Setup(s => s.Value.Transform<int, string>(It.IsAny<int>()))
                .Returns("TenLetters");

            var handler = new TenInputOutputHandler(transformerMock.Object);
            int result = await handler.ExecFromAsync(0);

            Assert.Equal(10, result);
        }
    }

    #region Mocks

    public class TenInputOutputHandler : ActionHandlerWithTransformer<string, int>
    {
        public TenInputOutputHandler(ILazy<ITransformationManager> transformer) : base(transformer)
        {
        }

        protected override Task<int> ExecActionAsync(string input) => Task.Run(() => 10);
    }

    #endregion
}
