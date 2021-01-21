// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using E5R.Architecture.Core.Exceptions;

namespace E5R.Architecture.Core
{
    /// <summary>
    /// Implements lazy loading based on an <see cref="IServiceProvider"/> 
    /// </summary>
    /// <typeparam name="TTarget">Target type</typeparam>
    public class LazyResolver<TTarget> : ILazy<TTarget>
    {
        private readonly IServiceProvider _serviceProvider;
        private TTarget _instance;

        public LazyResolver(IServiceProvider serviceProvider)
        {
            Checker.NotNullArgument(serviceProvider, nameof(serviceProvider));
            
            _serviceProvider = serviceProvider;
        }
        
        public TTarget Value
        {
            get
            {
                if (_instance == null)
                {
                    var targetType = typeof(TTarget);
                    var instance = _serviceProvider.GetService(targetType);

                    if (instance == null)
                    {
                        // TODO: Implementar i18n/l10n
                        throw new InfrastructureLayerException($"Type {targetType.FullName} could not be resolved");
                    }
                    
                    _instance = (TTarget) instance;
                }
                
                return _instance;
            }
        }
    }
}
