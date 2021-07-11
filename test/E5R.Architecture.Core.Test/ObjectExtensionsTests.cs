// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using E5R.Architecture.Core.Extensions;
using Xunit;

namespace E5R.Architecture.Core.Test
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void Fill_Raises_Exception_When_ParameterIsNull()
        {
            var ex1 = Assert.Throws<ArgumentNullException>(() =>
                ObjectExtensions.Fill<object>(null, null));
            var ex2 = Assert.Throws<ArgumentNullException>(() => new { }.Fill(null));

            Assert.Equal("object", ex1.ParamName);
            Assert.Equal("dictionary", ex2.ParamName);
        }
        
        [Fact]
        public void AllProperties_AreFilled()
        {
            var obj = new MyNamingConventionActivatable().Fill(new Dictionary<string, string>
            {
                {"my", "My"},
                {"my_First", "MyFirst"},
                {"my_first_property", "MyFirstProperty"},
                {"my__First_Property__naming", "MyFirstPropertyNaming"},
                {"myFirstPropertyNamingConvention", "MyFirstPropertyNamingConvention"},
            });
            
            Assert.Equal("My", obj.My);
            Assert.Equal("MyFirst", obj.MyFirst);
            Assert.Equal("MyFirstProperty", obj.MyFirstProperty);
            Assert.Equal("MyFirstPropertyNaming", obj.MyFirstPropertyNaming);
            Assert.Equal("MyFirstPropertyNamingConvention", obj.MyFirstPropertyNamingConvention);
        }

        private class MyNamingConventionActivatable
        {
            public string My { get; set; }
            public string MyFirst { get; set; }
            public string MyFirstProperty { get; set; }
            public string MyFirstPropertyNaming { get; set; }
            public string MyFirstPropertyNamingConvention { get; set; }
        }
    }
}
