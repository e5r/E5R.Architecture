// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Xunit;

namespace E5R.Architecture.Core.Test
{
    public class UniqueIdTests
    {
        [Fact]
        public void Generate_NewId_With64Chars_OnInstantiate()
        {
            var uid1 = new UniqueId();
            var uid2 = new UniqueId();

            Assert.NotNull(uid1);
            Assert.NotNull(uid2);
            Assert.NotEqual(uid1, uid2);
            Assert.NotEqual(uid1.ToString(), uid2.ToString());
            Assert.Equal(64, uid1.ToString().Length);
            Assert.Equal(64, uid2.ToString().Length);
        }

        [Fact]
        public void AllowsYouTo_ChooseThe_LengthOf_TheId()
        {
            var uid40 = new UniqueId(UniqueIdLength.Length40);
            var uid64 = new UniqueId(UniqueIdLength.Length64);
            var uid96 = new UniqueId(UniqueIdLength.Length96);
            var uid128 = new UniqueId(UniqueIdLength.Length128);
            
            Assert.NotNull(uid40);
            Assert.Equal(40, uid40.ToString().Length);
            
            Assert.NotNull(uid64);
            Assert.Equal(64, uid64.ToString().Length);
            
            Assert.NotNull(uid96);
            Assert.Equal(96, uid96.ToString().Length);
            
            Assert.NotNull(uid128);
            Assert.Equal(128, uid128.ToString().Length);
        }

        [Fact]
        public void Requires_AValidLength()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new UniqueId((UniqueIdLength)(-1));
            });
            
            Assert.Equal("length", ex.ParamName);
        }
        
        [Fact]
        public void Requires_ANonEmptyIdString()
        {
            var ex1 = Assert.Throws<ArgumentNullException>(() =>
            {
                new UniqueId(null);
            });
            
            var ex2 = Assert.Throws<ArgumentNullException>(() =>
            {
                new UniqueId(string.Empty);
            });
            
            var ex3 = Assert.Throws<ArgumentNullException>(() =>
            {
                new UniqueId("         ");
            });
            
            Assert.Equal("stringId", ex1.ParamName);
            Assert.Equal("stringId", ex2.ParamName);
            Assert.Equal("stringId", ex3.ParamName);
        }
        
        [Fact]
        public void AValidId_HasPredefined_Sizes()
        {
            var ex1 = Assert.Throws<InvalidCastException>(() =>
            {
                // Length: 39
                new UniqueId("010101010101010101010101010101010101010");
            });
            
            var ex2 = Assert.Throws<InvalidCastException>(() =>
            {
                // Length: 129
                new UniqueId("010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010");
            });

            var ok40 = new UniqueId("0000000000000000000000000000000000000000");
            var ok64 = new UniqueId("0000000000000000000000000000000000000000111111111111111111111111");
            var ok96 = new UniqueId("000000000000000000000000000000000000000011111111111111110000000000000000000000000000000000000000");
            var ok128 = new UniqueId("00000000000000000000000000000000000000001111111111111111111111111111111111111111000000000000000000000000000000000000000011111111");
            
            Assert.Equal("The string could not be converted to a valid UniqueId", ex1.Message);
            Assert.Equal("The string could not be converted to a valid UniqueId", ex2.Message);
            Assert.NotNull(ok40);
            Assert.NotNull(ok64);
            Assert.NotNull(ok96);
            Assert.NotNull(ok128);
        }
        
        [Fact]
        public void Requires_AHexadecimalIdString()
        {
            var ex = Assert.Throws<InvalidCastException>(() =>
            {
                new UniqueId("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa-");
            });
            
            Assert.Equal("The string could not be converted to a valid UniqueId", ex.Message);
        }
        
        public static IEnumerable<object[]> ValidHexadecimalChars()
        {
            foreach (var c in new[]
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'a', 'b', 'c', 'd', 'e', 'f',
                'A', 'B', 'C', 'D', 'E', 'F'
            })
            {
                yield return new object[] {c};
            }
        }
        
        [Theory]
        [MemberData(nameof(ValidHexadecimalChars))]
        public void Accept_AHexadecimalIdString(char c)
        {
            var ok40 = string.Concat(Enumerable.Repeat(c, 40));
            var ok64 = string.Concat(Enumerable.Repeat(c, 64));
            var ok96 = string.Concat(Enumerable.Repeat(c, 96));
            var ok128 = string.Concat(Enumerable.Repeat(c, 128));

            Assert.NotNull(ok40);
            Assert.NotNull(ok64);
            Assert.NotNull(ok96);
            Assert.NotNull(ok128);
        }

        [Fact]
        public void ConvertsImplicitly_To_String()
        {
            UniqueId uidNull = null;
            var uid = new UniqueId();
            var uidString1 = (string) uid;
            var uidStringNull = (string) uidNull;
            
            string uidString2 = uid;
            string uidString3 = new UniqueId();

            Assert.NotNull(uid);
            Assert.Null(uidStringNull);
            Assert.NotEmpty(uidString1);
            Assert.NotEmpty(uidString2);
            Assert.NotEmpty(uidString3);
            
            Assert.Equal(uid.ToString(), uidString1);
            Assert.Equal(uid.ToString(), uidString2);
        }
        
        [Fact]
        public void ConvertsImplicitly_From_String()
        {
            var uid1 = (UniqueId)"0000000000111111111100000000001111111111";
            UniqueId uid2 = "0000000000111111111100000000001111111111";
            var uidNull = (UniqueId) (string) null;
            
            string uidString1 = uid1;
            var uidString2 = (string)uid1;
            string uidString3 = new UniqueId();

            Assert.NotNull(uid1);
            Assert.NotNull(uid2);
            Assert.Null(uidNull);
            Assert.NotEmpty(uidString1);
            Assert.NotEmpty(uidString2);
            Assert.NotEmpty(uidString3);
            
            Assert.Equal(uid1.ToString(), uidString1);
            Assert.Equal(uid1.ToString(), uidString2);
            Assert.Equal("0000000000111111111100000000001111111111", uid1.ToString());
            Assert.Equal("0000000000111111111100000000001111111111", uid2.ToString());
            Assert.Equal("0000000000111111111100000000001111111111", uidString1);
            Assert.Equal("0000000000111111111100000000001111111111", uidString2);
        }
    }
}
