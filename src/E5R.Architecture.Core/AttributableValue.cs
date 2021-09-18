// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Core
{
    /// <summary>
    /// Value with assignment control
    /// </summary>
    public class AttributableValue<TValue>
    {
        private TValue _value;

        public AttributableValue()
        {
            Assigned = false;
        }

        public AttributableValue(TValue value)
        {
            _value = value;
            Assigned = true;
        }

        public bool Assigned { get; private set; }

        public TValue Value
        {
            get
            {
                if (!Assigned)
                {
                    // TODO: Implementar i18n/l10n
                    throw new InvalidOperationException("There is no assigned value");
                }

                return _value;
            }
        }

        public static implicit operator AttributableValue<TValue>(TValue value) => new AttributableValue<TValue>(value);

        public static implicit operator TValue(AttributableValue<TValue> value) => value.Value;
    }
}
