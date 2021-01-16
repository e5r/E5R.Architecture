// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using E5R.Architecture.Core.Extensions;
using E5R.Architecture.Core.Utils;
using Tag =  E5R.Architecture.Core.MetaTagAttribute;

namespace E5R.Architecture.Core.Models
{
    /// <summary>
    /// Enum model for view representation
    /// </summary>
    public class EnumModel
    {
        public string Id { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Convert model to <see cref="TEnum"/> type
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <returns><see cref="TEnum"/> instance or null if the model data is invalid</returns>
        public TEnum ToEnum<TEnum>() where TEnum : Enum
        {
            Checker.NotEmptyOrWhiteArgument(Id, nameof(Id));
            
            var enumValues = EnumUtil.GetValues<TEnum>();
            
            // Fazemos aqui a varredura item a item e logo abaixo novamente
            // porque, se compararmos as duas no mesmo laço, pode ser que
            // tenhamos um "CustomId" configurado com mesmo código numérido de
            // um item, e neste caso prevalece o "CustomId". Por isso precisamos
            // terminar de varrer e comparar todos os "CustomId's".
            // Somente se não encontrarmos um correspondente aqui, procuraremos
            // logo abaixo com relação ao valor numérico.
            foreach (var enumValue in enumValues)
            {
                if (enumValue.GetTag(Tag.CustomIdKey) == Id)
                    return enumValue;
            }

            var enumType = typeof(TEnum);
            
            foreach (var enumValue in enumValues)
            {
                if (Convert.ChangeType(enumValue, Type.GetTypeCode(enumType)).ToString() == Id)
                    return enumValue;
            }

            // Se não encontramos um correspondente retornamos o valor padrão do
            // enum, porque este já é o comportamento padrão conhecido
            return default;
        }

        /// <summary>
        /// Implicit convert <see cref="Enum" /> to <see cref="EnumModel"/>
        /// </summary>
        /// <param name="input">Enum instance</param>
        /// <returns>Instance of <see cref="EnumModel" /></returns>
        public static implicit operator EnumModel(Enum input)
        {
            Checker.NotNullArgument(input, nameof(input));

            var tags = input.GetMetaTags();

            var enumId = tags != null && tags.TryGetValue(Tag.CustomIdKey, out string idTag)
                ? idTag
                : Convert.ChangeType(input, input.GetTypeCode()).ToString();

            var enumDescription = tags != null &&
                                  tags.TryGetValue(Tag.DescriptionKey, out string descriptionTag)
                ? descriptionTag
                : Enum.GetName(input.GetType(), input);
            
            return new EnumModel
            {
                Id = enumId,
                Description = enumDescription
            };
        }
    }
}
