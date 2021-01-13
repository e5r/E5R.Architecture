// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using E5R.Architecture.Core.Exceptions;

namespace E5R.Architecture.Infrastructure
{
    public class UnitOfWorkPropertyStrategyBuilderOptions
    {
        public IDictionary<Type, Action<UnitOfWorkByProperty, object>> Properties
        {
            get;
            private set;
        } = new Dictionary<Type, Action<UnitOfWorkByProperty, object>>();

        public UnitOfWorkPropertyStrategyBuilderOptions AddProperty<TType>()
            where TType : class
        => AddProperty<TType>((_uow, _object) => { });

        public UnitOfWorkPropertyStrategyBuilderOptions TryAddProperty<TType>()
            where TType : class
        => TryAddProperty<TType>((_uow, _object) => { });

        public UnitOfWorkPropertyStrategyBuilderOptions AddProperty<TType>(Action<UnitOfWorkByProperty, TType> config)
            where TType : class
        {
            var propertyList = Properties as Dictionary<Type, Action<UnitOfWorkByProperty, object>>;
            var targetType = typeof(TType);

            if (propertyList.ContainsKey(targetType))
            {
                throw new InfrastructureLayerException($"The {targetType.FullName} property has already been registered");
            }

            propertyList.Add(targetType, (uow, @object) => config(uow, @object as TType));

            return this;
        }

        public UnitOfWorkPropertyStrategyBuilderOptions TryAddProperty<TType>(Action<UnitOfWorkByProperty, TType> config)
            where TType : class
        {
            var propertyList = Properties as Dictionary<Type, Action<UnitOfWorkByProperty, object>>;
            var targetType = typeof(TType);

            if (propertyList.ContainsKey(targetType))
            {
                return this;
            }

            propertyList.Add(targetType, (uow, @object) => config(uow, @object as TType));

            return this;
        }
    }
}
