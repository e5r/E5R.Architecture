// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Text;

namespace E5R.Architecture.Core.Extensions
{
    public static partial class StringExtensions
    {
        public static byte[] ToBytes(this string inputString, Encoding encoding)
        {
            Checker.NotEmptyArgument(inputString, nameof(inputString));
            Checker.NotNullArgument(encoding, nameof(encoding));

            return encoding.GetBytes(inputString);
        }

        public static byte[] ToBytes(this string inputString) =>
            ToBytes(inputString, Encoding.UTF8);
    }
}
