// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace E5R.Architecture.Core.Test
{
    public class RuleTests
    {
        [Theory]
        [InlineData("RN1", "My businness rule 1")]
        [InlineData("RN2", "My businness rule 2")]
        [InlineData("RN3", "My businness rule 3")]
        public void RuleFor_ExposesCodeAndDescription(string code, string description)
        {
            var rn = new DynamicRule(code, description);

            Assert.Equal(code, rn.Code);
            Assert.Equal(description, rn.Description);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("RN", null)]
        [InlineData("RN", "")]
        [InlineData(null, "My businness rule")]
        [InlineData("", "My businness rule")]
        public void RuleFor_RequiresValidCodeAndDescription(string code, string description)
        {
            var exception = Assert.Throws<ArgumentNullException>(
                () => new DynamicRule(code, description)
            );

            Assert.Matches("code|description", exception.ParamName);
        }
    }

    #region Mocks
    public class MyEmptyModel { }

    public class MyTreeProperties
    {
        public string One { get; set; }
        public string Two { get; set; }
        public string Tree { get; set; }
    }

    public class IsOkRule : RuleFor<MyEmptyModel>
    {
        public IsOkRule() : base("RN", "IsOk RN!") { }

        public override Task<RuleCheckResult> CheckAsync(MyEmptyModel target)
        {
            return Task.FromResult(RuleCheckResult.Success);
        }
    }

    public class DynamicRule : RuleFor<MyEmptyModel>
    {
        public DynamicRule(string code, string description)
            : base(code, description)
        { }

        public override Task<RuleCheckResult> CheckAsync(MyEmptyModel target)
        {
            return Task.FromResult(RuleCheckResult.Success);
        }
    }

    public class TwoUnconformitiesRule : RuleFor<MyTreeProperties>
    {
        public TwoUnconformitiesRule()
            : base("RN", "Two unconformities rule")
        { }

        public override Task<RuleCheckResult> CheckAsync(MyTreeProperties target)
        {
            return Task.FromResult(new RuleCheckResult(false, new Dictionary<string, string>
            {
                { "one","Unconformity one" },
                { "two","Unconformity two" }
            }));
        }
    }
    #endregion
}
