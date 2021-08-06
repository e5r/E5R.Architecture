// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Core.Internal
{
    internal delegate object FromStringDelegate(string value);
    
    internal static class FromStringUtil
    {
        internal static FromStringDelegate FindConverter(Type type)
        {
            Checker.NotNullArgument(type, nameof(type));

            return type == typeof(string) ? s => s
                : type == typeof(sbyte) ? s => ConvertFromString(s, f => Convert.ToSByte(f))
                : type == typeof(byte) ? s => ConvertFromString(s, f => Convert.ToByte(f))
                : type == typeof(short) ? s => ConvertFromString(s, f => Convert.ToInt16(f))
                : type == typeof(ushort) ? s => ConvertFromString(s, f => Convert.ToUInt16(f))
                : type == typeof(int) ? s => ConvertFromString(s, f => Convert.ToInt32(f))
                : type == typeof(uint) ? s => ConvertFromString(s, f => Convert.ToUInt32(f))
                : type == typeof(long) ? s => ConvertFromString(s, f => Convert.ToInt64(f))
                : type == typeof(ulong) ? s => ConvertFromString(s, f => Convert.ToUInt64(f))
                : type == typeof(char) ? s => ConvertFromString(s, f => Convert.ToChar(f))
                : type == typeof(float) ? s => ConvertFromString(s, f => Convert.ToSingle(f))
                : type == typeof(double) ? s => ConvertFromString(s, f => Convert.ToDouble(f))
                : type == typeof(decimal) ? s => ConvertFromString(s, f => Convert.ToDecimal(f))
                : type == typeof(IntPtr) ? IntPtrFromString
                : type == typeof(UIntPtr) ? UIntPtrFromString
                : type == typeof(bool) ? BooleanFromString
                : (FromStringDelegate) null;
        }

        private static object ConvertFromString(string value, Func<string, object> h) =>
            string.IsNullOrWhiteSpace(value) ? default : h(value);

        private static object IntPtrFromString(string value) =>
            IntPtr.Size == 8
                ? new IntPtr(Convert.ToInt64(value))
                : new IntPtr(Convert.ToInt32(value));
        
        private static object UIntPtrFromString(string value) =>
            UIntPtr.Size == 8
                ? new UIntPtr(Convert.ToUInt64(value))
                : new UIntPtr(Convert.ToUInt32(value));

        private static object BooleanFromString(string value)
        {
            if (bool.TrueString.Equals(value, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            if (bool.FalseString.Equals(value, StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            if (long.TryParse(value, out long number))
            {
                return number != 0;
            }

            // TODO: Implementar i18n/l10n
            throw new InvalidCastException("String to boolean conversion failed");
        }
    }
}
