using System;
using Xunit;

namespace E5R.Architecture.Core.Tests.Formatters
{
    using Core.Formatters;

    public class UnformattedCPFTests
    {
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
            string calculated = (UnformattedCPF)formatted;

            Assert.Equal(unformatted, calculated);
            Assert.Equal(unformatted, (UnformattedCPF)formatted);
            Assert.Equal(unformatted, (string)(UnformattedCPF)formatted);
        }

        [Theory]
        [InlineData("c")]
        [InlineData("!")]
        [InlineData("01235678900#")]
        [InlineData("012.35678900#")]
        [InlineData("012-5678900#")]
        public void Only_Number_Dot_And_Dash_Is_Accepted_On_String(string value)
        {
            Assert.Throws<FormatException>(() => { string calculated = (UnformattedCPF)value; });
        }

        [Theory]
        [InlineData("000.000.000-000")]
        public void Max_11Numeric_Chars(string value)
        {
            Assert.Throws<FormatException>(() => {
                string calculated = (UnformattedCPF)value;
            });
        }
    }
}
