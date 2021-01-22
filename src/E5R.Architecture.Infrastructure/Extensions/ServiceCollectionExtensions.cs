// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using E5R.Architecture.Core;
using E5R.Architecture.Infrastructure;
using E5R.Architecture.Infrastructure.Abstractions;
using E5R.Architecture.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWorkPropertyStrategy<TAppUnitOfWork>(
            this IServiceCollection serviceCollection,
            Action<UnitOfWorkPropertyStrategyBuilderOptions> config)
            where TAppUnitOfWork : UnitOfWorkByProperty
        {
            var options = new UnitOfWorkPropertyStrategyBuilderOptions();

            config(options);

            if (typeof(TAppUnitOfWork) == typeof(DbTransactionUnitOfWork))
            {
                options
                    .TryAddProperty<DbConnection>()
                    .TryAddProperty<DbTransaction>();
            }

            serviceCollection.AddScoped<TAppUnitOfWork>();
            serviceCollection.AddScoped<IUnitOfWork>(
                serviceProvider => serviceProvider.GetRequiredService<TAppUnitOfWork>()
            );
            serviceCollection.AddScoped(typeof(UnitOfWorkProperty<>));

            serviceCollection.AddScoped<UnitOfWorkByProperty>(serviceProvider =>
            {
                var uow = serviceProvider.GetRequiredService<TAppUnitOfWork>();

                foreach (var (type, propConfig) in options.Properties.Select(s => (s.Key, s.Value)))
                {
                    uow.Property(type, () =>
                    {
                        var property = serviceProvider.GetRequiredService(type);

                        propConfig(uow, property);

                        return property;
                    });
                }

                return uow;
            });

            return serviceCollection;
        }
        
        public static IServiceCollection AddUnitOfWorkPropertyStrategy(
            this IServiceCollection serviceCollection,
            Action<UnitOfWorkPropertyStrategyBuilderOptions> config)
        {
            var descriptor = new ServiceDescriptor(typeof(DbTransaction), serviceProvider =>
            {
                var connection = serviceProvider.GetRequiredService<DbConnection>();

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                return connection.BeginTransaction();
            }, ServiceLifetime.Scoped);

            serviceCollection.TryAdd(descriptor);

            return serviceCollection.AddUnitOfWorkPropertyStrategy<DbTransactionUnitOfWork>(config);
        }
        
        public static IServiceCollection AddUnitOfWorkTransactionScopeStrategy(
            this IServiceCollection serviceCollection)
            => serviceCollection.AddScoped<IUnitOfWork, UnitOfWorkByTransactionScope>();
        
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection serviceCollection,
            string[] customServiceAssemblies = null)
        {
            // Habilita "lazy loading"
            serviceCollection.AddScoped(typeof(ILazy<>), typeof(LazyResolver<>));
            
            // Habilita "cross cutting"
            var container = new ServiceCollectionDIContainer(serviceCollection);
            
            AppDomain.CurrentDomain.DIRegistrar(container);
            
            if (customServiceAssemblies != null && customServiceAssemblies.Length > 0)
            {
                customServiceAssemblies.ToList().ForEach(assemblyName =>
                {
                    Assembly.Load(assemblyName).DIRegistrar(container);
                });
            }

            return serviceCollection;
        }

        public static IServiceCollection AddTransformationManager(
            this IServiceCollection serviceCollection)
            => serviceCollection.AddScoped<ITransformationManager, TransformationManager>();
    }
}
