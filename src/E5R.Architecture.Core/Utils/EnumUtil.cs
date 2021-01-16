// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
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
    }
}
