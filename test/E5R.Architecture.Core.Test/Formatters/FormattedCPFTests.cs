using System;
using E5R.Architecture.Core.Formatters;
using Xunit;

namespace E5R.Architecture.Core.Test.Formatters
{
    public class FormattedCpfTests
    {
        [Fact]
        public void Must_Format_An_Literal_Unformatted_CPF_String()
        {
            const string expected = "123.456.789-01";
            string calculated = (FormattedCpf)"12345678901";

            Assert.Equal(expected, calculated);
            Assert.Equal(expected, (FormattedCpf)"12345678901");
            Assert.Equal(expected, (string)(FormattedCpf)"12345678901");
        }

        [Theory]
        [InlineData("1", "000.000.000-01")]
        [InlineData("12", "000.000.000-12")]
        [InlineData("123", "000.000.001-23")]
        [InlineData("1234", "000.000.012-34")]
        [InlineData("12345", "000.000.123-45")]
        [InlineData("123456", "000.001.234-56")]
        [InlineData("1234567", "000.012.345-67")]
        [InlineData("12345678", "000.123.456-78")]
        [InlineData("123456789", "001.234.567-89")]
        [InlineData("1234567890", "012.345.678-90")]
        [InlineData("01235678901", "012.356.789-01")]
        [InlineData("99999999999", "999.999.999-99")]
        [InlineData("00000000000", "000.000.000-00")]
        public void Must_Format_An_Unformatted_CPF_String(string unformatted, string formatted)
        {
            string calculated = (FormattedCpf)unformatted;

            Assert.Equal(formatted, calculated);
            Assert.Equal(formatted, (FormattedCpf)unformatted);
            Assert.Equal(formatted, (string)(FormattedCpf)unformatted);
        }

        [Theory]
        [InlineData("c")]
        [InlineData("!")]
        [InlineData("01235678900#")]
        public void Only_Number_Is_Accepted_On_String(string value)
        {
            Assert.Throws<FormatException>(() => { string calculated = (FormattedCpf)value; });
        }

        [Theory]
        [InlineData("000000000000")]
        public void Max_11Chars(string value)
        {
            Assert.Throws<FormatException>(() => {
                string calculated = (FormattedCpf)value;
            });
        }
    }
}
