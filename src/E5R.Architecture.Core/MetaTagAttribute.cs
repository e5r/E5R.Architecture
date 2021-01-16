// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Core
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class MetaTagAttribute : Attribute
    {
        public const string DescriptionKey = "Description";
        public const string CustomIdKey = "CustomId";
        
        public MetaTagAttribute(string tagKey, string tagValue)
        {
            Checker.NotNullArgument(tagKey, nameof(tagKey));
            Checker.NotNullArgument(tagValue, nameof(tagValue));

            TagKey = tagKey;
            TagValue = tagValue;
        }
        
        public string TagKey { get; }
        public string TagValue { get; }
    }
}
