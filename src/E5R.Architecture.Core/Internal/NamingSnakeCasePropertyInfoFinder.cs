// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace E5R.Architecture.Core.Internal
{
    internal sealed class NamingSnakeCasePropertyInfoFinder : IPropertyInfoFinder
    {
        private static IPropertyInfoFinder _instance;
        private static TextInfo _textInfo;

        private NamingSnakeCasePropertyInfoFinder()
        {
        }

        public PropertyInfo FindPropertyInfo(IEnumerable<PropertyInfo> properties, string propertyName)
        {
            Checker.NotNullArgument(properties, nameof(properties));
            Checker.NotEmptyOrWhiteArgument(propertyName, nameof(propertyName));

            var targetPropertyName = TextInfo.ToTitleCase(propertyName).Replace("_", string.Empty);

            return properties.FirstOrDefault(p => p.Name.Equals(targetPropertyName));
        }

        public static IPropertyInfoFinder Instance =>
            _instance ?? (_instance = new NamingSnakeCasePropertyInfoFinder());

        private static TextInfo TextInfo =>
            _textInfo ?? (_textInfo = new CultureInfo("en-us", false).TextInfo);
    }
}
