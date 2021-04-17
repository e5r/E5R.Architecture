// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Security.Cryptography;

namespace E5R.Architecture.Core.Extensions
{
    public static partial class ByteArrayExtensions
    {
        /// <summary>
        /// Computes HMAC of bytes
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="hmac">The <see cref="HMAC"/> encryption algorithm</param>
        /// <returns>The computed <see cref="HMAC"/> hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or is null or empty, or <paramref name="hmac"/> is null
        /// </exception>
        public static byte[] Hmac(this byte[] bytes, HMAC hmac)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullArgument(hmac, nameof(hmac));

            return Hash(bytes, hmac);
        }

        /// <summary>
        /// Computes HMAC of bytes
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="hmac">The <see cref="HMAC"/> encryption algorithm</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMAC"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or is null or empty, or <paramref name="hmac"/> is null
        /// </exception>
        public static string HmacHex(this byte[] bytes, HMAC hmac) => HmacHex(bytes, hmac, false);

        /// <summary>
        /// Computes HMAC of bytes
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="hmac">The <see cref="HMAC"/> encryption algorithm</param>
        /// <param name="upperCaseOutput">Generate uppercase output</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMAC"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or is null or empty, or <paramref name="hmac"/> is null
        /// </exception>
        public static string HmacHex(this byte[] bytes, HMAC hmac, bool upperCaseOutput)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullArgument(hmac, nameof(hmac));

            return HashHex(bytes, hmac, upperCaseOutput);
        }

        #region Byte alias

        /// <summary>
        /// Computes HMAC of bytes by using the <see cref="HMACMD5"/> hash function
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="key">The secret key for <see cref="HMACMD5"/> encryption</param>
        /// <returns>The computed <see cref="HMACMD5"/> hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static byte[] HmacMd5(this byte[] bytes, byte[] key)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return Hmac(bytes, new HMACMD5(key));
        }

        /// <summary>
        /// Computes HMAC of bytes by using the <see cref="HMACSHA1"/> hash function
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="key">The secret key for <see cref="HMACSHA1"/> encryption</param>
        /// <returns>The computed <see cref="HMACSHA1"/> hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static byte[] HmacSha1(this byte[] bytes, byte[] key)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return Hmac(bytes, new HMACSHA1(key));
        }

        /// <summary>
        /// Computes HMAC of bytes by using the <see cref="HMACSHA256"/> hash function
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="key">The secret key for <see cref="HMACSHA256"/> encryption</param>
        /// <returns>The computed <see cref="HMACSHA256"/> hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static byte[] HmacSha256(this byte[] bytes, byte[] key)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return Hmac(bytes, new HMACSHA256(key));
        }

        /// <summary>
        /// Computes HMAC of bytes by using the <see cref="HMACSHA384"/> hash function
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="key">The secret key for <see cref="HMACSHA384"/> encryption</param>
        /// <returns>The computed <see cref="HMACSHA384"/> hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static byte[] HmacSha384(this byte[] bytes, byte[] key)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return Hmac(bytes, new HMACSHA384(key));
        }

        /// <summary>
        /// Computes HMAC of bytes by using the <see cref="HMACSHA512"/> hash function
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="key">The secret key for <see cref="HMACSHA512"/> encryption</param>
        /// <returns>The computed <see cref="HMACSHA512"/> hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static byte[] HmacSha512(this byte[] bytes, byte[] key)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return Hmac(bytes, new HMACSHA512(key));
        }

        #endregion

        #region String lowercase alias

        /// <summary>
        /// Computes HMAC of bytes by using the <see cref="HMACMD5"/> hash function
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="key">The secret key for <see cref="HMACMD5"/> encryption</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACMD5"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacMd5Hex(this byte[] bytes, byte[] key)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(bytes, new HMACMD5(key), false);
        }

        /// <summary>
        /// Computes HMAC of bytes by using the <see cref="HMACSHA1"/> hash function
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="key">The secret key for <see cref="HMACSHA1"/> encryption</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACSHA1"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacSha1Hex(this byte[] bytes, byte[] key)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(bytes, new HMACSHA1(key), false);
        }

        /// <summary>
        /// Computes HMAC of bytes by using the <see cref="HMACSHA256"/> hash function
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="key">The secret key for <see cref="HMACSHA256"/> encryption</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACSHA256"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacSha256Hex(this byte[] bytes, byte[] key)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(bytes, new HMACSHA256(key), false);
        }

        /// <summary>
        /// Computes HMAC of bytes by using the <see cref="HMACSHA384"/> hash function
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="key">The secret key for <see cref="HMACSHA384"/> encryption</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACSHA384"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacSha384Hex(this byte[] bytes, byte[] key)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(bytes, new HMACSHA384(key), false);
        }

        /// <summary>
        /// Computes HMAC of bytes by using the <see cref="HMACSHA512"/> hash function
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="key">The secret key for <see cref="HMACSHA512"/> encryption</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACSHA512"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacSha512Hex(this byte[] bytes, byte[] key)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(bytes, new HMACSHA512(key), false);
        }

        #endregion

        #region String alias

        /// <summary>
        /// Computes HMAC of bytes by using the <see cref="HMACMD5"/> hash function
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="key">The secret key for <see cref="HMACMD5"/> encryption</param>
        /// <param name="upperCaseOutput">Generate uppercase output</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACMD5"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacMd5Hex(this byte[] bytes, byte[] key, bool upperCaseOutput)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(bytes, new HMACMD5(key), upperCaseOutput);
        }

        /// <summary>
        /// Computes HMAC of bytes by using the <see cref="HMACSHA1"/> hash function
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="key">The secret key for <see cref="HMACSHA1"/> encryption</param>
        /// <param name="upperCaseOutput">Generate uppercase output</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACSHA1"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacSha1Hex(this byte[] bytes, byte[] key, bool upperCaseOutput)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(bytes, new HMACSHA1(key), upperCaseOutput);
        }

        /// <summary>
        /// Computes HMAC of bytes by using the <see cref="HMACSHA256"/> hash function
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="key">The secret key for <see cref="HMACSHA256"/> encryption</param>
        /// <param name="upperCaseOutput">Generate uppercase output</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACSHA256"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacSha256Hex(this byte[] bytes, byte[] key, bool upperCaseOutput)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(bytes, new HMACSHA256(key), upperCaseOutput);
        }

        /// <summary>
        /// Computes HMAC of bytes by using the <see cref="HMACSHA384"/> hash function
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="key">The secret key for <see cref="HMACSHA384"/> encryption</param>
        /// <param name="upperCaseOutput">Generate uppercase output</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACSHA384"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacSha384Hex(this byte[] bytes, byte[] key, bool upperCaseOutput)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(bytes, new HMACSHA384(key), upperCaseOutput);
        }

        /// <summary>
        /// Computes HMAC of bytes by using the <see cref="HMACSHA512"/> hash function
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="key">The secret key for <see cref="HMACSHA512"/> encryption</param>
        /// <param name="upperCaseOutput">Generate uppercase output</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACSHA512"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacSha512Hex(this byte[] bytes, byte[] key, bool upperCaseOutput)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(bytes, new HMACSHA512(key), upperCaseOutput);
        }

        #endregion
    }
}
