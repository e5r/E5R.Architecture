// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using System.Reflection;
using E5R.Architecture.Core;
using E5R.Architecture.Core.Extensions;
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
            var ex2 = Assert.Throws<ArgumentNullException>(() => new MetaTagAttribute("not null", null));
            
            Assert.Equal("tagKey", ex1.ParamName);
            Assert.Equal("tagValue", ex2.ParamName);
        }

        [Fact]
        public void Exports_Constants()
        {
            Assert.Equal("Description", MetaTagAttribute.DescriptionKey);
            Assert.Equal("CustomValue", MetaTagAttribute.CustomValueKey);
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
            var firstCustomValue = MyEnum.FirstOption.GetTag(CustomValueKey);
            var firstMyCustom = MyEnum.FirstOption.GetTag("MyCustom");
            
            var secondDescription = MyEnum.SecondOption.GetTag(DescriptionKey);
            var secondCustomValue = MyEnum.SecondOption.GetTag(CustomValueKey);
            var secondMyCustom = MyEnum.SecondOption.GetTag("MyCustom");
            
            var thirdDescription = MyEnum.ThirdOption.GetTag(DescriptionKey);
            var thirdCustomValue = MyEnum.ThirdOption.GetTag(CustomValueKey);
            var thirdMyCustom = MyEnum.ThirdOption.GetTag("MyCustom");
            
            var emptyDescription = MyEnum.EmptyOption.GetTag(DescriptionKey);
            var emptyCustomValue = MyEnum.EmptyOption.GetTag(CustomValueKey);
            var emptyMyCustom = MyEnum.EmptyOption.GetTag("MyCustom");
            
            Assert.NotEmpty(firstDescription);
            Assert.Null(firstCustomValue);
            Assert.Null(firstMyCustom);
            Assert.Equal("My First Option", firstDescription);
            
            Assert.Null(secondDescription);
            Assert.NotEmpty(secondCustomValue);
            Assert.Null(secondMyCustom);
            Assert.Equal("second-option", secondCustomValue);
            
            Assert.NotEmpty(thirdDescription);
            Assert.NotEmpty(thirdCustomValue);
            Assert.Null(thirdMyCustom);
            Assert.Equal(nameof(MyEnum.ThirdOption), thirdCustomValue);
            
            Assert.Null(emptyDescription);
            Assert.Null(emptyCustomValue);
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
            var field = structType.GetField("data", BindingFlags.Instance|BindingFlags.NonPublic);
            var method = structType.GetMethod("Method", BindingFlags.Instance|BindingFlags.NonPublic);
            
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
    }
    
    #region Fakes

    [MetaTag(DescriptionKey, "My enum tag")]
    enum MyEnum
    {
        [MetaTag(DescriptionKey, "My First Option")]
        FirstOption,
        
        [MetaTag(CustomValueKey, "second-option")]
        SecondOption,
        
        [MetaTag(DescriptionKey, "My Third Option")]
        [MetaTag(CustomValueKey, nameof(ThirdOption))]
        ThirdOption,
        
        EmptyOption
    }

    [MetaTag(DescriptionKey, "My class tag")]
    class MyClass
    {
        [MetaTag(DescriptionKey, "My field tag")]
        int _field;
        
        [MetaTag(DescriptionKey, "My constructor tag")]
        MyClass() { }

        [MetaTag(DescriptionKey, "My property tag")]
        string MyProperty { get; set; }
        
        [MetaTag(DescriptionKey, "My method tag")]
        void MyMethod([MetaTag(DescriptionKey, "My parameter tag")] int parameter){}

        [MetaTag(DescriptionKey, "My event tag")]
        private event MyDelegate MyEvent;
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
        void Method() { }
    }

    #endregion
}
