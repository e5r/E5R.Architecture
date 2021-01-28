// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Core
{
    public class Checker
    {
        public static void NotNullArgument(object argObj, string argName)
        {
            if (argObj == null)
            {
                // TODO: Implementar i18n/l10n
                throw new ArgumentNullException(argName);
            }
        }

        public static void NotNullObject(object @object, string objName)
        {
            if (@object == null)
            {
                // TODO: Implementar i18n/l10n
                throw new NullReferenceException($"Object {objName} can not be null.");
            }
        }
        
        public static void NotNullOrEmptyArgument<T>(T[] argArray, string argName)
        {
            if (argArray == null || argArray.Length < 1)
            {
                // TODO: Implementar i18n/l10n
                throw new ArgumentNullException(argName);
            }
        }

        public static void NotEmptyArgument(string argStr, string argName)
        {
            if (string.IsNullOrEmpty(argStr))
            {
                // TODO: Implementar i18n/l10n
                throw new ArgumentNullException(argName);
            }
        }

        public static void NotEmptyOrWhiteArgument(string argStr, string argName)
        {
            if (string.IsNullOrWhiteSpace(argStr))
            {
                // TODO: Implementar i18n/l10n
                throw new ArgumentNullException(argName);
            }
        }
    }
}
