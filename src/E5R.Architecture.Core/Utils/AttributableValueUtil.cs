// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

namespace E5R.Architecture.Core.Utils
{
    public static class AttributableValueUtil
    {
        public static bool Assigned<TValue>(AttributableValue<TValue> value)
        {
            if (value == null)
            {
                return false;
            }

            return (bool)value?.Assigned;
        }
    }
}
