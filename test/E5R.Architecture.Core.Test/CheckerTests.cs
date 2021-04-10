// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Data;
using System.Linq.Expressions;
using Xunit;

namespace E5R.Architecture.Core.Test
{
    public class CheckerTests
    {
        [Fact]
        public void ItIsExpected_ToUseNull_InTheArgumentName()
        {
            var ex1 =
                Assert.Throws<ArgumentNullException>(() =>
                    Checker.NotNullArgument(null, (string) null));
            var ex2 = Assert.Throws<ArgumentNullException>(() =>
                Checker.NotNullArgument(null, (Expression<Func<object>>) null));

            Assert.Null(ex1.ParamName);
            Assert.Null(ex2.ParamName);
        }

        [Fact]
        public void Raises_Exception_When_Parameter_IsNull()
        {
            var exception =
                Assert.Throws<ArgumentNullException>(() => Checker.NotNullArgument(null, "name"));

            Assert.Equal("name", exception.ParamName);
        }

        [Fact]
        public void Nothing_Occurs_When_Parameter_IsNotNull()
        {
            Checker.NotNullArgument(new object(), "nothing");
        }

        [Fact]
        public void Raises_Exception_When_Object_IsNull()
        {
            var exception =
                Assert.Throws<NullReferenceException>(() =>
                    Checker.NotNullObject(null, "objectName"));

            Assert.Equal("Object objectName can not be null", exception.Message);
        }

        [Fact]
        public void Nothing_Occurs_When_Object_IsNotNull()
        {
            Checker.NotNullObject(new object(), "nothing");
        }

        [Fact]
        public void Raises_Exception_When_Parameter_IsEmpty()
        {
            var ex1 =
                Assert.Throws<ArgumentNullException>(() =>
                    Checker.NotEmptyArgument(string.Empty, "name1"));
            var ex2 =
                Assert.Throws<ArgumentNullException>(() =>
                    Checker.NotNullOrEmptyArgument(string.Empty.ToCharArray(), "name2"));

            Assert.Equal("name1", ex1.ParamName);
            Assert.Equal("name2", ex2.ParamName);
        }

        [Fact]
        public void Nothing_Occurs_When_Parameter_IsNotEmpty()
        {
            Checker.NotEmptyArgument("not empty", "nothing");
            Checker.NotNullOrEmptyArgument(new byte[1], "nothing");
        }

        [Fact]
        public void Raises_Exception_When_Parameter_IsEmptyOrWhite()
        {
            var ex1 =
                Assert.Throws<ArgumentNullException>(() =>
                    Checker.NotEmptyOrWhiteArgument("       ", "name"));

            Assert.Equal("name", ex1.ParamName);
        }

        [Fact]
        public void Nothing_Occurs_When_Parameter_IsNotEmptyOrWhite()
        {
            Checker.NotEmptyArgument("not empty", "nothing");
            Checker.NotNullOrEmptyArgument(new byte[1], "nothing");
        }

        [Fact]
        public void OnlyMemberAccessExpression_AreAllowed()
        {
            var ex1 = Assert.Throws<InvalidExpressionException>(() =>
                Checker.NotNullArgument(null, () => 1 * 2));

            Assert.Equal("Only MemberExpression are allowed", ex1.Message);
        }

        [Fact]
        public void ExpressionsCanBeUsed_ToDefine_AnArgumentName()
        {
            var paramName1 = string.Empty;
            ClassOuter paramName2 = null;

            var ex1 =
                Assert.Throws<ArgumentNullException>(() =>
                    Checker.NotNullArgument(null, () => paramName1));

            var ex2 =
                Assert.Throws<ArgumentNullException>(() =>
                    Checker.NotNullArgument(paramName2?.InnerProp?.InnerInnerProp,
                        () => paramName2.InnerProp.InnerInnerProp));

            Assert.Equal("paramName1", ex1.ParamName);
            Assert.Equal("paramName2.InnerProp.InnerInnerProp", ex2.ParamName);
        }

        [Fact]
        public void ExpressionsCanBeUsed_ToDefine_AnArgumentNameInAllExtensionsMethods()
        {
            ClassOuter paramName = null;

            var ex1 = Assert.Throws<ArgumentNullException>(() =>
                Checker.NotNullArgument(null, () => paramName.InnerProp.InnerInnerProp));
            var ex2 = Assert.Throws<NullReferenceException>(() =>
                Checker.NotNullObject(null, () => paramName.InnerProp.InnerInnerProp));
            var ex3 = Assert.Throws<ArgumentNullException>(() =>
                Checker.NotEmptyArgument(null, () => paramName.InnerProp.InnerInnerProp));
            var ex4 = Assert.Throws<ArgumentNullException>(() =>
                Checker.NotEmptyArgument(string.Empty, () => paramName.InnerProp.InnerInnerProp));
            var ex5 = Assert.Throws<ArgumentNullException>(() =>
                Checker.NotEmptyOrWhiteArgument(null, () => paramName.InnerProp.InnerInnerProp));
            var ex6 = Assert.Throws<ArgumentNullException>(() =>
                Checker.NotEmptyOrWhiteArgument(string.Empty,
                    () => paramName.InnerProp.InnerInnerProp));
            var ex7 = Assert.Throws<ArgumentNullException>(() =>
                Checker.NotEmptyOrWhiteArgument("      ",
                    () => paramName.InnerProp.InnerInnerProp));
            var ex8 = Assert.Throws<ArgumentNullException>(() =>
                Checker.NotNullOrEmptyArgument((int[]) null,
                    () => paramName.InnerProp.InnerInnerProp));
            var ex9 = Assert.Throws<ArgumentNullException>(() =>
                Checker.NotNullOrEmptyArgument(new int[0],
                    () => paramName.InnerProp.InnerInnerProp));

            Assert.Equal("paramName.InnerProp.InnerInnerProp", ex1.ParamName);
            Assert.Equal("Object paramName.InnerProp.InnerInnerProp can not be null", ex2.Message);
            Assert.Equal("paramName.InnerProp.InnerInnerProp", ex3.ParamName);
            Assert.Equal("paramName.InnerProp.InnerInnerProp", ex4.ParamName);
            Assert.Equal("paramName.InnerProp.InnerInnerProp", ex5.ParamName);
            Assert.Equal("paramName.InnerProp.InnerInnerProp", ex6.ParamName);
            Assert.Equal("paramName.InnerProp.InnerInnerProp", ex7.ParamName);
            Assert.Equal("paramName.InnerProp.InnerInnerProp", ex8.ParamName);
            Assert.Equal("paramName.InnerProp.InnerInnerProp", ex9.ParamName);
        }
    }

    public class ClassOuter
    {
        public ClassInner InnerProp { get; set; }
    }

    public class ClassInner
    {
        public ClassInnerInner InnerInnerProp { get; set; }
    }

    public class ClassInnerInner
    {
    }
}
