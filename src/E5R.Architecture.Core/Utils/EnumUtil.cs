﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using E5R.Architecture.Core.Extensions;
using E5R.Architecture.Core.Models;

namespace E5R.Architecture.Core.Utils
{
    /// <summary>
    /// Enum utils
    /// </summary>
    public static class EnumUtil
    {
        /// <summary>
        /// Get a enum values on <see cref="EnumModel"/> format
        /// </summary>
        /// <typeparam name="TEnum">The enum type</typeparam>
        /// <returns>Array of <see cref="EnumModel"/></returns>
        public static EnumModel[] GetModels<TEnum>() where TEnum : Enum
            => GetValues<TEnum>().Select(e => (EnumModel) e).ToArray();

        /// <summary>
        /// Get a enum values
        /// </summary>
        /// <typeparam name="TEnum">The enum type</typeparam>
        /// <returns>Array of enum values</returns>
        public static TEnum[] GetValues<TEnum>() where TEnum : Enum
            => (TEnum[]) Enum.GetValues(typeof(TEnum));

        /// <summary>
        /// Get a enum value from a tag value
        /// </summary>
        /// <param name="tagKey">The tag key</param>
        /// <param name="tagValue">The tag value</param>
        /// <typeparam name="TEnum">The enum type</typeparam>
        /// <returns>Value of enum based on tag</returns>
        public static TEnum FromTag<TEnum>(string tagKey, string tagValue) where TEnum : Enum
        {
            Checker.NotEmptyOrWhiteArgument(tagKey, nameof(tagKey));
            Checker.NotEmptyOrWhiteArgument(tagValue, nameof(tagValue));

            var values = GetValues<TEnum>().Where(e =>
                    string.Equals(e.GetTag(tagKey), tagValue, StringComparison.CurrentCulture))
                .ToArray();

            if (!values.Any())
            {
                // TODO: Implementar i18n/l10n
                throw new InvalidCastException(
                    "The Enum could not be converted because there is no value signed with the informed tag");
            }

            return values.First();
        }
    }
}
