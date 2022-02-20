// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using E5R.Architecture.Core.Extensions;

namespace E5R.Architecture.Core
{
    /// <summary>
    /// A unique identifier optimizer for <see cref="String"/>.
    /// </summary>
    public class UniqueIdentifier
    {
        private static readonly string InvalidCastErrorMessage =
            $"The string could not be converted to a valid {nameof(UniqueIdentifier)}";

        private string StringId { get; }

        public UniqueIdentifier() : this(UniqueIdentifierLength.Length64)
        {
        }

        public UniqueIdentifier(UniqueIdentifierLength length)
        {
            switch (length)
            {
                case UniqueIdentifierLength.Length32:
                    StringId = Guid.NewGuid().ToString("N");
                    break;

                case UniqueIdentifierLength.Length64:
                    StringId = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
                    break;

                case UniqueIdentifierLength.Length96:
                    StringId = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N") +
                               Guid.NewGuid().ToString("N");
                    break;

                case UniqueIdentifierLength.Length128:
                    StringId = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N") +
                               Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(length), length, null);
            }
        }

        public UniqueIdentifier(string stringId)
        {
            Checker.NotEmptyOrWhiteArgument(stringId, nameof(stringId));

            EnsureLength(stringId);
            EnsureHexadecimal(stringId);

            StringId = stringId;
        }

        public override string ToString() => StringId;

        public static implicit operator string(UniqueIdentifier uid) => uid?.StringId;

        public static implicit operator UniqueIdentifier(string stringId) =>
            stringId != null ? new UniqueIdentifier(stringId) : null;

        void EnsureLength(string stringId)
        {
            if (!new[] {32, 40, 64, 96, 128}.Contains(stringId.Length))
                // TODO: Implementar i18n/l10n
                throw new InvalidCastException(InvalidCastErrorMessage);
        }

        void EnsureHexadecimal(string stringId)
        {
            if (!stringId.All(Uri.IsHexDigit))
                throw new InvalidCastException(InvalidCastErrorMessage);
        }
    }
}
