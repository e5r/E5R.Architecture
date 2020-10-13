// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using E5R.Architecture.Core;
using E5R.Architecture.Core.Exceptions;
using E5R.Architecture.Infrastructure.Abstractions;

namespace E5R.Architecture.Infrastructure
{
    public abstract class UnitOfWorkByProperty : IUnitOfWork
    {
        IDictionary<Type, Func<object>> _properties = new Dictionary<Type, Func<object>>();

        public abstract void SaveWork();

        public abstract void DiscardWork();

        public T Property<T>() where T : class
        {
            var targetType = typeof(T);
            Func<object> getProperty;

            if (!_properties.TryGetValue(targetType, out getProperty))
            {
                return null;
            }

            object property = getProperty();

            if (property == null)
            {
                return null;
            }

            var propertyType = property.GetType();

            if (!targetType.IsAssignableFrom(propertyType))
            {
                throw new InvalidCastException();
            }

            return property as T;
        }

        public void Property(Type targetType, Func<object> getter)
        {
            Checker.NotNullArgument(targetType, nameof(targetType));
            Checker.NotNullArgument(getter, nameof(getter));

            if (_properties.ContainsKey(targetType))
            {
                var ex = new InvalidOperationException($"The {targetType.FullName} property has already been registered");

                throw new InfrastructureLayerException(ex);
            }

            _properties.Add(targetType, getter);
        }
    }
}
