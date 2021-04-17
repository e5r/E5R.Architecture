// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using E5R.Architecture.Core.Extensions;
using Moq;
using Xunit;

namespace E5R.Architecture.Core.Test
{
    public class ByteArrayExtensionsTests
    {
        #region Hash extensions

        [Fact]
        public void HashRequires_AllArguments()
        {
            var hashMock = new Mock<HashAlgorithm>();
            var ex1 = Assert.Throws<ArgumentNullException>(() =>
                ByteArrayExtensions.Hash(null, hashMock.Object));
            var ex2 = Assert.Throws<ArgumentNullException>(() => new byte[0].Hash(hashMock.Object));
            var ex3 = Assert.Throws<ArgumentNullException>(() => new byte[1].Hash(null));

            Assert.Equal("bytes", ex1.ParamName);
            Assert.Equal("bytes", ex2.ParamName);
            Assert.Equal("algorithm", ex3.ParamName);
        }

        [Theory]
        [InlineData(nameof(MD5))]
        [InlineData(nameof(SHA1))]
        [InlineData(nameof(SHA256))]
        [InlineData(nameof(SHA384))]
        [InlineData(nameof(SHA512))]
        [InlineData(nameof(HMACMD5))]
        [InlineData(nameof(HMACSHA1))]
        [InlineData(nameof(HMACSHA256))]
        [InlineData(nameof(HMACSHA384))]
        [InlineData(nameof(HMACSHA512))]
        public void HashCompute_AllAlgorithms(string algorithmName)
        {
            var inputBuffer = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            var algorithm = HashAlgorithm.Create(algorithmName);
            var expectedOutput = algorithm.ComputeHash(inputBuffer);
            var output = inputBuffer.Hash(algorithm);

            Assert.Equal(expectedOutput, output);
        }

        [Theory]
        [InlineData(nameof(MD5), "69962bc348eccebd2d2b4718b96f0846")]
        [InlineData(nameof(SHA1), "ef05d63454abefbdb7adb6781b4e8651950fb6d4")]
        [InlineData(nameof(SHA256),
            "80d4572c6902e3cb31f624162150b43a4a359839fa0f238fe1bcdd4f176b04f4")]
        [InlineData(nameof(SHA384),
            "f100e18b260e9336fbc589d4403078d766a6a7647b76b1d0b870426ab2b8dca2d9b42ba32eacfd518bd2cc44abae65f7")]
        [InlineData(nameof(SHA512),
            "b64ae429aba540932dbd14252819c77f881923065874a7275147cb6f21e31cb96103d21de32f68793c2db81dd1a4c29b27e8e01826a1b02f44e1c32a3bb874e4")]
        public void HashCompute_KnownUTF8ValuesFromE5RWord(string algorithmName,
            string expectedValue)
        {
            var inputE5RBytes = Encoding.UTF8.GetBytes("E5R");

            var algorithm = HashAlgorithm.Create(algorithmName);
            var output = inputE5RBytes.Hash(algorithm);
            var outputString = string.Concat(output.Select(c => c.ToString("x2")));

            Assert.Equal(expectedValue, outputString);
        }

        #endregion

        #region HMAC extensions

        [Fact]
        public void HmacRequires_AllArguments()
        {
            var hmacMock = new Mock<HMAC>();
            var ex1 = Assert.Throws<ArgumentNullException>(() =>
                ByteArrayExtensions.Hmac(null, hmacMock.Object));
            var ex2 = Assert.Throws<ArgumentNullException>(() => new byte[0].Hmac(hmacMock.Object));
            var ex3 = Assert.Throws<ArgumentNullException>(() => new byte[1].Hmac(null));

            Assert.Equal("bytes", ex1.ParamName);
            Assert.Equal("bytes", ex2.ParamName);
            Assert.Equal("hmac", ex3.ParamName);
        }

        [Theory]
        [InlineData(nameof(HMACMD5))]
        [InlineData(nameof(HMACSHA1))]
        [InlineData(nameof(HMACSHA256))]
        [InlineData(nameof(HMACSHA384))]
        [InlineData(nameof(HMACSHA512))]
        public void HmacCompute_AllAlgorithms(string algorithmName)
        {
            var inputBuffer = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            var hmac = HMAC.Create(algorithmName);
            var expectedOutput = hmac.ComputeHash(inputBuffer);
            var output = inputBuffer.Hmac(hmac);

            Assert.Equal(expectedOutput, output);
        }

        [Theory]
        [InlineData(nameof(HMACMD5), "eb435285b9f681c22269ade8622a8016")]
        [InlineData(nameof(HMACSHA1), "8af82660b6c1f6059048ad7a16ff6343c2bfaa1d")]
        [InlineData(nameof(HMACSHA256),
            "a0d64f7c16be101bfae860cc4b5d23cdd7a4590f8206da058e82f63129ec742c")]
        [InlineData(nameof(HMACSHA384),
            "91a737284ae15d657dc9a65ec2f92f2bf1a09f9abe3033728c100b9082b9c1d07b15c5ed588cd24d0ca43c7d0b944cfe")]
        [InlineData(nameof(HMACSHA512),
            "887142ae4874ff3b54dfa0d8e76721baf0e81db9b4e5b1522f88d80c081134b890b07931c7d98fb6bdf5b99f624bd806c8c1ab0bc988935e18822d2f631e88da")]
        public void HmacCompute_KnownUTF8ValuesFromE5RWordAndKey(string algorithmName,
            string expectedValue)
        {
            var inputKey = new byte[] {5, 5, 18};
            var inputE5RBytes = Encoding.UTF8.GetBytes("E5R");
            var algorithm = HMAC.Create(algorithmName);

            algorithm.Key = inputKey;

            var output = inputE5RBytes.Hash(algorithm);
            var outputString = string.Concat(output.Select(c => c.ToString("x2")));

            Assert.Equal(expectedValue, outputString);
        }

        #endregion

        #region String extensions

        [Fact]
        public void IntoString_RequiresAllArguments()
        {
            var ex1 = Assert.Throws<ArgumentNullException>(() =>
                ByteArrayExtensions.IntoString(null, Encoding.Default));
            var ex2 =
                Assert.Throws<ArgumentNullException>(() => new byte[1].IntoString(null));

            Assert.Equal("bytes", ex1.ParamName);
            Assert.Equal("encoding", ex2.ParamName);
        }

        [Fact]
        public void IntoString_ConvertToCorrectEncodedString()
        {
            var phrase = "E5R Development Team";
            var buffer1 = Encoding.UTF8.GetBytes(phrase);
            var buffer2 = Encoding.BigEndianUnicode.GetBytes(phrase);

            var convertedPhrase1 = buffer1.IntoString(Encoding.UTF8);
            var convertedPhrase2 = buffer2.IntoString(Encoding.BigEndianUnicode);
            var convertedPhrase3 = buffer1.IntoString(Encoding.BigEndianUnicode);

            Assert.Equal(phrase, convertedPhrase1);
            Assert.Equal(phrase, convertedPhrase2);
            Assert.NotEqual(phrase, convertedPhrase3);
        }

        [Fact]
        public void IntoString_ConvertToDefaultUTF8String()
        {
            var phrase = "E5R Development Team";
            var buffer1 = Encoding.UTF8.GetBytes(phrase);
            var buffer2 = Encoding.BigEndianUnicode.GetBytes(phrase);

            var convertedPhrase1 = buffer1.IntoString();
            var convertedPhrase2 = buffer2.IntoString();

            Assert.Equal(phrase, convertedPhrase1);
            Assert.NotEqual(phrase, convertedPhrase2);
        }

        #endregion
    }
}
