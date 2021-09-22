// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
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

        public TTo Transform<TFrom, TTo>(TFrom from)
        {
            Checker.NotNullArgument(from, nameof(from));

            return ResolveType<ITransformer<TFrom, TTo>>().Transform(from);
        }


        public TTo Transform<TFrom, TTo, TOperation>(TFrom from, TOperation operation)
            where TTo : new() where TOperation : Enum
        {
            Checker.NotNullArgument(from, nameof(from));

            return ResolveType<ITransformer<TFrom, TTo, TOperation>>().Transform(@from, operation);
        }

        public IEnumerable<TTo> Transform<TFrom, TTo>(IEnumerable<TFrom> from)
        {
            Checker.NotNullArgument(from, nameof(from));

            var t = ResolveType<ITransformer<TFrom, TTo>>();

            return from.ToList().ConvertAll(t.Transform);
        }

        IEnumerable<TTo> ITransformationManager.Transform<TFrom, TTo, TOperation>(IEnumerable<TFrom> from, TOperation operation)
        {
            Checker.NotNullArgument(from, nameof(from));

            var t = ResolveType<ITransformer<TFrom, TTo>>();

            return from.ToList().ConvertAll(t.Transform);
        }

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

            return (TType)service;
        }
    }
}
