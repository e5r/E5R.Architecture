// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using  E5R.Architecture.Core.Formatters;
using Xunit;

namespace E5R.Architecture.Core.Test.Formatters
{
    public class UnformattedCpfTests
    {
        [Fact]
        public void Must_Unformat_An_Literal_Formatted_CPF_String()
        {
            const string expected = "12345678901";
            string calculated = (UnformattedCpf)"123.456.789-01";

            Assert.Equal(expected, calculated);
            Assert.Equal(expected, (UnformattedCpf)"123.456.789-01");
            Assert.Equal(expected, (string)(UnformattedCpf)"123.456.789-01");
        }

        [Theory]
        [InlineData("1", "1")]
        [InlineData("12", "-12")]
        [InlineData("123", "1-23")]
        [InlineData("1234", "12-34")]
        [InlineData("12345", ".123-45")]
        [InlineData("123456", "1.234-56")]
        [InlineData("1234567", "12.345-67")]
        [InlineData("12345678", ".123.456-78")]
        [InlineData("123456789", "1.234.567-89")]
        [InlineData("1234567890", "12.345.678-90")]
        [InlineData("01235678901", "012.356.789-01")]
        [InlineData("99999999999", "999.999.999-99")]
        [InlineData("00000000000", "000.000.000-00")]
        public void Must_Unformat_An_Formatted_CPF_String(string unformatted, string formatted)
        {
            string calculated = (UnformattedCpf)formatted;

            Assert.Equal(unformatted, calculated);
            Assert.Equal(unformatted, (UnformattedCpf)formatted);
            Assert.Equal(unformatted, (string)(UnformattedCpf)formatted);
        }

        [Theory]
        [InlineData("c")]
        [InlineData("!")]
        [InlineData("01235678900#")]
        [InlineData("012.35678900#")]
        [InlineData("012-5678900#")]
        public void Only_Number_Dot_And_Dash_Is_Accepted_On_String(string value)
        {
            Assert.Throws<FormatException>(() => { string calculated = (UnformattedCpf)value; });
        }

        [Theory]
        [InlineData("000.000.000-000")]
        public void Max_11Numeric_Chars(string value)
        {
            Assert.Throws<FormatException>(() => {
                string calculated = (UnformattedCpf)value;
            });
        }
    }
}
