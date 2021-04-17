// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Text;
using E5R.Architecture.Core.Extensions;
using Xunit;

namespace E5R.Architecture.Core.Test
{
    public class StringExtensionsTests
    {
        [Fact]
        public void ToBytes_RequiresAllArguments()
        {
            var ex1 = Assert.Throws<ArgumentNullException>(() =>
                StringExtensions.ToBytes(null, Encoding.Default));
            var ex2 = Assert.Throws<ArgumentNullException>(() =>
                string.Empty.ToBytes(Encoding.Default));
            var ex3 =
                Assert.Throws<ArgumentNullException>(() => "E5R".ToBytes(null));

            Assert.Equal("inputString", ex1.ParamName);
            Assert.Equal("inputString", ex2.ParamName);
            Assert.Equal("encoding", ex3.ParamName);
        }

        [Fact]
        public void ToBytes_ConvertFromCorrectEncodedString()
        {
            var phrase = "E5R Development Team";
            var buffer1 = Encoding.UTF8.GetBytes(phrase);
            var buffer2 = Encoding.BigEndianUnicode.GetBytes(phrase);

            var convertedBuffer1 = phrase.ToBytes(Encoding.UTF8);
            var convertedBuffer2 = phrase.ToBytes(Encoding.BigEndianUnicode);

            Assert.Equal(buffer1, convertedBuffer1);
            Assert.Equal(buffer2, convertedBuffer2);
            Assert.NotEqual(buffer1, buffer2);
        }

        [Fact]
        public void ToBytes_ConvertFromDefaultUTF8String()
        {
            var phrase = "E5R Development Team";
            var buffer1 = Encoding.UTF8.GetBytes(phrase);
            var buffer2 = Encoding.BigEndianUnicode.GetBytes(phrase);

            var convertedBuffer = phrase.ToBytes();

            Assert.Equal(buffer1, convertedBuffer);
            Assert.NotEqual(buffer1, buffer2);
            Assert.NotEqual(buffer2, convertedBuffer);
        }
    }
}
