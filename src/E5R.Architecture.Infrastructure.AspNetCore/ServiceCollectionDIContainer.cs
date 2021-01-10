// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using E5R.Architecture.Core;
using E5R.Architecture.Infrastructure.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace E5R.Architecture.Infrastructure.AspNetCore
{
    public class ServiceCollectionDIContainer : IDIContainer
    {
        private readonly IServiceCollection _serviceCollection;

        public ServiceCollectionDIContainer(IServiceCollection serviceCollection)
        {
            Checker.NotNullArgument(serviceCollection, nameof(serviceCollection));

            _serviceCollection = serviceCollection;
        }

        public void Register(DILifetime lifetime, Type implementationType)
        {
            var serviceDescriptor = new ServiceDescriptor(
                implementationType, implementationType, lifetime.ToServiceLifetime()
            );

            _serviceCollection.Add(serviceDescriptor);
        }

        public void Register(DILifetime lifetime, Type baseType, Type implementationType)
        {
            var serviceDescriptor = new ServiceDescriptor(
                baseType, implementationType, lifetime.ToServiceLifetime()
            );

            _serviceCollection.Add(serviceDescriptor);
        }

        public void Register(DILifetime lifetime, Type baseType, Func<object> implementationFactory)
        {
            var serviceDescriptor = new ServiceDescriptor(
                baseType, _ => implementationFactory(), lifetime.ToServiceLifetime()
            );

            _serviceCollection.Add(serviceDescriptor);
        }
    }
}
