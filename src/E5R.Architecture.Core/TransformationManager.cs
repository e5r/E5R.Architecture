// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using E5R.Architecture.Core.Exceptions;
using E5R.Architecture.Core.Extensions;

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

        #region Item transformer
        public TTo Transform<TFrom, TTo>(TFrom from)
        {
            return ResolveType<ITransformer<TFrom, TTo>>().Transform(from);
        }

        public TTo AutoTransform<TFrom, TTo>(TFrom from) where TTo : new()
        {
            Checker.NotNullArgument(from, nameof(from));

            var t = ResolveOptionalType<ITransformer<TFrom, TTo>>();

            // Encontramos um ITransformer? Então vamos usá-lo e pronto.
            if (t != null)
            {
                return t.Transform(from);
            }

            // Não encontramos um ITransformer. Então vamos criar um objeto e copiar as
            // propriedades, pois é o que dá pra fazer por agora.
            var to = new TTo();
            var _ = from.CopyPropertyValuesTo(to);

            return to;
        }
        #endregion

        #region List transformer
        public IEnumerable<TTo> Transform<TFrom, TTo>(IEnumerable<TFrom> from)
        {
            Checker.NotNullArgument(from, nameof(from));

            var t = ResolveType<ITransformer<TFrom, TTo>>();

            return from.ToList().ConvertAll(t.Transform);
        }

        public IEnumerable<TTo> AutoTransform<TFrom, TTo>(IEnumerable<TFrom> from) where TTo : new()
        {
            Checker.NotNullArgument(from, nameof(from));

            var t = ResolveOptionalType<ITransformer<TFrom, TTo>>();

            // Encontramos um ITransformer? Então vamos usá-lo e pronto.
            if (t != null)
            {
                return from.ToList().ConvertAll(t.Transform);
            }

            // Não encontramos um ITransformer. Então vamos criar um objeto de cada
            //  e copiar as propriedades, pois é o que dá pra fazer por agora.
            return from.Select(s =>
            {
                var to = new TTo();
                var _ = s.CopyPropertyValuesTo(to);

                return to;
            });
        }

        IEnumerable<TTo> ITransformationManager.Transform<TFrom, TTo, TOperation>(IEnumerable<TFrom> from, TOperation operation)
        {
            Checker.NotNullArgument(from, nameof(from));

            var t = ResolveType<ITransformer<TFrom, TTo>>();

            return from.ToList().ConvertAll(t.Transform);
        }
        #endregion

        #region Paginated list transformer
        public PaginatedResult<TTo> Transform<TFrom, TTo>(PaginatedResult<TFrom> from)
        {
            Checker.NotNullArgument(from, nameof(from));

            var result = Transform<TFrom, TTo>(from.Result);

            return new PaginatedResult<TTo>(result, from.Offset, from.Limit, from.Total);
        }

        public PaginatedResult<TTo> AutoTransform<TFrom, TTo>(PaginatedResult<TFrom> from) where TTo : new()
        {
            Checker.NotNullArgument(from, nameof(from));

            var result = AutoTransform<TFrom, TTo>(from.Result);

            return new PaginatedResult<TTo>(result, from.Offset, from.Limit, from.Total);
        }

        PaginatedResult<TTo> ITransformationManager.Transform<TFrom, TTo, TOperation>(PaginatedResult<TFrom> from, TOperation operation)
        {
            Checker.NotNullArgument(from, nameof(from));

            var result = ((ITransformationManager)this).Transform<TFrom, TTo, TOperation>(from.Result, operation);

            return new PaginatedResult<TTo>(result, from.Offset, from.Limit, from.Total);
        }
        #endregion

        private TType ResolveOptionalType<TType>() => (TType)_serviceProvider.GetService(typeof(TType));

        private TType ResolveType<TType>()
        {
            var service = ResolveOptionalType<TType>();

            if (service == null)
            {
                // TODO: Implementar i18n/l10n
                throw new InfrastructureLayerException(
                    $"Type {typeof(TType).FullName} could not be resolved");
            }

            return (TType)service;
        }
    }
}
