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
        }

        [Fact]
        public void DictionaryStart()
        {
            var parameters = new Parameters(new Dictionary<string, object>
            {
                { "key1", "" },
                { "key2", "" }
            });

            Assert.NotEmpty(parameters);
            Assert.Equal(2, parameters.Count);
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

            parameters.Add("key1", new { object1 = "" });
            parameters.Add("key2", new { object2 = "" });
            parameters.Add("key3", new { object3 = "" });

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
        public void Contains()
        {
            var parameters = new Parameters();

            var obj1 = new { object1 = "" };
            var obj2 = new { object2 = "" };

            parameters.Add("key1", obj1);

            Assert.Contains(new KeyValuePair<string, object>("key1", obj1), parameters);
            Assert.DoesNotContain(new KeyValuePair<string, object>("key2", obj1), parameters);
            Assert.DoesNotContain(new KeyValuePair<string, object>("key1", obj2), parameters);
        }

        [Fact]
        public void Allows_SameKey_WithMultipleValues()
        {
            var parameters = new Parameters();

            var obj1 = new { value = "object 1" };
            var obj2 = new { value = "object 2" };

            parameters.Add("key", obj1);
            parameters.Add("key", obj2);

            Assert.Single(parameters);
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

        [Fact]
        public void TypedTryGet_Returns_ATypedList()
        {
            var parameters = new Parameters();

            var obj1 = "object 1";
            var obj2 = "object 2";

            parameters.Add("key", obj1);
            parameters.Add("key", obj2);

            var exists = parameters.TryGetValue<string>("key", out IList<string> values);

            Assert.True(exists);
            Assert.NotNull(values);
            Assert.True(typeof(IList<string>).IsAssignableFrom(values.GetType()));
            Assert.Equal("object 1object 2", values.Aggregate((a, b) => a + b));
        }

        [Fact]
        public void TypedTryGet_RaisesAnException_WhenTryingToGetDifferentTypeOfValue()
        {
            var parameters = new Parameters();
            var obj1 = "object 1";

            parameters.Add("key", obj1);

            var exception = Assert.Throws<InvalidCastException>(() =>
            {
                parameters.TryGetValue<int>("key", out IList<int> values);
            });

            Assert.NotNull(exception);
        }

        [Fact]
        public void TypedTryGetFirstValue_Returns_NotNull()
        {
            var parameters = new Parameters();

            var obj1 = "object 1";

            parameters.Add("key", obj1);

            var exists = parameters.TryGetFirstValue<string>("key", out string value);

            Assert.True(exists);
            Assert.NotNull(value);
            Assert.False(string.IsNullOrEmpty(value));
            Assert.Equal("object 1", value);
        }

        [Fact]
        public void TypedTryGetFirstValue_Returns_Null()
        {
            var parameters = new Parameters();
            var exists = parameters.TryGetFirstValue<string>("not exists key", out string value);

            Assert.False(exists);
            Assert.Null(value);
            Assert.True(string.IsNullOrEmpty(value));
        }

        [Fact]
        public void TypedTryGetFirstValue_RaisesAnException_WhenTryingToGetDifferentTypeOfValue()
        {
            var parameters = new Parameters();
            var obj1 = "object 1";

            parameters.Add("key", obj1);

            var exception = Assert.Throws<InvalidCastException>(() =>
            {
                parameters.TryGetFirstValue<int>("key", out int values);
            });

            Assert.NotNull(exception);
        }

        [Fact]
        public void TypedFirstOrDefaultValue_Returns_NotNull()
        {
            var parameters = new Parameters();

            var obj1 = "object 1";

            parameters.Add("key", obj1);

            var value = parameters.FirstOrDefaultValue<string>("key");

            Assert.NotNull(value);
            Assert.False(string.IsNullOrEmpty(value));
            Assert.Equal("object 1", value);
        }

        [Fact]
        public void TypedFirstOrDefaultValue_Returns_Null()
        {
            var parameters = new Parameters();
            var value = parameters.FirstOrDefaultValue<string>("not exists key");

            Assert.Null(value);
            Assert.True(string.IsNullOrEmpty(value));
        }

        [Fact]
        public void TypedFirstOrDefaultValue_RaisesAnException_WhenTryingToGetDifferentTypeOfValue()
        {
            var parameters = new Parameters();
            var obj1 = "object 1";

            parameters.Add("key", obj1);

            var exception = Assert.Throws<InvalidCastException>(() =>
            {
                parameters.FirstOrDefaultValue<int>("key");
            });

            Assert.NotNull(exception);
        }

        [Fact]
        public void Values_AreCombined_IntoASingleList()
        {
            var parameters = new Parameters();

            parameters.Add("key1", "Value 1 for key 1");
            parameters.Add("key1", "Value 2 for key 1");
            parameters.Add("key1", "Value 3 for key 1");
            parameters.Add("key2", "Value 1 for key 2");
            parameters.Add("key2", "Value 2 for key 2");
            parameters.Add("key2", "Value 3 for key 2");

            Assert.Equal(6, parameters.Values.Count);
            Assert.Equal(""
                + "Value 1 for key 1,"
                + "Value 2 for key 1,"
                + "Value 3 for key 1,"
                + "Value 1 for key 2,"
                + "Value 2 for key 2,"
                + "Value 3 for key 2", string.Join(',', parameters.Values));
        }

        [Fact]
        public void ValuesCanBe_ObtainedByIndexing()
        {
            var parameters = new Parameters();

            parameters.Add("key1", "Value 1 for key 1");
            parameters.Add("key1", "Value 2 for key 1");
            parameters.Add("key1", "Value 3 for key 1");
            parameters.Add("key2", "Value 1 for key 2");
            parameters.Add("key2", "Value 2 for key 2");
            parameters.Add("key2", "Value 3 for key 2");

            var key1 = parameters["key1"];
            var key2 = parameters["key2"];

            Assert.NotNull(key1);
            Assert.NotNull(key2);

            Assert.IsType<List<object>>(key1);
            Assert.IsType<List<object>>(key2);

            Assert.Equal(3, (key1 as List<object>).Count);
            Assert.Equal(3, (key2 as List<object>).Count);

            Assert.Equal(""
                + "Value 1 for key 1,"
                + "Value 2 for key 1,"
                + "Value 3 for key 1", string.Join(',', key1 as List<object>));

            Assert.Equal(""
                + "Value 1 for key 2,"
                + "Value 2 for key 2,"
                + "Value 3 for key 2", string.Join(',', key2 as List<object>));
        }

        [Fact]
        public void Allows_Iterations()
        {
            var parameters = new Parameters();

            parameters.Add("key1", "Value 1 for key 1");
            parameters.Add("key1", "Value 2 for key 1");
            parameters.Add("key1", "Value 3 for key 1");
            parameters.Add("key2", "Value 1 for key 2");
            parameters.Add("key2", "Value 2 for key 2");
            parameters.Add("key2", "Value 3 for key 2");

            string keys = string.Empty;
            string values = string.Empty;

            foreach (var param in parameters)
            {
                Assert.Contains("key", param.Key);
                Assert.Equal(3, (param.Value as List<object>).Count);
            }
        }
    }
}
