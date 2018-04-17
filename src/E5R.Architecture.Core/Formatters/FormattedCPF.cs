using System;

namespace E5R.Architecture.Core.Formatters
{
    public class FormattedCpf
    {
        private long _value;

        /// <summary>
        /// Format Brazilian CPF string
        /// </summary>
        /// <param name="value">Value of unformatted CPF</param>
        private FormattedCpf(string value)
        {
            value = value
                    ?? throw new ArgumentNullException(nameof(value));

            EnsureLongValue(value);
        }

        private void EnsureLongValue(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length > 11 ||
                !long.TryParse(value, out _value))
            {
                throw new FormatException("Invalid unformatted CPF string value");
            }
        }

        private string Value => _value.ToString(@"000\.000\.000\-00");

        /// <summary>
        /// Conversion from <see cref="string" /> to <see cref="FormattedCpf" />
        /// </summary>
        /// <param name="unformatted">Unformatted CPF string</param>
        public static explicit operator FormattedCpf(string unformatted)
        {
            return new FormattedCpf(unformatted);
        }

        /// <summary>
        /// Conversion from <see cref="FormattedCpf" /> to <see cref="string" />
        /// </summary>
        /// <param name="formatter">CPF formatter</param>
        public static implicit operator string(FormattedCpf formatter)
        {
            return formatter.Value;
        }
    }
}
