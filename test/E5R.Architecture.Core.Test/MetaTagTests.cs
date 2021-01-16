// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using System.Reflection;
using E5R.Architecture.Core;
using E5R.Architecture.Core.Extensions;
using E5R.Architecture.Core.Models;
using Xunit;
using static E5R.Architecture.Core.MetaTagAttribute;

[assembly:MetaTag(DescriptionKey, "My assembly tag")]
[module:MetaTag(DescriptionKey, "My module tag")]

namespace E5R.Architecture.Core.Test
{
    public class MetaTagTests
    {
        [Fact]
        public void Raises_Exception_When_Parameter_IsNull()
        {
            var ex1 = Assert.Throws<ArgumentNullException>(() => new MetaTagAttribute(null, null));
            var ex2 = Assert.Throws<ArgumentNullException>(() =>
                new MetaTagAttribute("not null", null));

            Assert.Equal("tagKey", ex1.ParamName);
            Assert.Equal("tagValue", ex2.ParamName);
        }

        [Fact]
        public void Exports_Constants()
        {
            Assert.Equal("Description", MetaTagAttribute.DescriptionKey);
            Assert.Equal("CustomId", MetaTagAttribute.CustomIdKey);
        }

        [Theory]
        [InlineData("Key1")]
        [InlineData("Key2")]
        [InlineData("Key3")]
        [InlineData("Key4")]
        [InlineData("Key5")]
        public void Store_TagKey(string key)
        {
            var tag = new MetaTagAttribute(key, "value");

            Assert.Equal(tag.TagKey, key);
        }

        [Theory]
        [InlineData("Value 1")]
        [InlineData("Value 2")]
        [InlineData("Value 3")]
        [InlineData("Value 4")]
        [InlineData("Value 5")]
        public void Store_TagValue(string value)
        {
            var tag = new MetaTagAttribute("key", value);

            Assert.Equal(value, tag.TagValue);
        }

        [Fact]
        public void ItIsPossibleTo_Retrieve_MetaTags_ByExtension()
        {
            var tags1 = MyEnum.FirstOption.GetMetaTags();
            var tags2 = MyEnum.SecondOption.GetMetaTags();
            var tags3 = MyEnum.ThirdOption.GetMetaTags();
            var tagsEmpty = MyEnum.EmptyOption.GetMetaTags();

            Assert.NotNull(tags1);
            Assert.NotNull(tags2);
            Assert.NotNull(tags3);
            Assert.NotNull(tagsEmpty);

            Assert.NotEmpty(tags1);
            Assert.NotEmpty(tags2);
            Assert.NotEmpty(tags3);

            Assert.Single(tags1);
            Assert.Single(tags2);
            Assert.Equal(2, tags3.Count());

            Assert.Empty(tagsEmpty);
        }

        [Fact]
        public void ItIsPossibleTo_Retrieve_MetaTagNames_ByExtension()
        {
            var tags1 = MyEnum.FirstOption.GetTags();
            var tags2 = MyEnum.SecondOption.GetTags();
            var tags3 = MyEnum.ThirdOption.GetTags();
            var tagsEmpty = MyEnum.EmptyOption.GetTags();

            Assert.NotNull(tags1);
            Assert.NotNull(tags2);
            Assert.NotNull(tags3);
            Assert.NotNull(tagsEmpty);

            Assert.NotEmpty(tags1);
            Assert.NotEmpty(tags2);
            Assert.NotEmpty(tags3);

            Assert.Single(tags1);
            Assert.Single(tags2);
            Assert.Equal(2, tags3.Count());

            Assert.Empty(tagsEmpty);
        }

        [Fact]
        public void ItIsPossibleTo_Retrieve_MetaTagValue_ByExtension()
        {
            var firstDescription = MyEnum.FirstOption.GetTag(DescriptionKey);
            var firstCustomId = MyEnum.FirstOption.GetTag(CustomIdKey);
            var firstMyCustom = MyEnum.FirstOption.GetTag("MyCustom");

            var secondDescription = MyEnum.SecondOption.GetTag(DescriptionKey);
            var secondCustomId = MyEnum.SecondOption.GetTag(CustomIdKey);
            var secondMyCustom = MyEnum.SecondOption.GetTag("MyCustom");

            var thirdDescription = MyEnum.ThirdOption.GetTag(DescriptionKey);
            var thirdCustomId = MyEnum.ThirdOption.GetTag(CustomIdKey);
            var thirdMyCustom = MyEnum.ThirdOption.GetTag("MyCustom");

            var emptyDescription = MyEnum.EmptyOption.GetTag(DescriptionKey);
            var emptyCustomId = MyEnum.EmptyOption.GetTag(CustomIdKey);
            var emptyMyCustom = MyEnum.EmptyOption.GetTag("MyCustom");

            Assert.NotEmpty(firstDescription);
            Assert.Null(firstCustomId);
            Assert.Null(firstMyCustom);
            Assert.Equal("My First Option", firstDescription);

            Assert.Null(secondDescription);
            Assert.NotEmpty(secondCustomId);
            Assert.Null(secondMyCustom);
            Assert.Equal("second-option", secondCustomId);

            Assert.NotEmpty(thirdDescription);
            Assert.NotEmpty(thirdCustomId);
            Assert.Null(thirdMyCustom);
            Assert.Equal(nameof(MyEnum.ThirdOption), thirdCustomId);

            Assert.Null(emptyDescription);
            Assert.Null(emptyCustomId);
            Assert.Null(emptyMyCustom);
        }

        [Fact]
        public void ItIs_PossibleToTag_Modules()
        {
            var module = typeof(MetaTagTests).Assembly.Modules.FirstOrDefault();
            var tags = module.GetMetaTags();
            var descriptionTag = module.GetTag(DescriptionKey);

            Assert.NotNull(tags);
            Assert.Single(tags);
            Assert.Equal("My module tag", descriptionTag);
        }

        [Fact]
        public void ItIs_PossibleToTag_Assemblies()
        {
            var assembly = typeof(MetaTagTests).Assembly;
            var tags = assembly.GetMetaTags();
            var descriptionTag = assembly.GetTag(DescriptionKey);

            Assert.NotNull(tags);
            Assert.Single(tags);
            Assert.Equal("My assembly tag", descriptionTag);
        }

        [Fact]
        public void ItIs_PossibleToTag_Types()
        {
            var enumType = typeof(MyEnum);
            var classType = typeof(MyClass);
            var tags1 = enumType.GetMetaTags();
            var tags2 = classType.GetMetaTags();
            var descriptionTag1 = enumType.GetTag(DescriptionKey);
            var descriptionTag2 = classType.GetTag(DescriptionKey);

            Assert.NotNull(tags1);
            Assert.NotNull(tags2);
            Assert.Single(tags1);
            Assert.Single(tags2);
            Assert.Equal("My enum tag", descriptionTag1);
            Assert.Equal("My class tag", descriptionTag2);
        }

        [Fact]
        public void ItIs_PossibleToTag_Fields()
        {
            var field = typeof(MyClass).GetField("_field",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var tags = field.GetMetaTags();
            var descriptionTag = field.GetTag(DescriptionKey);

            Assert.NotNull(tags);
            Assert.Single(tags);
            Assert.Equal("My field tag", descriptionTag);
        }

        [Fact]
        public void ItIs_PossibleToTag_Constructors()
        {
            var ctor = typeof(MyClass).GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new Type[0],
                null);
            var tags = ctor.GetMetaTags();
            var descriptionTag = ctor.GetTag(DescriptionKey);

            Assert.NotNull(tags);
            Assert.Single(tags);
            Assert.Equal("My constructor tag", descriptionTag);
        }

        [Fact]
        public void ItIs_PossibleToTag_Properties()
        {
            var prop = typeof(MyClass).GetProperty("MyProperty",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var tags = prop.GetMetaTags();
            var descriptionTag = prop.GetTag(DescriptionKey);

            Assert.NotNull(tags);
            Assert.Single(tags);
            Assert.Equal("My property tag", descriptionTag);
        }

        [Fact]
        public void ItIs_PossibleToTag_Methods()
        {
            var method = typeof(MyClass).GetMethod("MyMethod",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var tags = method.GetMetaTags();
            var descriptionTag = method.GetTag(DescriptionKey);

            Assert.NotNull(tags);
            Assert.Single(tags);
            Assert.Equal("My method tag", descriptionTag);
        }

        [Fact]
        public void ItIs_PossibleToTag_Events()
        {
            var myEvent = typeof(MyClass).GetEvent("MyEvent",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var tags = myEvent.GetMetaTags();
            var descriptionTag = myEvent.GetTag(DescriptionKey);

            Assert.NotNull(tags);
            Assert.Single(tags);
            Assert.Equal("My event tag", descriptionTag);
        }

        [Fact]
        public void ItIs_PossibleToTag_Interfaces()
        {
            var interfaceType = typeof(MyInterface);
            var interfaceMethod = interfaceType.GetMethod("Method");
            var tags1 = interfaceType.GetMetaTags();
            var tags2 = interfaceMethod.GetMetaTags();
            var descriptionTag1 = interfaceType.GetTag(DescriptionKey);
            var descriptionTag2 = interfaceMethod.GetTag(DescriptionKey);

            Assert.NotNull(tags1);
            Assert.NotNull(tags2);
            Assert.Single(tags1);
            Assert.Single(tags2);
            Assert.Equal("My interface tag", descriptionTag1);
            Assert.Equal("My interface method tag", descriptionTag2);
        }

        [Fact]
        public void ItIs_PossibleToTag_Delegates()
        {
            var delegateType = typeof(MyDelegate);
            var tags = delegateType.GetMetaTags();
            var descriptionTag = delegateType.GetTag(DescriptionKey);

            Assert.NotNull(tags);
            Assert.Single(tags);
            Assert.Equal("My delegate tag", descriptionTag);
        }

        [Fact]
        public void ItIs_PossibleToTag_Structs()
        {
            var structType = typeof(MyStruct);
            var field = structType.GetField("data", BindingFlags.Instance | BindingFlags.NonPublic);
            var method =
                structType.GetMethod("Method", BindingFlags.Instance | BindingFlags.NonPublic);

            var structTags = structType.GetMetaTags();
            var structDescription = structType.GetTag(DescriptionKey);
            var fieldTags = field.GetMetaTags();
            var fieldDescription = field.GetTag(DescriptionKey);
            var methodTags = method.GetMetaTags();
            var methodDescription = method.GetTag(DescriptionKey);

            Assert.NotNull(structTags);
            Assert.NotNull(fieldTags);
            Assert.NotNull(methodTags);
            Assert.Single(structTags);
            Assert.Single(fieldTags);
            Assert.Single(methodTags);
            Assert.Equal("My struct tag", structDescription);
            Assert.Equal("My struct field tag", fieldDescription);
            Assert.Equal("My struct method tag", methodDescription);
        }

        [Fact]
        public void EveryEnum_CanBeConvertedTo_An_EnumModel()
        {
            EnumModel firstModel = MyEnum.FirstOption;
            EnumModel secondModel = MyEnum.SecondOption;
            EnumModel thirdModel = MyEnum.ThirdOption;
            EnumModel emptyModel = MyEnum.EmptyOption;
            EnumModel outOfRangeModel = (MyEnum) (-1);

            Assert.NotNull(firstModel);
            Assert.NotNull(secondModel);
            Assert.NotNull(thirdModel);
            Assert.NotNull(emptyModel);
            Assert.NotNull(outOfRangeModel);

            Assert.Equal("0", firstModel.Id);
            Assert.Equal("My First Option", firstModel.Description);

            Assert.Equal("second-option", secondModel.Id);
            Assert.Equal(nameof(MyEnum.SecondOption), secondModel.Description);

            Assert.Equal(nameof(MyEnum.ThirdOption), thirdModel.Id);
            Assert.Equal("My Third Option", thirdModel.Description);

            Assert.Equal(((int) MyEnum.EmptyOption).ToString(), emptyModel.Id);
            Assert.Equal(nameof(MyEnum.EmptyOption), emptyModel.Description);

            Assert.Equal("-1", outOfRangeModel.Id);
            Assert.Null(outOfRangeModel.Description);
        }

        [Fact]
        public void An_EnumModel_WithId_CanBeConvertedTo_Any_Enum()
        {
            var firstModel = new EnumModel {Id = "0"};
            var secondModel = new EnumModel {Id = "second-option"};
            var thirdModel = new EnumModel {Id = nameof(MyEnum.ThirdOption)};
            var emptyModel = new EnumModel {Id = ((int) MyEnum.EmptyOption).ToString()};

            Assert.Equal(MyEnum.FirstOption, firstModel.ToEnum<MyEnum>());
            Assert.Equal(MyEnum.SecondOption, secondModel.ToEnum<MyEnum>());
            Assert.Equal(MyEnum.ThirdOption, thirdModel.ToEnum<MyEnum>());
            Assert.Equal(MyEnum.EmptyOption, emptyModel.ToEnum<MyEnum>());
        }

        [Fact]
        public void An_EnumModel_WithoutAnId_CanNotBeConvertedTo_An_Enum()
        {
            var exception =
                Assert.Throws<ArgumentNullException>(() => new EnumModel().ToEnum<MyEnum>());

            Assert.Equal(nameof(EnumModel.Id), exception.ParamName);
        }

        #region Fakes
        #pragma warning disable 67, 169

        [MetaTag(DescriptionKey, "My enum tag")]
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

        [MetaTag(DescriptionKey, "My class tag")]
        class MyClass
        {
            [MetaTag(DescriptionKey, "My field tag")]
            int _field;

            [MetaTag(DescriptionKey, "My constructor tag")]
            MyClass()
            {
            }

            [MetaTag(DescriptionKey, "My property tag")]
            string MyProperty { get; set; }

            [MetaTag(DescriptionKey, "My method tag")]
            void MyMethod([MetaTag(DescriptionKey, "My parameter tag")]
                int parameter)
            {
            }

            [MetaTag(DescriptionKey, "My event tag")]

            event MyDelegate MyEvent;

        }

        [MetaTag(DescriptionKey, "My interface tag")]
        interface MyInterface
        {
            [MetaTag(DescriptionKey, "My interface method tag")]
            void Method();
        }

        [MetaTag(DescriptionKey, "My delegate tag")]
        delegate void MyDelegate();

        [MetaTag(DescriptionKey, "My struct tag")]
        struct MyStruct
        {
            [MetaTag(DescriptionKey, "My struct field tag")]
            int data;

            [MetaTag(DescriptionKey, "My struct method tag")]
            void Method()
            {
            }
        }

        #pragma warning restore 67, 169
        #endregion
    }
}
