// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

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
            Assert.Contains(values, w => w.Id == "second-option" && w.Description == nameof(MyEnum.SecondOption));
            Assert.Contains(values, w => w.Id == nameof(MyEnum.ThirdOption) && w.Description == "My Third Option");
            Assert.Contains(values, w => w.Id == ((int)MyEnum.EmptyOption).ToString() && w.Description == nameof(MyEnum.EmptyOption));
        }

        #region Fakes
        #pragma warning disable 67, 169
        
        enum MyEnum
        {
            [MetaTag(DescriptionKey, "My First Option")]
            FirstOption,

            [MetaTag(CustomIdKey, "second-option")]
            SecondOption,

            [MetaTag(DescriptionKey, "My Third Option")]
            [MetaTag(CustomIdKey, nameof(ThirdOption))]
            ThirdOption,

            EmptyOption
        }
        
        #pragma warning restore 67, 169
        #endregion
    }
}
