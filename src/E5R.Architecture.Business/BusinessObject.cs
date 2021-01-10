// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Threading.Tasks;
using E5R.Architecture.Core;
using E5R.Architecture.Core.Exceptions;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Business
{
    /// <summary>
    /// Business object
    /// </summary>
    /// <remarks>Base to implements business rules</remarks>
    /// <typeparam name="TObject">Type of a business object</typeparam>
    /// <typeparam name="TModule">Type of a data module</typeparam>
    public class BusinessObject<TObject, TModule>
        where TObject : class
        where TModule : IDataMouleSignature
    {
        protected TModule Module { get; private set; }

        private readonly Type _originType;

        protected BusinessObject(object origin)
        {
            Checker.NotNullArgument(origin, nameof(origin));

            _originType = origin.GetType();
        }

        protected void EnsureOrigin<TOrigin>()
        {
            var expectedType = typeof(TOrigin);

            if (_originType == null || _originType != expectedType)
            {
                // TODO: Implementar i18n/l10n
                throw new BusinessLayerException(
                    $"This operation requires a business object created from the [{expectedType.Name}] model.");
            }
        }

        protected async Task<TResult> RunWithEnsureOriginAsync<TOrigin, TResult>(
            Func<TResult> function)
            => await Task.Run(() =>
            {
                EnsureOrigin<TOrigin>();

                return function();
            });

        protected async Task RunWithEnsureOriginAsync<TOrigin>(Action action)
            => await Task.Run(() =>
            {
                EnsureOrigin<TOrigin>();

                action();
            });

        public TObject Anchor(TModule module)
        {
            Checker.NotNullArgument(module, nameof(module));

            Module = module;

            return this as TObject;
        }
    }
}
