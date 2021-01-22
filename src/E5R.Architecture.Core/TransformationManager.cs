// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using E5R.Architecture.Core.Exceptions;

namespace E5R.Architecture.Core
{
    /// <summary>
    /// Default simple transformation manager
    /// </summary>
    public class TransformationManager : ITransformationManager
    {
        private readonly IServiceProvider _serviceProvider;

        public TransformationManager(IServiceProvider serviceProvider)
        {
            Checker.NotNullArgument(serviceProvider, nameof(serviceProvider));
            
            _serviceProvider = serviceProvider;
        }

        public TTo Transform<TFrom, TTo>(TFrom @from) where TTo : new()
            => ResolveType<ITransformer<TFrom, TTo>>().Transform(@from);


        public TTo Transform<TFrom, TTo, TOperation>(TFrom @from, TOperation operation)
            where TTo : new() where TOperation : Enum
            => ResolveType<ITransformer<TFrom, TTo, TOperation>>().Transform(@from, operation);

        TType ResolveType<TType>()
        {
            var serviceType = typeof(TType);
            var service = _serviceProvider.GetService(serviceType);
            
            if (service == null)
            {
                // TODO: Implementar i18n/l10n
                throw new InfrastructureLayerException(
                    $"Type {serviceType.FullName} could not be resolved");
            }

            return (TType) service;
        }
    }
}
