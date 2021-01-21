// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using System.Security.Cryptography;

namespace E5R.Architecture.Core
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public class UniqueId
    {
        private static readonly string InvalidCastErrorMessage =
            $"The string could not be converted to a valid {nameof(UniqueId)}";

        private static readonly char[] ValidHexadecimalChars = new char[]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f',
            'A', 'B', 'C', 'D', 'E', 'F'
        };

        private string StringId { get; }

        public UniqueId() : this(UniqueIdLength.Length64)
        { }

        public UniqueId(UniqueIdLength length)
        {
            switch (length)
            {
                case UniqueIdLength.Length40:
                    StringId = ComputeHash(SHA1.Create(), Guid.NewGuid().ToByteArray());
                    break;
                
                case UniqueIdLength.Length64:
                    StringId = ComputeHash(SHA256.Create(), Guid.NewGuid().ToByteArray());
                    break;
                
                case UniqueIdLength.Length96:
                    StringId = ComputeHash(SHA384.Create(), Guid.NewGuid().ToByteArray());
                    break;
                
                case UniqueIdLength.Length128:
                    StringId = ComputeHash(SHA512.Create(), Guid.NewGuid().ToByteArray());
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(length), length, null);
            }
        }

        public UniqueId(string stringId)
        {
            Checker.NotEmptyOrWhiteArgument(stringId, nameof(stringId));
            
            EnsureLength(stringId);
            EnsureHexadecimal(stringId);
            
            StringId = stringId;
        }

        public override string ToString() => StringId;

        public static implicit operator string(UniqueId uid) => uid?.StringId;

        public static implicit operator UniqueId(string stringId) =>
            stringId != null ? new UniqueId(stringId) : null;

        void EnsureLength(string stringId)
        {
            if (!new[] {40, 64, 96, 128}.Contains(stringId.Length))
            {
                // TODO: Implementar i18n/l10n
                throw new InvalidCastException(InvalidCastErrorMessage);
            }
        }
        
        void EnsureHexadecimal(string stringId)
        {
            if (!stringId.All(c => ValidHexadecimalChars.Contains(c)))
            {
                throw new InvalidCastException(InvalidCastErrorMessage);
            }
        }

        string ComputeHash(HashAlgorithm hash, byte[] bytes)
            => string.Concat(hash.ComputeHash(bytes).Select(s => s.ToString("x2")));
    }
}
