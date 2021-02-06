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
    public class InputOnlyBusinessFeatureTests
    {
        [Fact]
        void RequiresITransformationManager_ToBe_Instantiated()
        {
            var transformerMock = new Mock<ILazy<ITransformationManager>>();
            var feature = new IntegerInputonlyFeature(transformerMock.Object);
            var exception =
                Assert.Throws<ArgumentNullException>(() => new IntegerInputonlyFeature(null));

            Assert.NotNull(feature);
            Assert.Equal("transformer", exception.ParamName);
        }

        [Fact]
        async void ExecRequires_NotNullInput()
        {
            var transformerMock = new Mock<ILazy<ITransformationManager>>();
            var feature = new IntegerInputonlyFeature(transformerMock.Object);
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    feature.ExecAsync((string) null));

            Assert.Equal("input", exception.ParamName);
        }

        [Fact]
        async void ExecFromRequires_NotNullInput()
        {
            var transformerMock = new Mock<ILazy<ITransformationManager>>();
            var feature = new IntegerInputonlyFeature(transformerMock.Object);
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    feature.ExecAsync<double?>(null));

            Assert.Equal("from", exception.ParamName);
        }

        [Fact]
        async void ExecFrom_DoesNotExecute_WhenInputIsAlreadyOfThe_ExpectedType()
        {
            var transformerMock = new Mock<ILazy<ITransformationManager>>();
            var feature = new IntegerInputonlyFeature(transformerMock.Object);
            var exception =
                await Assert.ThrowsAsync<BusinessLayerException>(() =>
                    feature.ExecAsync<string>("notnull"));

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

            var feature = new IntegerInputonlyFeature(transformerMock.Object);
            var exception =
                await Assert.ThrowsAsync<BusinessLayerException>(() => feature.ExecAsync(0));

            Assert.Equal("The input type cannot be transformed to the expected type properly",
                exception.Message);
        }

        [Fact]
        async void ExecFrom_WhenInputIsCorrectlyTransformed_ExecActionIsTriggered()
        {
            var transformerMock = new Mock<ILazy<ITransformationManager>>();

            transformerMock.Setup(s => s.Value.Transform<string, Int64?>(It.IsAny<string>()))
                .Returns(0);

            var feature = new IntegerInputonlyFeature(transformerMock.Object);
            var exception =
                await Assert.ThrowsAsync<NotImplementedException>(() => feature.ExecAsync(""));

            Assert.Equal("Integer passou por aqui!", exception.Message);
        }
    }

    #region Mocks

    public class IntegerInputonlyFeature : InputOnlyBusinessFeature<string>
    {
        public IntegerInputonlyFeature(ILazy<ITransformationManager> transformer) : base(
            transformer)
        {
        }

        protected override Task ExecActionAsync(string input) =>
            throw new NotImplementedException("Integer passou por aqui!");
    }

    #endregion
}
