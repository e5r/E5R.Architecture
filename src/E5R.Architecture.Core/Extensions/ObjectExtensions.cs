// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;

namespace E5R.Architecture.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static TObject Fill<TObject>(this TObject @object,
            Dictionary<string, string> dictionary)
        {
            Checker.NotNullArgument(@object, nameof(@object));
            Checker.NotNullArgument(dictionary, nameof(dictionary));

            return dictionary.FillObject(@object);
        }

        public static TTo CopyPropertyValuesTo<TFrom, TTo>(this TFrom from, TTo to, out int count)
        {
            count = CopyPropertyValuesTo(from, to);

            return to;
        }

        public static int CopyPropertyValuesTo<TFrom, TTo>(this TFrom from, TTo to)
        {
            Checker.NotNullArgument(from, nameof(from));
            Checker.NotNullArgument(to, nameof(to));

            var fromProperties = typeof(TFrom).GetProperties();
            var updateableProperties = typeof(TTo).GetProperties().Where(w =>
                w.CanWrite &&
                // TODO: Implementar conversão entre tipos de dados
                // NOTE: Aqui estamos considerando o nome e o tipo exato dos dados, porém
                //       é possível copiar valores "entre tipos" que possam ser convertidos
                //       entre si. O problema, além de criar a estrutura de conversão, é
                //       definir o que fazer quando não for possível a conversão.
                fromProperties.Any(a => a.Name == w.Name && a.PropertyType == w.PropertyType));

            int count = 0;

            updateableProperties
                .ToList()
                .ForEach(updateableProp =>
                {
                    var fromProp =
                        fromProperties.FirstOrDefault(w => w.Name == updateableProp.Name);

                    // Esta exceção nunca deve ocorrer, mas... "O seguro morreu de velho"
                    if (fromProp == null)
                    {
                        // TODO: Implementar i18n/l10n
                        throw new Exception(
                            $"No matching data was found to update property {fromProp.Name}");
                    }

                    var fromValue = fromProp.GetValue(from);

                    updateableProp.SetValue(to, fromValue);

                    count++;
                });

            return count;
        }
    }
}
