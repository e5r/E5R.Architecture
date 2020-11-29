// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace E5R.Architecture.Core.Test
{
    public class ParametersTests
    {
        [Fact]
        public void EmptyStart()
        {
            var parameters = new Parameters();

            Assert.Empty(parameters);
            Assert.Equal(0, parameters.Count);
        }

        [Fact]
        public void IsNotReadOnly()
        {
            var parameters = new Parameters();

            Assert.False(parameters.IsReadOnly);
        }

        [Fact]
        public void ClearAllData()
        {
            var parameters = new Parameters();

            var initialCount = parameters.Count;

            parameters.Add("key1", new { });
            parameters.Add("key2", new { });
            parameters.Add("key3", new { });

            var beforeCount = parameters.Count;

            parameters.Clear();

            var afterCount = parameters.Count;

            Assert.Equal(0, initialCount);
            Assert.Equal(3, beforeCount);
            Assert.Equal(0, afterCount);
        }

        [Fact]
        public void ContainsKeys()
        {
            var parameters = new Parameters();

            parameters.Add("key1", new { });

            Assert.True(parameters.ContainsKey("key1"));
            Assert.False(parameters.ContainsKey("key2"));
        }

        [Fact]
        public void Allows_SameKey_WithMultipleValues()
        {
            var parameters = new Parameters();

            var obj1 = new { value = "object 1" };
            var obj2 = new { value = "object 2" };

            parameters.Add("key", obj1);
            parameters.Add("key", obj2);

            Assert.Equal(1, parameters.Count);
        }

        [Fact]
        public void AllowsYou_ToRemove_OnlyTheKeyValue()
        {
            var parameters = new Parameters();

            var obj1 = new { value = "object 1" };
            var obj2 = new { value = "object 2" };

            parameters.Add("key", obj1);
            parameters.Add("key", obj2);

            var beforeCount = parameters.Count;

            parameters.Remove(new KeyValuePair<string, object>("key", obj1));

            var afterCount = parameters.Count;

            Assert.Equal(1, beforeCount);
            Assert.Equal(1, afterCount);
        }

        [Fact]
        public void TryGet_Returns_AList()
        {
            var parameters = new Parameters();

            var obj1 = "object 1";
            var obj2 = "object 2";

            parameters.Add("key", obj1);
            parameters.Add("key", obj2);

            var exists = parameters.TryGetValue("key", out object @object);

            Assert.True(exists);
            Assert.NotNull(@object);
            Assert.IsType<List<object>>(@object);
            Assert.Equal(2, (@object as IList<object>).Count);
            Assert.Equal("object 1,object 2", string.Join(',', (@object as IList<object>).Select(s => (string)s)));
        }
    }
}
