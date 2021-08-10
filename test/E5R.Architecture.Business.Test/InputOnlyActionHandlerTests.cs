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
    public class InputOnlyActionHandlerTests
    {
        [Fact]
        void RequiresITransformationManager_ToBe_Instantiated()
        {
            var transformerMock = new Mock<ILazy<ITransformationManager>>();
            var handler = new IntegerInputOnlyHandler(transformerMock.Object);
            var exception =
                Assert.Throws<ArgumentNullException>(() => new IntegerInputOnlyHandler(null));

            Assert.NotNull(handler);
            Assert.Equal("transformer", exception.ParamName);
        }

        [Fact]
        async void ExecRequires_NotNullInput()
        {
            var transformerMock = new Mock<ILazy<ITransformationManager>>();
            var handler = new IntegerInputOnlyHandler(transformerMock.Object);
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    handler.ExecAsync((string) null));

            Assert.Equal("input", exception.ParamName);
        }

        [Fact]
        async void ExecFromRequires_NotNullInput()
        {
            var transformerMock = new Mock<ILazy<ITransformationManager>>();
            var handler = new IntegerInputOnlyHandler(transformerMock.Object);
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    handler.ExecFromAsync<double?>(null));

            Assert.Equal("from", exception.ParamName);
        }

        [Fact]
        async void ExecFrom_DoesNotExecute_WhenInputIsAlreadyOfThe_ExpectedType()
        {
            var transformerMock = new Mock<ILazy<ITransformationManager>>();
            var handler = new IntegerInputOnlyHandler(transformerMock.Object);
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

            var handler = new IntegerInputOnlyHandler(transformerMock.Object);
            var exception =
                await Assert.ThrowsAsync<BusinessLayerException>(() => handler.ExecFromAsync(0));

            Assert.Equal("The input type cannot be transformed to the expected type properly",
                exception.Message);
        }

        [Fact]
        async void ExecFrom_WhenInputIsCorrectlyTransformed_ExecActionIsTriggered()
        {
            var transformerMock = new Mock<ILazy<ITransformationManager>>();

            transformerMock.Setup(s => s.Value.Transform<string, Int64?>(It.IsAny<string>()))
                .Returns(0);

            var handler = new IntegerInputOnlyHandler(transformerMock.Object);
            var exception =
                await Assert.ThrowsAsync<NotImplementedException>(() => handler.ExecAsync(""));

            Assert.Equal("Integer passou por aqui!", exception.Message);
        }
    }

    #region Mocks

    public class IntegerInputOnlyHandler : InputOnlyActionHandlerWithTransformer<string>
    {
        public IntegerInputOnlyHandler(ILazy<ITransformationManager> transformer) : base(
            transformer)
        {
        }

        protected override Task ExecActionAsync(string input) =>
            throw new NotImplementedException("Integer passou por aqui!");
    }

    #endregion
}
