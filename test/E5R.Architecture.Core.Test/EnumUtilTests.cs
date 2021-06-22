// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using E5R.Architecture.Core.Utils;
using Xunit;
using static E5R.Architecture.Core.MetaTagAttribute;

namespace E5R.Architecture.Core.Test
{
    public class EnumUtilTests
    {
        [Fact]
        public void GetValues_CorrespondsTo_TheValuesOfThe_Enum()
        {
            var values = EnumUtil.GetValues<MyEnum>();

            Assert.NotNull(values);
            Assert.NotEmpty(values);
            Assert.Equal(4, values.Length);

            Assert.Contains(values, w => w == MyEnum.FirstOption);
            Assert.Contains(values, w => w == MyEnum.SecondOption);
            Assert.Contains(values, w => w == MyEnum.ThirdOption);
            Assert.Contains(values, w => w == MyEnum.EmptyOption);
        }

        [Fact]
        public void GetModels_CorrespondsTo_TheValuesOfThe_Enum()
        {
            var values = EnumUtil.GetModels<MyEnum>();

            Assert.NotNull(values);
            Assert.NotEmpty(values);
            Assert.Equal(4, values.Length);

            Assert.Contains(values, w => w.Id == "0" && w.Description == "My First Option");
            Assert.Contains(values,
                w => w.Id == "second-option" && w.Description == nameof(MyEnum.SecondOption));
            Assert.Contains(values,
                w => w.Id == nameof(MyEnum.ThirdOption) && w.Description == "My Third Option");
            Assert.Contains(values,
                w => w.Id == ((int) MyEnum.EmptyOption).ToString() &&
                     w.Description == nameof(MyEnum.EmptyOption));
        }

        [Fact]
        public void Parse_FromValidMetatag()
        {
            var firstOption = EnumUtil.FromTag<MyEnum>(DescriptionKey, "My First Option");
            var thirdOption1 = EnumUtil.FromTag<MyEnum>(DescriptionKey, "My Third Option");
            var secondOption = EnumUtil.FromTag<MyEnum>(CustomIdKey, "second-option");
            var thirdOption2 = EnumUtil.FromTag<MyEnum>(CustomIdKey, nameof(MyEnum.ThirdOption));

            Assert.Equal(MyEnum.FirstOption, firstOption);
            Assert.Equal(MyEnum.SecondOption, secondOption);
            Assert.Equal(MyEnum.ThirdOption, thirdOption1);
            Assert.Equal(MyEnum.ThirdOption, thirdOption2);
        }
        
        [Fact]
        public void TryParse_FromValidMetatag()
        {
            var firstOptionResult = EnumUtil.TryFromTag<MyEnum>(DescriptionKey, "My First Option",
                out MyEnum firstOption);
            var thirdOption1Result = EnumUtil.TryFromTag<MyEnum>(DescriptionKey, "My Third Option",
                out MyEnum thirdOption1);
            var secondOptionResult =
                EnumUtil.TryFromTag<MyEnum>(CustomIdKey, "second-option", out MyEnum secondOption);
            var thirdOption2Result = EnumUtil.TryFromTag<MyEnum>(CustomIdKey,
                nameof(MyEnum.ThirdOption), out MyEnum thirdOption2);

            Assert.True(firstOptionResult);
            Assert.Equal(MyEnum.FirstOption, firstOption);
            Assert.True( secondOptionResult);
            Assert.Equal(MyEnum.SecondOption, secondOption);
            Assert.True(thirdOption1Result);
            Assert.Equal(MyEnum.ThirdOption, thirdOption1);
            Assert.True(thirdOption2Result);
            Assert.Equal(MyEnum.ThirdOption, thirdOption2);
        }

        [Fact]
        public void RaiseException_FromInvalidMetatag()
        {
            var ex1 = Assert.Throws<ArgumentNullException>(() =>
                EnumUtil.FromTag<MyEnum>(null, "invalid"));

            var ex2 = Assert.Throws<ArgumentNullException>(() =>
                EnumUtil.FromTag<MyEnum>("    ", "invalid"));

            var ex3 = Assert.Throws<ArgumentNullException>(() =>
                EnumUtil.FromTag<MyEnum>(CustomIdKey, null));

            var ex4 = Assert.Throws<ArgumentNullException>(() =>
                EnumUtil.FromTag<MyEnum>(CustomIdKey, "    "));

            var ex5 = Assert.Throws<InvalidCastException>(() =>
                EnumUtil.FromTag<MyEnum>(CustomIdKey, "invalid"));

            Assert.Equal("tagKey", ex1.ParamName);
            Assert.Equal("tagKey", ex2.ParamName);
            Assert.Equal("tagValue", ex3.ParamName);
            Assert.Equal("tagValue", ex4.ParamName);
            Assert.Equal(
                "The Enum could not be converted because there is no value signed with the informed tag",
                ex5.Message);
        }
        
        [Fact]
        public void TryParse_FromInvalidMetatag()
        {
            var result1 = EnumUtil.TryFromTag<MyEnum>(null, "invalid", out MyEnum enum1);
            var result2 = EnumUtil.TryFromTag<MyEnum>("    ", "invalid", out MyEnum enum2);
            var result3 = EnumUtil.TryFromTag<MyEnum>(CustomIdKey, null, out MyEnum enum3);
            var result4 = EnumUtil.TryFromTag<MyEnum>(CustomIdKey, "    ", out MyEnum enum4);
            var result5 = EnumUtil.TryFromTag<MyEnum>(CustomIdKey, "invalid", out MyEnum enum5);

            Assert.False(result1);
            Assert.False(result2);
            Assert.False(result3);
            Assert.False(result4);
            Assert.False(result5);
            Assert.Equal(default, enum1);
            Assert.Equal(default, enum2);
            Assert.Equal(default, enum3);
            Assert.Equal(default, enum4);
            Assert.Equal(default, enum5);
        }

        #region Fakes

#pragma warning disable 67, 169

        enum MyEnum
        {
            [MetaTag(DescriptionKey, "My First Option")]
            FirstOption,

            [MetaTag(CustomIdKey, "second-option")]
            SecondOption,

            [MetaTag(DescriptionKey, "My Third Option")] [MetaTag(CustomIdKey, nameof(ThirdOption))]
            ThirdOption,

            EmptyOption
        }

#pragma warning restore 67, 169

        #endregion
    }
}
