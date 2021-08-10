// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using E5R.Architecture.Core;
using E5R.Architecture.Core.Exceptions;

namespace E5R.Architecture.Business
{
    public class TransformerBasedActionHandler<TInput>
    {
        private readonly ILazy<ITransformationManager> _transformer;

        protected TransformerBasedActionHandler(ILazy<ITransformationManager> transformer)
        {
            Checker.NotNullArgument(transformer, nameof(transformer));

            _transformer = transformer;
        }

        protected TInput TransformInput<TFrom>(TFrom @from)
        {
            Checker.NotNullArgument(@from, nameof(@from));

            if (@from.GetType() == typeof(TInput))
            {
                // TODO: Implementar i18n/l10n
                var exception = new InvalidOperationException(
                    "The input object is already of the expected type. You should use Exec() instead of Exec<>()");
                throw new BusinessLayerException(exception);
            }

            var input = _transformer.Value.Transform<TFrom, TInput>(@from);

            if (input == null)
            {
                // TODO: Implementar i18n/l10n
                throw new BusinessLayerException(
                    "The input type cannot be transformed to the expected type properly");
            }

            return input;
        }
    }
}
