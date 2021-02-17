// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using System.Security.Cryptography;

namespace E5R.Architecture.Core.Extensions
{
    public static partial class ByteArrayExtensions
    {
        /// <summary>
        /// Computes hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <param name="algorithm">Hash algorithm</param>
        /// <returns>The computed hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty, or <paramref name="algorithm"/>
        /// is null
        /// </exception>
        public static byte[] Hash(this byte[] bytes, HashAlgorithm algorithm)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullArgument(algorithm, nameof(algorithm));

            return algorithm.ComputeHash(bytes);
        }

        /// <summary>
        /// Computes hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <param name="algorithm">Hash algorithm</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty, or <paramref name="algorithm"/>
        /// is null
        /// </exception>
        public static string HashHex(this byte[] bytes, HashAlgorithm algorithm) =>
            HashHex(bytes, algorithm, false);

        /// <summary>
        /// Computes hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <param name="algorithm">Hash algorithm</param>
        /// <param name="upperCaseOutput">If the output is to be uppercase</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty, or <paramref name="algorithm"/>
        /// is null
        /// </exception>
        public static string HashHex(this byte[] bytes, HashAlgorithm algorithm,
            bool upperCaseOutput)
        {
            Checker.NotNullOrEmptyArgument(bytes, nameof(bytes));
            Checker.NotNullArgument(algorithm, nameof(algorithm));

            return string.Concat(algorithm.ComputeHash(bytes)
                .Select(c => c.ToString(upperCaseOutput ? "X2" : "x2")));
        }

        #region Byte alias

        /// <summary>
        /// Computes MD5 hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns>The computed MD5 hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty
        /// </exception>
        public static byte[] Md5(this byte[] bytes) => Hash(bytes, MD5.Create());

        /// <summary>
        /// Computes SHA-1 hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns>The computed SHA-1 hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty
        /// </exception>
        public static byte[] Sha1(this byte[] bytes) => Hash(bytes, SHA1.Create());

        /// <summary>
        /// Computes SHA-256 hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns>The computed SHA-256 hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty
        /// </exception>
        public static byte[] Sha256(this byte[] bytes) => Hash(bytes, SHA256.Create());

        /// <summary>
        /// Computes SHA-384 hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns>The computed SHA-384 hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty
        /// </exception>
        public static byte[] Sha384(this byte[] bytes) => Hash(bytes, SHA384.Create());

        /// <summary>
        /// Computes SHA-512 hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns>The computed SHA-512 hash code</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty
        /// </exception>
        public static byte[] Sha512(this byte[] bytes) => Hash(bytes, SHA512.Create());

        #endregion

        #region String lowercase alias

        /// <summary>
        /// Computes MD5 hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty
        /// </exception>
        public static string Md5Hex(this byte[] bytes) => HashHex(bytes, MD5.Create(), false);
        
        /// <summary>
        /// Computes SHA-1 hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty
        /// </exception>
        public static string Sha1Hex(this byte[] bytes) => HashHex(bytes, SHA1.Create(), false);

        /// <summary>
        /// Computes SHA-256 hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty
        /// </exception>
        public static string Sha256Hex(this byte[] bytes) => HashHex(bytes, SHA256.Create(), false);

        /// <summary>
        /// Computes SHA-384 hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty
        /// </exception>
        public static string Sha384Hex(this byte[] bytes) => HashHex(bytes, SHA384.Create(), false);

        /// <summary>
        /// Compute SHA-512 hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty
        /// </exception>
        public static string Sha512Hex(this byte[] bytes) => HashHex(bytes, SHA512.Create(), false);

        #endregion

        #region String alias
        
        /// <summary>
        /// Computes MD5 hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <param name="upperCaseOutput">Generate uppercase output</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty
        /// </exception>
        public static string Md5Hex(this byte[] bytes, bool upperCaseOutput) =>
            HashHex(bytes, MD5.Create(), upperCaseOutput);

        /// <summary>
        /// Computes SHA-1 hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <param name="upperCaseOutput">Generate uppercase output</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty
        /// </exception>
        public static string Sha1Hex(this byte[] bytes, bool upperCaseOutput) =>
            HashHex(bytes, SHA1.Create(), upperCaseOutput);

        /// <summary>
        /// Computes SHA-256 hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <param name="upperCaseOutput">Generate uppercase output</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty
        /// </exception>
        public static string Sha256Hex(this byte[] bytes, bool upperCaseOutput) =>
            HashHex(bytes, SHA256.Create(), upperCaseOutput);

        /// <summary>
        /// Computes SHA-384 hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <param name="upperCaseOutput">Generate uppercase output</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty
        /// </exception>
        public static string Sha384Hex(this byte[] bytes, bool upperCaseOutput) =>
            HashHex(bytes, SHA384.Create(), upperCaseOutput);

        /// <summary>
        /// Compute SHA-512 hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <param name="upperCaseOutput">Generate uppercase output</param>
        /// <returns>
        /// Hexadecimal <see cref="String"/> representation of the computed hash code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="bytes"/> is null or empty
        /// </exception>
        public static string Sha512Hex(this byte[] bytes, bool upperCaseOutput) =>
            HashHex(bytes, SHA512.Create(), upperCaseOutput);

        #endregion
    }
}
