// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;

namespace E5R.Architecture.Infrastructure
{
    using Abstractions;

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

            // TODO: Precisa mesmo do propertyType == targetType?
            if (propertyType == targetType || targetType.IsAssignableFrom(propertyType))
            {
                return (T)property;
            }

            throw new InvalidCastException();
        }

        public void Property<T>(Func<T> getter) where T : class
        {
            var targetType = typeof(T);

            if (_properties.ContainsKey(targetType))
            {
                throw new InvalidOperationException();
            }

            _properties.Add(targetType, getter as Func<object>);
        }
    }
}
