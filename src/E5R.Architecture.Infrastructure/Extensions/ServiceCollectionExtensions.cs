// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Data;
using System.Data.Common;
using System.Linq;
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
            this IServiceCollection serviceCollection) => AddInfrastructure(serviceCollection,
            (_) => { });
        
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection serviceCollection,
            Action<InfrastructureOptions> config)
        {
            Checker.NotNullArgument(config, nameof(config));
            
            var options = new InfrastructureOptions();

            config(options);

            return AddInfrastructure(serviceCollection, options);
        }

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection serviceCollection,
            InfrastructureOptions options)
        {
            Checker.NotNullArgument(options, nameof(options));
            Checker.NotNullArgument(options.CustomServiceAssemblies,
                () => options.CustomServiceAssemblies);
            
            // Forçamos o carregamento dos assemblies informados.
            //
            // NOTE: Isso é necessário porque o otimizador de compiladores como
            //       Roslyn "removem" referências de objetos não utilizados.
            //       O efeito colateral disto é que mesmo que você tenha um projeto
            //       referenciado mas não utilize explicitamente nenhum objeto dessa
            //       referência, o assembly não estará disponível no AppDomain.
            //       Com isso, não conseguiríamos encontrar objetos para registrar
            //       aqui. Por isso forçamos o carregamos dos assemblies informados aqui.
            options.CustomServiceAssemblies.ToList().ForEach(n => AppDomain.CurrentDomain.Load(n));
            
            // IFileSystem
            if (options.FileSystemType != null)
            {
                serviceCollection.TryAddScoped(typeof(IFileSystem), options.FileSystemType);
            }
            
            // ISystemClock
            if (options.SystemClockType != null)
            {
                serviceCollection.TryAddScoped(typeof(ISystemClock), options.SystemClockType);
            }

            // Habilita "notification manager"
            serviceCollection.TryAddScoped(typeof(NotificationManager<>));
            AppDomain.CurrentDomain.AddAllNotificationDispatchers(serviceCollection);

            // Habilita "transformers"
            serviceCollection.TryAddScoped(typeof(ITransformationManager), options.TransformationManagerType);
            AppDomain.CurrentDomain.AddAllTransformers(serviceCollection);
            
            // Habilita "cross cutting" e "rule for"
            var container = new ServiceCollectionDIContainer(serviceCollection);

            serviceCollection.TryAddScoped(typeof(IRuleSet<>), typeof(RuleSet<>));

            AppDomain.CurrentDomain.DIRegistrar(container);
            AppDomain.CurrentDomain.AddAllRules(serviceCollection);

            // Habilita "lazy loading"
            serviceCollection.TryAddScoped(typeof(ILazy<>), options.LazyResolverType);
            AppDomain.CurrentDomain.AddAllLazyGroups(serviceCollection);

            return serviceCollection;
        }
    }
}
