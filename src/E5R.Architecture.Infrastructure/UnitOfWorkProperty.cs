// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using E5R.Architecture.Core;

namespace E5R.Architecture.Infrastructure
{
    public class UnitOfWorkProperty<TProperty>
        where TProperty : class
    {
        private readonly TProperty _property;

        public UnitOfWorkProperty(UnitOfWorkByProperty uow)
        {
            Checker.NotNullArgument(uow, nameof(uow));

            _property = uow.Property<TProperty>()
                ?? throw new NullReferenceException();
        }

        public static implicit operator TProperty(UnitOfWorkProperty<TProperty> p)
        {
            return p._property;
        }
    }
}
