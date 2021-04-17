// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Security.Cryptography;
using System.Text;

namespace E5R.Architecture.Core.Extensions
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// Computes HMAC of <see cref="String"/>
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="hmac">The <see cref="HMAC"/> encryption algorithm</param>
        /// <returns>The computed <see cref="HMAC"/> hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty, or <paramref name="hmac"/>
        /// is null
        /// </exception>
        public static byte[] Hmac(this string inputString, HMAC hmac) =>
            Hmac(inputString, hmac, Encoding.UTF8);

        /// <summary>
        /// Computes HMAC of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="hmac">The <see cref="HMAC"/> encryption algorithm</param>
        /// <param name="encoding">Encoding</param>
        /// <returns>The computed <see cref="HMAC"/> hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty, or <paramref name="hmac"/>
        /// is null, or <paramref name="encoding"/> is null
        /// </exception>
        public static byte[] Hmac(this string inputString, HMAC hmac,
            Encoding encoding)
        {
            Checker.NotEmptyArgument(inputString, nameof(inputString));
            Checker.NotNullArgument(hmac, nameof(hmac));

            return encoding.GetBytes(inputString).Hmac(hmac);
        }

        /// <summary>
        /// Computes HMAC of <see cref="String"/>
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="hmac">The <see cref="HMAC"/> encryption algorithm</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMAC"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty, or <paramref name="hmac"/> is null
        /// </exception>
        public static string HmacHex(this string inputString, HMAC hmac) =>
            HmacHex(inputString, hmac, Encoding.UTF8, false);

        /// <summary>
        /// Computes HMAC of <see cref="String"/>
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="hmac">The <see cref="HMAC"/> encryption algorithm</param>
        /// <param name="upperCaseOutput">If the output is to be uppercase</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMAC"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty, or <paramref name="hmac"/> is null
        /// </exception>
        public static string HmacHex(this string inputString, HMAC hmac,
            bool upperCaseOutput) =>
            HmacHex(inputString, hmac, Encoding.UTF8, upperCaseOutput);

        /// <summary>
        /// Computes HMAC of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="hmac">The <see cref="HMAC"/> encryption algorithm</param>
        /// <param name="encoding">Encoding</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMAC"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty, or <paramref name="hmac"/>, or
        /// <paramref name="encoding"/> is null
        /// </exception>
        public static string HmacHex(this string inputString, HMAC hmac,
            Encoding encoding) => HmacHex(inputString, hmac, encoding, false);

        /// <summary>
        /// Computes HMAC of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="hmac">The <see cref="HMAC"/> encryption algorithm</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="upperCaseOutput">If the output is to be uppercase</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMAC"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty, or <paramref name="hmac"/>, or
        /// <paramref name="encoding"/> is null
        /// </exception>
        public static string HmacHex(this string inputString, HMAC hmac,
            Encoding encoding, bool upperCaseOutput)
        {
            Checker.NotEmptyOrWhiteArgument(inputString, nameof(inputString));
            Checker.NotNullArgument(hmac, nameof(hmac));
            Checker.NotNullArgument(encoding, nameof(encoding));

            return encoding.GetBytes(inputString).HmacHex(hmac, upperCaseOutput);
        }

        #region UTF-8 byte alias

        /// <summary>
        /// Computes HMAC of <see cref="String"/> by using the <see cref="HMACMD5"/> hash function
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="key">The secret key for <see cref="HMACMD5"/> encryption</param>
        /// <returns>The computed <see cref="HMACMD5"/> hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static byte[] HmacMd5(this string inputString, byte[] key)
        {
            Checker.NotEmptyOrWhiteArgument(inputString, nameof(inputString));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return Hmac(inputString, new HMACMD5(key));
        }

        /// <summary>
        /// Computes HMAC of <see cref="String"/> by using the <see cref="HMACSHA1"/> hash function
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="key">The secret key for <see cref="HMACSHA1"/> encryption</param>
        /// <returns>The computed <see cref="HMACSHA1"/> hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static byte[] HmacSha1(this string inputString, byte[] key)
        {
            Checker.NotEmptyOrWhiteArgument(inputString, nameof(inputString));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return Hmac(inputString, new HMACSHA1(key));
        }

        /// <summary>
        /// Computes HMAC of <see cref="String"/> by using the <see cref="HMACSHA256"/> hash function
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="key">The secret key for <see cref="HMACSHA256"/> encryption</param>
        /// <returns>The computed <see cref="HMACSHA256"/> hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static byte[] HmacSha256(this string inputString, byte[] key)
        {
            Checker.NotEmptyOrWhiteArgument(inputString, nameof(inputString));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return Hmac(inputString, new HMACSHA256(key));
        }

        /// <summary>
        /// Computes HMAC of <see cref="String"/> by using the <see cref="HMACSHA384"/> hash function
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="key">The secret key for <see cref="HMACSHA384"/> encryption</param>
        /// <returns>The computed <see cref="HMACSHA384"/> hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static byte[] HmacSha384(this string inputString, byte[] key)
        {
            Checker.NotEmptyOrWhiteArgument(inputString, nameof(inputString));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return Hmac(inputString, new HMACSHA384(key));
        }

        /// <summary>
        /// Computes HMAC of <see cref="String"/> by using the <see cref="HMACSHA512"/> hash function
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="key">The secret key for <see cref="HMACSHA512"/> encryption</param>
        /// <returns>The computed <see cref="HMACSHA512"/> hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static byte[] HmacSha512(this string inputString, byte[] key)
        {
            Checker.NotEmptyOrWhiteArgument(inputString, nameof(inputString));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return Hmac(inputString, new HMACSHA512(key));
        }

        #endregion

        #region UTF-8 string lowercase alias

        /// <summary>
        /// Computes HMAC of <see cref="String"/> by using the <see cref="HMACMD5"/> hash function
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="key">The secret key for <see cref="HMACMD5"/> encryption</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACMD5"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacMd5Hex(this string inputString, byte[] key)
        {
            Checker.NotEmptyOrWhiteArgument(inputString, nameof(inputString));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(inputString, new HMACMD5(key));
        }

        /// <summary>
        /// Computes HMAC of <see cref="String"/> by using the <see cref="HMACSHA1"/> hash function
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="key">The secret key for <see cref="HMACSHA1"/> encryption</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACSHA1"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacSha1Hex(this string inputString, byte[] key)
        {
            Checker.NotEmptyOrWhiteArgument(inputString, nameof(inputString));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(inputString, new HMACSHA1(key));
        }

        /// <summary>
        /// Computes HMAC of <see cref="String"/> by using the <see cref="HMACSHA256"/> hash function
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="key">The secret key for <see cref="HMACSHA256"/> encryption</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACSHA256"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacSha256Hex(this string inputString, byte[] key)
        {
            Checker.NotEmptyOrWhiteArgument(inputString, nameof(inputString));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(inputString, new HMACSHA256(key));
        }

        /// <summary>
        /// Computes HMAC of <see cref="String"/> by using the <see cref="HMACSHA384"/> hash function
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="key">The secret key for <see cref="HMACSHA384"/> encryption</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACSHA384"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacSha384Hex(this string inputString, byte[] key)
        {
            Checker.NotEmptyOrWhiteArgument(inputString, nameof(inputString));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(inputString, new HMACSHA384(key));
        }

        /// <summary>
        /// Computes HMAC of <see cref="String"/> by using the <see cref="HMACSHA512"/> hash function
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="key">The secret key for <see cref="HMACSHA512"/> encryption</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACSHA512"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacSha512Hex(this string inputString, byte[] key)
        {
            Checker.NotEmptyOrWhiteArgument(inputString, nameof(inputString));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(inputString, new HMACSHA512(key));
        }

        #endregion

        #region UTF-8 string alias

        /// <summary>
        /// Computes HMAC of <see cref="String"/> by using the <see cref="HMACMD5"/> hash function
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="key">The secret key for <see cref="HMACMD5"/> encryption</param>
        /// <param name="upperCaseOutput">If the output is to be uppercase</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACMD5"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacMd5Hex(this string inputString, byte[] key,
            bool upperCaseOutput)
        {
            Checker.NotEmptyOrWhiteArgument(inputString, nameof(inputString));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(inputString, new HMACMD5(key), upperCaseOutput);
        }

        /// <summary>
        /// Computes HMAC of <see cref="String"/> by using the <see cref="HMACSHA1"/> hash function
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="key">The secret key for <see cref="HMACSHA1"/> encryption</param>
        /// <param name="upperCaseOutput">If the output is to be uppercase</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACSHA1"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacSha1Hex(this string inputString, byte[] key,
            bool upperCaseOutput)
        {
            Checker.NotEmptyOrWhiteArgument(inputString, nameof(inputString));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(inputString, new HMACSHA1(key), upperCaseOutput);
        }

        /// <summary>
        /// Computes HMAC of <see cref="String"/> by using the <see cref="HMACSHA256"/> hash function
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="key">The secret key for <see cref="HMACSHA256"/> encryption</param>
        /// <param name="upperCaseOutput">If the output is to be uppercase</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACSHA256"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacSha256Hex(this string inputString, byte[] key,
            bool upperCaseOutput)
        {
            Checker.NotEmptyOrWhiteArgument(inputString, nameof(inputString));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(inputString, new HMACSHA256(key), upperCaseOutput);
        }

        /// <summary>
        /// Computes HMAC of <see cref="String"/> by using the <see cref="HMACSHA384"/> hash function
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="key">The secret key for <see cref="HMACSHA384"/> encryption</param>
        /// <param name="upperCaseOutput">If the output is to be uppercase</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACSHA384"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacSha384Hex(this string inputString, byte[] key,
            bool upperCaseOutput)
        {
            Checker.NotEmptyOrWhiteArgument(inputString, nameof(inputString));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(inputString, new HMACSHA384(key), upperCaseOutput);
        }

        /// <summary>
        /// Computes HMAC of <see cref="String"/> by using the <see cref="HMACSHA512"/> hash function
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="key">The secret key for <see cref="HMACSHA512"/> encryption</param>
        /// <param name="upperCaseOutput">If the output is to be uppercase</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed <see cref="HMACSHA512"/>
        /// hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> or <paramref name="key"/> is null or empty
        /// </exception>
        public static string HmacSha512Hex(this string inputString, byte[] key,
            bool upperCaseOutput)
        {
            Checker.NotEmptyOrWhiteArgument(inputString, nameof(inputString));
            Checker.NotNullOrEmptyArgument(key, nameof(key));

            return HmacHex(inputString, new HMACSHA512(key), upperCaseOutput);
        }

        #endregion
    }
}
