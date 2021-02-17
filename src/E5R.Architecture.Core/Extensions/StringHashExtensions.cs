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
        /// Computes hash of <see cref="String"/>
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="algorithm">The hash algorithm</param>
        /// <returns>The computed hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty, or <paramref name="algorithm"/>
        /// is null
        /// </exception>
        public static byte[] Hash(this string inputString, HashAlgorithm algorithm) =>
            Hash(inputString, algorithm, Encoding.UTF8);

        /// <summary>
        /// Computes hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="algorithm">The hash algorithm</param>
        /// <param name="encoding">Encoding</param>
        /// <returns>The computed hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty, or <paramref name="algorithm"/>
        /// is null, or <paramref name="encoding"/> is null
        /// </exception>
        public static byte[] Hash(this string inputString, HashAlgorithm algorithm,
            Encoding encoding)
        {
            Checker.NotEmptyArgument(inputString, nameof(inputString));
            Checker.NotNullArgument(algorithm, nameof(algorithm));

            return encoding.GetBytes(inputString).Hash(algorithm);
        }

        /// <summary>
        /// Computes hash of <see cref="String"/>
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="algorithm">The hash algorithm</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty, or <paramref name="algorithm"/>
        /// </exception>
        public static string HashHex(this string inputString, HashAlgorithm algorithm) =>
            HashHex(inputString, algorithm, Encoding.UTF8, false);

        /// <summary>
        /// Computes hash of <see cref="String"/>
        /// </summary>
        /// <remarks>Uses UTF-8 encoded <see cref="String"/></remarks>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="algorithm">The hash algorithm</param>
        /// <param name="upperCaseOutput">If the output is to be uppercase</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty, or <paramref name="algorithm"/>
        /// is null
        /// </exception>
        public static string HashHex(this string inputString, HashAlgorithm algorithm,
            bool upperCaseOutput) =>
            HashHex(inputString, algorithm, Encoding.UTF8, upperCaseOutput);

        /// <summary>
        /// Computes hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="algorithm">The hash algorithm</param>
        /// <param name="encoding">Encoding</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty, or <paramref name="algorithm"/>
        /// is null, or <paramref name="encoding"/> is null
        /// </exception>
        public static string HashHex(this string inputString, HashAlgorithm algorithm,
            Encoding encoding) => HashHex(inputString, algorithm, encoding, false);

        /// <summary>
        /// Computes hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="algorithm">The hash algorithm</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="upperCaseOutput">If the output is to be uppercase</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty, or <paramref name="algorithm"/>
        /// is null, or <paramref name="encoding"/> is null
        /// </exception>
        public static string HashHex(this string inputString, HashAlgorithm algorithm,
            Encoding encoding, bool upperCaseOutput)
        {
            Checker.NotEmptyOrWhiteArgument(inputString, nameof(inputString));
            Checker.NotNullArgument(algorithm, nameof(algorithm));
            Checker.NotNullArgument(encoding, nameof(encoding));

            return encoding.GetBytes(inputString).HashHex(algorithm, upperCaseOutput);
        }

        #region UTF-8 byte alias

        /// <summary>
        /// Computes MD5 hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <returns>The computed MD5 hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty array
        /// </exception>
        public static byte[] Md5(this string inputString) => Hash(inputString, MD5.Create());

        /// <summary>
        /// Computes SHA-1 hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <returns>The computed SHA-1 hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty array
        /// </exception>
        public static byte[] Sha1(this string inputString) => Hash(inputString, SHA1.Create());

        /// <summary>
        /// Computes SHA-256 hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <returns>The computed SHA-256 hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty array
        /// </exception>
        public static byte[] Sha256(this string inputString) => Hash(inputString, SHA256.Create());

        /// <summary>
        /// Computes SHA-384 hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <returns>The computed SHA-384 hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty array
        /// </exception>
        public static byte[] Sha384(this string inputString) => Hash(inputString, SHA384.Create());

        /// <summary>
        /// Computes SHA-512 hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <returns>The computed SHA-512 hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty array
        /// </exception>
        public static byte[] Sha512(this string inputString) => Hash(inputString, SHA512.Create());

        #endregion

        #region UTF-8 string lowercase alias

        /// <summary>
        /// Computes MD5 hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty array
        /// </exception>
        public static string Md5Hex(this string inputString) =>
            HashHex(inputString, MD5.Create(), false);

        /// <summary>
        /// Computes SHA-1 hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty array
        /// </exception>
        public static string Sha1Hex(this string inputString) =>
            HashHex(inputString, SHA1.Create(), false);

        /// <summary>
        /// Computes SHA-256 hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty array
        /// </exception>
        public static string Sha256Hex(this string inputString) =>
            HashHex(inputString, SHA256.Create(), false);

        /// <summary>
        /// Computes SHA-384 hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty array
        /// </exception>
        public static string Sha384Hex(this string inputString) =>
            HashHex(inputString, SHA384.Create(), false);

        /// <summary>
        /// Computes SHA-512 hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty array
        /// </exception>
        public static string Sha512Hex(this string inputString) =>
            HashHex(inputString, SHA512.Create(), false);

        #endregion

        #region UTF-8 string alias

        /// <summary>
        /// Computes MD5 hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="upperCaseOutput">If the output is to be uppercase</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty array
        /// </exception>
        public static string Md5Hex(this string inputString, bool upperCaseOutput) =>
            HashHex(inputString, MD5.Create(), upperCaseOutput);

        /// <summary>
        /// Computes SHA-1 hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="upperCaseOutput">If the output is to be uppercase</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty array
        /// </exception>
        public static string Sha1Hex(this string inputString, bool upperCaseOutput) =>
            HashHex(inputString, SHA1.Create(), upperCaseOutput);

        /// <summary>
        /// Computes SHA-256 hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="upperCaseOutput">If the output is to be uppercase</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty array
        /// </exception>
        public static string Sha256Hex(this string inputString, bool upperCaseOutput) =>
            HashHex(inputString, SHA256.Create(), upperCaseOutput);

        /// <summary>
        /// Computes SHA-256 hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="upperCaseOutput">If the output is to be uppercase</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty array
        /// </exception>
        public static string Sha384Hex(this string inputString, bool upperCaseOutput) =>
            HashHex(inputString, SHA384.Create(), upperCaseOutput);

        /// <summary>
        /// Computes SHA-256 hash of <see cref="String"/>
        /// </summary>
        /// <param name="inputString">The input <see cref="String"/></param>
        /// <param name="upperCaseOutput">If the output is to be uppercase</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="inputString"/> is null or empty array
        /// </exception>
        public static string Sha512Hex(this string inputString, bool upperCaseOutput) =>
            HashHex(inputString, SHA512.Create(), upperCaseOutput);

        #endregion
    }
}
