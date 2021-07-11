// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using E5R.Architecture.Core.Internal;

namespace E5R.Architecture.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static TObject FillObject<TObject>(this Dictionary<string, string> dictionary,
            TObject objectInstance)
        {
            Checker.NotNullArgument(dictionary, nameof(dictionary));
            Checker.NotNullArgument(objectInstance, nameof(objectInstance));

            var properties = objectInstance.GetType().GetProperties()
                .Where(p => p.CanWrite)
                .ToArray();

            if (!properties.Any()) return objectInstance;

            foreach (var (propName, propValue) in dictionary.Select(d => (d.Key, d.Value)))
            {
                var prop =
                    NamingConventionsPropertyInfoFinder.Instance.FindPropertyInfo(properties,
                        propName);

                if (prop == null)
                {
                    continue;
                }

                var converter = FromStringUtil.FindConverter(prop.PropertyType);

                if (converter == null)
                {
                    // TODO: Implementar i18n/l10n
                    throw new InvalidCastException(
                        $"Cast to type {prop.PropertyType.Name} not supported");
                }

                prop.SetValue(objectInstance, converter(propValue));
            }

            return objectInstance;
        }

        public static TObject Activate<TObject>(this Dictionary<string, string> dictionary)
            where TObject : new()
        {
            Checker.NotNullArgument(dictionary, nameof(dictionary));

            return FillObject(dictionary, new TObject());
        }
    }
}
