// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using System.Reflection;

namespace E5R.Architecture.Core.Internal
{
    internal sealed class NamingConventionsPropertyInfoFinder : IPropertyInfoFinder
    {
        private static IPropertyInfoFinder _instance;

        private NamingConventionsPropertyInfoFinder()
        {
        }

        private IEnumerable<IPropertyInfoFinder> GetFinders()
        {
            yield return NamingExactPropertyInfoFinder.Instance;
            yield return NamingCamelCasePropertyInfoFinder.Instance;
            yield return NamingSnakeCasePropertyInfoFinder.Instance;
        }

        public PropertyInfo FindPropertyInfo(IEnumerable<PropertyInfo> properties,
            string propertyName)
        {
            Checker.NotNullArgument(properties, nameof(properties));
            Checker.NotEmptyOrWhiteArgument(propertyName, nameof(propertyName));

            foreach (var finder in GetFinders())
            {
                var prop = finder.FindPropertyInfo(properties, propertyName);

                if (prop == null)
                {
                    continue;
                }

                return prop;
            }

            return null;
        }

        public static IPropertyInfoFinder Instance =>
            _instance ?? (_instance = new NamingConventionsPropertyInfoFinder());
    }
}
