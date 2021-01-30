// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
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

        [Fact]
        public void ByDefaultCategory_FilterOnly_UncategorizedItems()
        {
            var modelMock = new Mock<FakeModel>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var ruleForValidMock = new Mock<IRuleFor<FakeModel>>();
            var ruleForInvalidMock = new Mock<IRuleFor<FakeModel>>();

            ruleForValidMock.Setup(s => s.Category).Returns((string) null);
            ruleForValidMock.Setup(s => s.CheckAsync(It.IsAny<FakeModel>()))
                .ReturnsAsync(RuleCheckResult.Success);
            
            ruleForInvalidMock.Setup(s => s.Category).Returns("Not null");
            ruleForInvalidMock.Setup(s => s.CheckAsync(It.IsAny<FakeModel>()))
                .ReturnsAsync(RuleCheckResult.Fail);

            serviceProviderMock
                .Setup(p => p.GetService(typeof(IEnumerable<IRuleFor<FakeModel>>)))
                .Returns(new List<IRuleFor<FakeModel>>(new[]
                {
                    ruleForValidMock.Object,
                    ruleForInvalidMock.Object
                }));

            var ruleset = new RuleSet<FakeModel>(serviceProviderMock.Object);
            var result = ruleset.ByDefaultCategory().Check(modelMock.Object);
            
            serviceProviderMock.Verify(
                v => v.GetService(typeof(IEnumerable<IRuleFor<FakeModel>>)),
                Times.Exactly(1));
            
            ruleForValidMock.Verify(v => v.CheckAsync(It.IsAny<FakeModel>()), Times.Exactly(1));
            ruleForInvalidMock.Verify(v => v.CheckAsync(It.IsAny<FakeModel>()), Times.Never());
            
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Theory]
        [InlineData("ValidCategory1", "Invalid1")]
        [InlineData("Categoria Ok", "Invalid2")]
        [InlineData("---", "Invalid3")]
        public void ByCategory_FilterOnly_ItemsFrom_TheGivenCategory(string validCategory, string invalidCategory)
        {
            var modelMock = new Mock<FakeModel>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var ruleForValid1Mock = new Mock<IRuleFor<FakeModel>>();
            var ruleForValid2Mock = new Mock<IRuleFor<FakeModel>>();
            var ruleForInvalidMock = new Mock<IRuleFor<FakeModel>>();

            ruleForValid1Mock.Setup(s => s.Category).Returns(validCategory);
            ruleForValid1Mock.Setup(s => s.CheckAsync(It.IsAny<FakeModel>()))
                .ReturnsAsync(RuleCheckResult.Success);
            
            ruleForValid2Mock.Setup(s => s.Category).Returns(validCategory);
            ruleForValid2Mock.Setup(s => s.CheckAsync(It.IsAny<FakeModel>()))
                .ReturnsAsync(RuleCheckResult.Success);
            
            ruleForInvalidMock.Setup(s => s.Category).Returns(invalidCategory);
            ruleForInvalidMock.Setup(s => s.CheckAsync(It.IsAny<FakeModel>()))
                .ReturnsAsync(RuleCheckResult.Fail);

            serviceProviderMock
                .Setup(p => p.GetService(typeof(IEnumerable<IRuleFor<FakeModel>>)))
                .Returns(new List<IRuleFor<FakeModel>>(new[]
                {
                    ruleForValid1Mock.Object,
                    ruleForValid2Mock.Object,
                    ruleForInvalidMock.Object
                }));

            var ruleset = new RuleSet<FakeModel>(serviceProviderMock.Object);
            var result = ruleset.ByCategory(validCategory).Check(modelMock.Object);
            
            serviceProviderMock.Verify(
                v => v.GetService(typeof(IEnumerable<IRuleFor<FakeModel>>)),
                Times.Exactly(1));
            
            ruleForValid1Mock.Verify(v => v.CheckAsync(It.IsAny<FakeModel>()), Times.Exactly(1));
            ruleForValid2Mock.Verify(v => v.CheckAsync(It.IsAny<FakeModel>()), Times.Exactly(1));
            ruleForInvalidMock.Verify(v => v.CheckAsync(It.IsAny<FakeModel>()), Times.Never());
            
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Theory]
        [InlineData("CODE1-OK", "CODE2-OK", "CODE1-NO")]
        [InlineData("000", "111", "222")]
        [InlineData("-1-", "*1*", "_1_")]
        public void ByCode_FilterOnly_ItemsWith_EnteredCodes(string validCode1, string validCode2, string invalidCode)
        {
            var modelMock = new Mock<FakeModel>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var ruleForValid1Mock = new Mock<IRuleFor<FakeModel>>();
            var ruleForValid2Mock = new Mock<IRuleFor<FakeModel>>();
            var ruleForInvalidMock = new Mock<IRuleFor<FakeModel>>();

            ruleForValid1Mock.Setup(s => s.Code).Returns(validCode1);
            ruleForValid1Mock.Setup(s => s.CheckAsync(It.IsAny<FakeModel>()))
                .ReturnsAsync(RuleCheckResult.Success);
            
            ruleForValid2Mock.Setup(s => s.Code).Returns(validCode2);
            ruleForValid2Mock.Setup(s => s.CheckAsync(It.IsAny<FakeModel>()))
                .ReturnsAsync(RuleCheckResult.Success);
            
            ruleForInvalidMock.Setup(s => s.Code).Returns(invalidCode);
            ruleForInvalidMock.Setup(s => s.CheckAsync(It.IsAny<FakeModel>()))
                .ReturnsAsync(RuleCheckResult.Fail);

            serviceProviderMock
                .Setup(p => p.GetService(typeof(IEnumerable<IRuleFor<FakeModel>>)))
                .Returns(new List<IRuleFor<FakeModel>>(new[]
                {
                    ruleForValid1Mock.Object,
                    ruleForValid2Mock.Object,
                    ruleForInvalidMock.Object
                }));

            var ruleset = new RuleSet<FakeModel>(serviceProviderMock.Object);
            var result1 = ruleset.ByCode(validCode1).Check(modelMock.Object);
            var result2 = ruleset.ByCode(validCode2).Check(modelMock.Object);
            var result3 = ruleset.ByCode(new[] {validCode1, validCode2}).Check(modelMock.Object);
            
            serviceProviderMock.Verify(
                v => v.GetService(typeof(IEnumerable<IRuleFor<FakeModel>>)),
                Times.Exactly(1));
            
            ruleForValid1Mock.Verify(v => v.CheckAsync(It.IsAny<FakeModel>()), Times.Exactly(2));
            ruleForValid2Mock.Verify(v => v.CheckAsync(It.IsAny<FakeModel>()), Times.Exactly(2));
            ruleForInvalidMock.Verify(v => v.CheckAsync(It.IsAny<FakeModel>()), Times.Never());
            
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.NotNull(result3);
            Assert.True(result1.IsSuccess);
            Assert.True(result2.IsSuccess);
            Assert.True(result3.IsSuccess);
        }

        [Fact]
        public void WhenNoRulesFails_TheResultIs_Successful()
        {
            var modelMock = new Mock<FakeModel>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var ruleFor1Mock = new Mock<IRuleFor<FakeModel>>();
            var ruleFor2Mock = new Mock<IRuleFor<FakeModel>>();

            ruleFor1Mock.Setup(s => s.CheckAsync(It.IsAny<FakeModel>()))
                .ReturnsAsync(RuleCheckResult.Success);
            
            ruleFor2Mock.Setup(s => s.CheckAsync(It.IsAny<FakeModel>()))
                .ReturnsAsync(RuleCheckResult.Success);

            serviceProviderMock
                .Setup(p => p.GetService(typeof(IEnumerable<IRuleFor<FakeModel>>)))
                .Returns(new List<IRuleFor<FakeModel>>(new[]
                {
                    ruleFor1Mock.Object,
                    ruleFor2Mock.Object
                }));

            var ruleset = new RuleSet<FakeModel>(serviceProviderMock.Object);
            var result = ruleset.Check(modelMock.Object);
            
            serviceProviderMock.Verify(
                v => v.GetService(typeof(IEnumerable<IRuleFor<FakeModel>>)),
                Times.Exactly(1));
            
            ruleFor1Mock.Verify(v => v.CheckAsync(It.IsAny<FakeModel>()), Times.Exactly(1));
            ruleFor2Mock.Verify(v => v.CheckAsync(It.IsAny<FakeModel>()), Times.Exactly(1));
            
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void WhenAnyRulesFails_TheResultIs_Unsuccessful()
        {
            var modelMock = new Mock<FakeModel>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var ruleFor1Mock = new Mock<IRuleFor<FakeModel>>();
            var ruleFor2Mock = new Mock<IRuleFor<FakeModel>>();
            var ruleFor3Mock = new Mock<IRuleFor<FakeModel>>();

            ruleFor1Mock.Setup(s => s.CheckAsync(It.IsAny<FakeModel>()))
                .ReturnsAsync(RuleCheckResult.Success);
            
            ruleFor2Mock.Setup(s => s.CheckAsync(It.IsAny<FakeModel>()))
                .ReturnsAsync(RuleCheckResult.Fail);
            
            ruleFor3Mock.Setup(s => s.CheckAsync(It.IsAny<FakeModel>()))
                .ReturnsAsync(RuleCheckResult.Success);

            serviceProviderMock
                .Setup(p => p.GetService(typeof(IEnumerable<IRuleFor<FakeModel>>)))
                .Returns(new List<IRuleFor<FakeModel>>(new[]
                {
                    ruleFor1Mock.Object,
                    ruleFor2Mock.Object,
                    ruleFor3Mock.Object,
                }));

            var ruleset = new RuleSet<FakeModel>(serviceProviderMock.Object);
            var result = ruleset.Check(modelMock.Object);
            
            serviceProviderMock.Verify(
                v => v.GetService(typeof(IEnumerable<IRuleFor<FakeModel>>)),
                Times.Exactly(1));
            
            ruleFor1Mock.Verify(v => v.CheckAsync(It.IsAny<FakeModel>()), Times.Exactly(1));
            ruleFor2Mock.Verify(v => v.CheckAsync(It.IsAny<FakeModel>()), Times.Exactly(1));
            ruleFor3Mock.Verify(v => v.CheckAsync(It.IsAny<FakeModel>()), Times.Exactly(1));
            
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void WhenRuleFor_RaisesAnException_TheResultIs_Unsuccessful()
        {
            var modelMock = new Mock<FakeModel>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var ruleForMock = new Mock<IRuleFor<FakeModel>>();

            ruleForMock.Setup(s => s.CheckAsync(It.IsAny<FakeModel>())).Throws(new Exception());
            
            serviceProviderMock
                .Setup(p => p.GetService(typeof(IEnumerable<IRuleFor<FakeModel>>)))
                .Returns(new List<IRuleFor<FakeModel>>(new[]
                {
                    ruleForMock.Object
                }));

            var ruleset = new RuleSet<FakeModel>(serviceProviderMock.Object);
            var result = ruleset.Check(modelMock.Object);
            
            serviceProviderMock.Verify(
                v => v.GetService(typeof(IEnumerable<IRuleFor<FakeModel>>)),
                Times.Exactly(1));
            
            ruleForMock.Verify(v => v.CheckAsync(It.IsAny<FakeModel>()), Times.Exactly(1));
            
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void FailedRules_AreListedAs_Nonconformities()
        {
            var modelMock = new Mock<FakeModel>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var ruleFor1Mock = new Mock<IRuleFor<FakeModel>>();
            var ruleFor2Mock = new Mock<IRuleFor<FakeModel>>();
            var ruleFor3Mock = new Mock<IRuleFor<FakeModel>>();

            ruleFor1Mock.Setup(s => s.Code).Returns("Code1");
            ruleFor1Mock.Setup(s => s.CheckAsync(It.IsAny<FakeModel>()))
                .ReturnsAsync(RuleCheckResult.Fail);
            
            ruleFor2Mock.Setup(s => s.Code).Returns("Code2");
            ruleFor2Mock.Setup(s => s.CheckAsync(It.IsAny<FakeModel>()))
                .ReturnsAsync(RuleCheckResult.Fail);
            
            ruleFor3Mock.Setup(s => s.Code).Returns("Code3");
            ruleFor3Mock.Setup(s => s.CheckAsync(It.IsAny<FakeModel>()))
                .ReturnsAsync(RuleCheckResult.Fail);

            serviceProviderMock
                .Setup(p => p.GetService(typeof(IEnumerable<IRuleFor<FakeModel>>)))
                .Returns(new List<IRuleFor<FakeModel>>(new[]
                {
                    ruleFor1Mock.Object,
                    ruleFor2Mock.Object,
                    ruleFor3Mock.Object,
                }));

            var ruleset = new RuleSet<FakeModel>(serviceProviderMock.Object);
            var result = ruleset.Check(modelMock.Object);
            
            serviceProviderMock.Verify(
                v => v.GetService(typeof(IEnumerable<IRuleFor<FakeModel>>)),
                Times.Exactly(1));
            
            ruleFor1Mock.Verify(v => v.CheckAsync(It.IsAny<FakeModel>()), Times.Exactly(1));
            ruleFor2Mock.Verify(v => v.CheckAsync(It.IsAny<FakeModel>()), Times.Exactly(1));
            ruleFor3Mock.Verify(v => v.CheckAsync(It.IsAny<FakeModel>()), Times.Exactly(1));
            
            Assert.NotNull(result);
            Assert.NotNull(result.Unconformities);
            Assert.False(result.IsSuccess);
            Assert.Equal(3, result.Unconformities.Count);
            Assert.Equal("Code1", result.Unconformities.First().Key);
            Assert.Equal(nameof(RuleCheckResult.Fail), result.Unconformities.First().Value);
            Assert.Equal("Code2", result.Unconformities.Skip(1).First().Key);
            Assert.Equal(nameof(RuleCheckResult.Fail), result.Unconformities.Skip(1).First().Value);
            Assert.Equal("Code3", result.Unconformities.Skip(2).First().Key);
            Assert.Equal(nameof(RuleCheckResult.Fail), result.Unconformities.Skip(2).First().Value);
        }

        #region Fakes

        public class FakeModel
        {
            public int Number { get; set; }
            public string Name { get; set; }
        }

        public interface IFakeModel2Fail
        {
            RuleCheckResult GetFail();
        }

        public class FakeModel2Fail : IFakeModel2Fail
        {
            public RuleCheckResult GetFail()
            {
                return RuleCheckResult.Fail;
            }
        }

        #endregion
    }
}
