// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Globalization;
using E5R.Architecture.Core.Extensions;
using Xunit;

namespace E5R.Architecture.Core.Test
{
    public class DictionaryExtensionsTests
    {
        [Fact]
        public void FillObject_Raises_Exception_When_ParameterIsNull()
        {
            var ex1 = Assert.Throws<ArgumentNullException>(() =>
                DictionaryExtensions.FillObject(null, (object) null));
            var ex2 = Assert.Throws<ArgumentNullException>(() =>
                new Dictionary<string, string>().FillObject((object) null));

            Assert.Equal("dictionary", ex1.ParamName);
            Assert.Equal("objectInstance", ex2.ParamName);
        }

        [Fact]
        public void Activate_Raises_Exception_When_ParameterIsNull()
        {
            var ex1 = Assert.Throws<ArgumentNullException>(() =>
                DictionaryExtensions.Activate<MyActivatable>(null));
            var ex2 = Assert.Throws<ArgumentNullException>(() =>
                ((Dictionary<string, string>) null).Activate<MyActivatable>());

            Assert.Equal("dictionary", ex1.ParamName);
            Assert.Equal("dictionary", ex2.ParamName);
        }

        [Fact]
        public void StringAndCharProperties_AreFilled()
        {
            var obj = new Dictionary<string, string>
            {
                {"Prop1", "Dictionary value"},
                {"Prop2", ""},
                {"Prop4", "C"},
                {"Prop5", "\u03a0"},
                {"Prop6", "\u03a3"},
            }.FillObject(new MyStringActivatable());

            char piChar = '\u03a0';
            char sigmaChar = '\u03a3';

            Assert.Equal("Dictionary value", obj.Prop1);
            Assert.Equal("", obj.Prop2);
            Assert.Null(obj.Prop3);
            Assert.Equal('C', obj.Prop4);
            Assert.Equal(piChar, obj.Prop5);
            Assert.Equal(sigmaChar, obj.Prop6);
        }
        
        [Fact]
        public void IntegerProperties_AreFilled()
        {
            var obj1 = new Dictionary<string, string>
            {
                {"Prop0", "000"},
                {"Prop2", ""},
                {"Prop3", "0"},
                {"Prop4", "1"},
                {"Prop5", "-1"},
                {"Prop6", "+1"},
                {"Prop7", "+999"},
            }.FillObject(new MyIntegerActivatable());
            
            var obj2 = new Dictionary<string, string>
            {
                {"UProp0", byte.MaxValue.ToString()},
                {"UProp1", ushort.MaxValue.ToString()},
                {"UProp2", uint.MaxValue.ToString()},
                {"UProp3", ulong.MaxValue.ToString()},
                {"UProp4", uint.MaxValue.ToString()},
            }.FillObject(new MyIntegerActivatable());
        
            Assert.Equal(0, obj1.Prop0);
            Assert.Equal(0, obj1.Prop1);
            Assert.Equal(0, obj1.Prop2);
            Assert.Equal(0, obj1.Prop3);
            Assert.Equal(1, obj1.Prop4);
            Assert.Equal(-1, obj1.Prop5);
            Assert.Equal(1, obj1.Prop6);
            Assert.Equal((IntPtr)999, obj1.Prop7);
            
            Assert.Equal(byte.MaxValue, obj2.UProp0);
            Assert.Equal(ushort.MaxValue, obj2.UProp1);
            Assert.Equal(uint.MaxValue, obj2.UProp2);
            Assert.Equal(ulong.MaxValue, obj2.UProp3);
            Assert.Equal((UIntPtr)uint.MaxValue, obj2.UProp4);
        }
        
        [Fact]
        public void BooleanProperties_AreFilled()
        {
            var obj = new Dictionary<string, string>
            {
                {"PropTrue1", "True"},
                {"PropTrue2", "true"},
                {"PropTrue3", "TRUE"},
                {"PropTrue4", "-1"},
                {"PropTrue5", "1"},
                {"PropFalse1", "False"},
                {"PropFalse2", "false"},
                {"PropFalse3", "FALSE"},
                {"PropFalse4", "0"},
            }.FillObject(new MyBooleanActivatable());
        
            Assert.True(obj.PropTrue1);
            Assert.True(obj.PropTrue2);
            Assert.True(obj.PropTrue3);
            Assert.True(obj.PropTrue4);
            Assert.True(obj.PropTrue5);
            Assert.False(obj.PropFalse1);
            Assert.False(obj.PropFalse2);
            Assert.False(obj.PropFalse3);
            Assert.False(obj.PropFalse4);
        }
        
        [Fact]
        public void FloatProperties_AreFilled()
        {
            var obj = new Dictionary<string, string>
            {
                {"Float1", ""},
                {"Float2", "0"},
                {"Float3", "0.0"},
                {"Float4", "1e-35"},
                {"Float5", float.MinValue.ToString(CultureInfo.InvariantCulture)},
                {"Float6", float.MaxValue.ToString(CultureInfo.InvariantCulture)},
                {"Double1", "2.2"},
                {"Double2", double.MinValue.ToString(CultureInfo.InvariantCulture)},
                {"Double3", double.MaxValue.ToString(CultureInfo.InvariantCulture)},
                {"Decimal1", "3.3"},
                {"Decimal2", decimal.MinValue.ToString(CultureInfo.InvariantCulture)},
                {"Decimal3", decimal.MaxValue.ToString(CultureInfo.InvariantCulture)},
            }.FillObject(new MyFloatActivatable());
        
            Assert.Equal(0f, obj.Float1);
            Assert.Equal(0f, obj.Float2);
            Assert.Equal(0f, obj.Float3);
            Assert.Equal(1e-35f, obj.Float4);
            Assert.Equal(float.MinValue, obj.Float5);
            Assert.Equal(float.MaxValue, obj.Float6);
            
            Assert.Equal(2.2d, obj.Double1);
            Assert.Equal(double.MinValue, obj.Double2);
            Assert.Equal(double.MaxValue, obj.Double3);
            
            Assert.Equal(3.3m, obj.Decimal1);
            Assert.Equal(decimal.MinValue, obj.Decimal2);
            Assert.Equal(decimal.MaxValue, obj.Decimal3);
        }

        [Fact]
        public void Consider_CamelCaseConvention()
        {
            var obj = new Dictionary<string, string>
            {
                {"my", "My"},
                {"myFirst", "MyFirst"},
                {"myFirstProperty", "MyFirstProperty"},
                {"myFirstPropertyNaming", "MyFirstPropertyNaming"},
                {"myFirstPropertyNamingConvention", "MyFirstPropertyNamingConvention"},
            }.FillObject(new MyNamingConventionActivatable());
            
            Assert.Equal("My", obj.My);
            Assert.Equal("MyFirst", obj.MyFirst);
            Assert.Equal("MyFirstProperty", obj.MyFirstProperty);
            Assert.Equal("MyFirstPropertyNaming", obj.MyFirstPropertyNaming);
            Assert.Equal("MyFirstPropertyNamingConvention", obj.MyFirstPropertyNamingConvention);
        }
        
        [Fact]
        public void Consider_SnakelCaseConvention()
        {
            var obj = new Dictionary<string, string>
            {
                {"my", "My"},
                {"my_first", "MyFirst"},
                {"my_first_property", "MyFirstProperty"},
                {"my_first_property_naming", "MyFirstPropertyNaming"},
                {"my_first_property_naming_convention", "MyFirstPropertyNamingConvention"},
            }.FillObject(new MyNamingConventionActivatable());
            
            Assert.Equal("My", obj.My);
            Assert.Equal("MyFirst", obj.MyFirst);
            Assert.Equal("MyFirstProperty", obj.MyFirstProperty);
            Assert.Equal("MyFirstPropertyNaming", obj.MyFirstPropertyNaming);
            Assert.Equal("MyFirstPropertyNamingConvention", obj.MyFirstPropertyNamingConvention);
        }

        private class MyActivatable
        {
        }

        private class MyStringActivatable
        {
            public string Prop1 { get; set; }
            public string Prop2 { get; set; }
            public string Prop3 { get; set; }
            public char Prop4 { get; set; }
            public char Prop5 { get; set; }
            public char Prop6 { get; set; }
        }

        private class MyIntegerActivatable
        {
            public sbyte Prop0 { get; set; }
            public int Prop1 { get; set; }
            public Int32 Prop2 { get; set; }
            public short Prop3 { get; set; }
            public Int16 Prop4 { get; set; }
            public long Prop5 { get; set; }
            public Int64 Prop6 { get; set; }
            public IntPtr Prop7 { get; set; }

            public byte UProp0 { get; set; }
            public ushort UProp1 { get; set; }
            public uint UProp2 { get; set; }
            public ulong UProp3 { get; set; }
            public UIntPtr UProp4 { get; set; }
        }

        private class MyBooleanActivatable
        {
            public bool PropTrue1 { get; set; }
            public bool PropTrue2 { get; set; }
            public bool PropTrue3 { get; set; }
            public bool PropTrue4 { get; set; }
            public bool PropTrue5 { get; set; }
            public bool PropFalse1 { get; set; }
            public bool PropFalse2 { get; set; }
            public bool PropFalse3 { get; set; }
            public bool PropFalse4 { get; set; }
        }

        private class MyFloatActivatable
        {
            public float Float1 { get; set; }
            public float Float2 { get; set; }
            public float Float3 { get; set; }
            public float Float4 { get; set; }
            public float Float5 { get; set; }
            public float Float6 { get; set; }
            public double Double1 { get; set; }
            public double Double2 { get; set; }
            public double Double3 { get; set; }
            public decimal Decimal1 { get; set; }
            public decimal Decimal2 { get; set; }
            public decimal Decimal3 { get; set; }
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
