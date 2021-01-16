// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace E5R.Architecture.Core.Extensions
{
    public static class MetaTagAttributeExtensions
    {
        public static IDictionary<string, string> GetMetaTags(this MemberInfo m)
        {
            var metatags =
                (MetaTagAttribute[]) m.GetCustomAttributes(typeof(MetaTagAttribute), true);

            return metatags.ToDictionary(meta => meta.TagKey, meta => meta.TagValue);
        }
        
        public static ICollection<string> GetTags(this MemberInfo m)
            => GetMetaTags(m)?.Select(s => s.Key).ToArray();

        public static string GetTag(this MemberInfo m, string tagName)
            => GetMetaTags(m)?.FirstOrDefault(w => w.Key == tagName).Value;

        public static IDictionary<string, string> GetMetaTags(this Enum e)
        {
            var fieldInfo = e.GetType().GetField(e.ToString());

            if (fieldInfo == null)
            {
                return null;
            }

            return GetMetaTags(fieldInfo);
        }

        public static ICollection<string> GetTags(this Enum e)
            => GetMetaTags(e)?.Select(s => s.Key).ToArray();

        public static string GetTag(this Enum e, string tagName)
            => GetMetaTags(e)?.FirstOrDefault(w => w.Key == tagName).Value;

        public static Dictionary<string, string> GetMetaTags(this Assembly a)
        {
            var metatags =
                (MetaTagAttribute[]) a.GetCustomAttributes(typeof(MetaTagAttribute), true);

            return metatags.ToDictionary(meta => meta.TagKey, meta => meta.TagValue);
        }
        
        public static ICollection<string> GetTags(this Assembly a)
            => GetMetaTags(a)?.Select(s => s.Key).ToArray();

        public static string GetTag(this Assembly a, string tagName)
            => GetMetaTags(a)?.FirstOrDefault(w => w.Key == tagName).Value;
        
        public static Dictionary<string, string> GetMetaTags(this Module m)
        {
            var metatags =
                (MetaTagAttribute[]) m.GetCustomAttributes(typeof(MetaTagAttribute), true);

            return metatags.ToDictionary(meta => meta.TagKey, meta => meta.TagValue);
        }
        
        public static ICollection<string> GetTags(this Module m)
            => GetMetaTags(m)?.Select(s => s.Key).ToArray();

        public static string GetTag(this Module m, string tagName)
            => GetMetaTags(m)?.FirstOrDefault(w => w.Key == tagName).Value;
        
        public static Dictionary<string, string> GetMetaTags(this Type t)
        {
            var metatags =
                (MetaTagAttribute[]) t.GetCustomAttributes(typeof(MetaTagAttribute), true);

            return metatags.ToDictionary(meta => meta.TagKey, meta => meta.TagValue);
        }
        
        public static ICollection<string> GetTags(this Type t)
            => GetMetaTags(t)?.Select(s => s.Key).ToArray();

        public static string GetTag(this Type t, string tagName)
            => GetMetaTags(t)?.FirstOrDefault(w => w.Key == tagName).Value;
    }
}
