// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Text;

namespace E5R.Architecture.Core.Extensions
{
    public static partial class ByteArrayExtensions
    {
        /// <summary>
        /// Converts an byte array to <see cref="string"/>
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="encoding">The <see cref="Encoding"/></param>
        /// <returns>The encoded <see cref="string"/></returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty, or <paramref name="encoding"/> is null
        /// </exception>
        public static string IntoString(this byte[] bytes, Encoding encoding)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullArgument(encoding, nameof(encoding));

            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Converts an byte array to <see cref="string"/>
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="bytes">The byte array</param>
        /// <returns>The encoded <see cref="string"/></returns>
        public static string IntoString(this byte[] bytes) => IntoString(bytes, Encoding.UTF8);
    }
}
