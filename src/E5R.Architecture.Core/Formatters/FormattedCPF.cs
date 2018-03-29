using System;

namespace E5R.Architecture.Core.Formatters
{
    public class FormattedCPF
    {
        private long _value;

        /// <summary>
        /// Format Brazilian CPF string
        /// </summary>
        /// <param name="formatted"></param>
        public FormattedCPF(string value)
        {
            value = value
                ?? throw new ArgumentNullException(nameof(value));

            EnsureLongValue(value);
        }

        private void EnsureLongValue(string value)
        {
            if(string.IsNullOrEmpty(value) || value.Length > 11 || !long.TryParse(value, out _value))
            {
                throw new FormatException("Invalid unformatted CPF string value");
            }
        }

        private string Value
        {
            get
            {
                return _value.ToString(@"000\.000\.000\-00");
            }
        }

        /// <summary>
        /// Conversion from <see cref="string" /> to <see cref="FormattedCPF" />
        /// </summary>
        /// <param name="unformatted">Unformatted CPF string</param>
        public static explicit operator FormattedCPF(string unformatted)
        {
            return new FormattedCPF(unformatted);
        }

        /// <summary>
        /// Conversion from <see cref="FormattedCPF" /> to <see cref="string" />
        /// </summary>
        /// <param name="formatter">CPF formatter</param>
        public static implicit operator string(FormattedCPF formatter)
        {
            return formatter.Value;
        }
    }
}
