using System;
using System.Linq;

namespace E5R.Architecture.Core.Formatters
{
    public class UnformattedCPF
    {
        private string _value;

        /// <summary>
        /// Unformat Brazilian CPF string
        /// </summary>
        /// <param name="formatted"></param>
        public UnformattedCPF(string value)
        {
            value = value
                ?? throw new ArgumentNullException(nameof(value));

            EnsureValue(value);
        }

        private void EnsureValue(string value)
        {
            _value = value
                ?.Replace(".", string.Empty)
                ?.Replace("-", string.Empty);

            if(string.IsNullOrEmpty(_value) || _value.Length > 11 || _value.Any(c => !char.IsNumber(c)))
            {
                throw new FormatException("Invalid formatted CPF string value");
            }
        }

        /// <summary>
        /// Conversion from <see cref="string" /> to <see cref="UnformattedCPF" />
        /// </summary>
        /// <param name="formatted">Formatted CPF string</param>
        public static explicit operator UnformattedCPF(string formatted)
        {
            return new UnformattedCPF(formatted);
        }

        /// <summary>
        /// Conversion from <see cref="UnformattedCPF" /> to <see cref="string" />
        /// </summary>
        /// <param name="unformatter">CPF unformatter</param>
        public static implicit operator string(UnformattedCPF unformatter)
        {
            return unformatter._value;
        }
    }
}
