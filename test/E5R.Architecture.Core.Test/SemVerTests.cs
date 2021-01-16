// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using Xunit;

namespace E5R.Architecture.Core.Test
{
    public class SemVerTests
    {
        [Theory]
        [InlineData(0,0,0, null, "0.0.0")]
        [InlineData(1,0,0, null, "1.0.0")]
        [InlineData(0,1,0, null, "0.1.0")]
        [InlineData(0,0,1, null, "0.0.1")]
        [InlineData(1,0,0, "build", "1.0.0-build")]
        [InlineData(1,0,0, "alpha-4", "1.0.0-alpha-4")]
        [InlineData(1,0,0, "ci-build-999", "1.0.0-ci-build-999")]
        public void Should_Return_Matching_String(int major, int minor, int patch, string label, string version)
        {
            var semver = new SemVer { Major = major, Minor = minor, Patch = patch, Label = label };
            Assert.Equal(version, semver.ToString());
        }
    }
}
