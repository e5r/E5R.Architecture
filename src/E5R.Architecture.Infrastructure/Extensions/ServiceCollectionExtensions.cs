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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

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
            this IServiceCollection serviceCollection, IConfiguration configuration) =>
            AddInfrastructure(serviceCollection, configuration, (_) => { });

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection serviceCollection, IConfiguration configuration,
            Action<InfrastructureOptions> optionsHandler)
        {
            Checker.NotNullArgument(configuration, nameof(configuration));
            Checker.NotNullArgument(optionsHandler, nameof(optionsHandler));

            var options = new InfrastructureOptions();

            optionsHandler(options);

            return AddInfrastructure(serviceCollection, configuration, options);
        }
        
        public static IServiceCollection AddInfrastructureWithoutAutoload(
            this IServiceCollection serviceCollection, IConfiguration configuration,
            Action<InfrastructureOptions> optionsHandler)
        {
            Checker.NotNullArgument(configuration, nameof(configuration));
            Checker.NotNullArgument(optionsHandler, nameof(optionsHandler));

            var options = new InfrastructureOptions
            {
                RegisterRulesAutomatically = false,
                RegisterTransformersAutomatically = false,
                RegisterCrossCuttingAutomatically = false,
                RegisterNotificationDispatchersAutomatically = false,
                RegisterRuleModelValidatorAutomatically = false
            };

            optionsHandler(options);
            
            return AddInfrastructure(serviceCollection, configuration, options);
        }

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection serviceCollection, IConfiguration configuration,
            InfrastructureOptions options)
        {
            Checker.NotNullArgument(configuration, nameof(configuration));
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

            if (options.FileSystemType != null)
            {
                serviceCollection.TryAddScoped(typeof(IFileSystem), options.FileSystemType);
            }

            if (options.SystemClockType != null)
            {
                serviceCollection.TryAddScoped(typeof(ISystemClock), options.SystemClockType);
            }

            if (options.RuleModelValidatorType != null)
            {
                serviceCollection.TryAddScoped(typeof(IRuleModelValidator),
                    options.RuleModelValidatorType);
            }

            serviceCollection.TryAddScoped(typeof(NotificationManager<>));
            
            if (options.RegisterNotificationDispatchersAutomatically)
            {
                AppDomain.CurrentDomain.AddAllNotificationDispatchers(serviceCollection);
            }

            if (options.TransformationManagerType != null)
            {
                serviceCollection.TryAddScoped(typeof(ITransformationManager),
                    options.TransformationManagerType);
            }

            if (options.RegisterTransformersAutomatically)
            {
                AppDomain.CurrentDomain.AddAllTransformers(serviceCollection);
            }

            serviceCollection.TryAddScoped(typeof(IRuleSet<>), typeof(RuleSet<>));

            if (options.RegisterCrossCuttingAutomatically)
            {
                AppDomain.CurrentDomain.AddCrossCuttingRegistrar(serviceCollection, configuration);
            }

            if (options.RegisterRulesAutomatically)
            {
                AppDomain.CurrentDomain.AddAllRules(serviceCollection);
            }

            serviceCollection.TryAddScoped(typeof(ILazy<>), options.LazyResolverType);

            return serviceCollection;
        }

        public static IServiceCollection AddSettings<TSetting>(
            this IServiceCollection services, ServiceLifetime lifetime,
            IConfiguration configuration, string settingKey)
            where TSetting : class, new()
        {
            Checker.NotNullArgument(services, nameof(services));
            Checker.NotNullArgument(configuration, nameof(configuration));
            Checker.NotEmptyOrWhiteArgument(settingKey, nameof(settingKey));

            services.Configure<TSetting>(configuration.GetSection(settingKey));

            var descriptor = new ServiceDescriptor(typeof(TSetting),
                provider => provider.GetService<IOptions<TSetting>>()?.Value ?? new TSetting(),
                lifetime);

            services.TryAdd(descriptor);

            return services;
        }

        public static IServiceCollection AddTransientSettings<TSetting>(
            this IServiceCollection services, IConfiguration configuration, string settingKey)
            where TSetting : class, new() => AddSettings<TSetting>(services,
            ServiceLifetime.Transient, configuration, settingKey);

        public static IServiceCollection AddScopedSettings<TSetting>(
            this IServiceCollection services, IConfiguration configuration, string settingKey)
            where TSetting : class, new() => AddSettings<TSetting>(services, ServiceLifetime.Scoped,
            configuration, settingKey);

        public static IServiceCollection AddSingletonSettings<TSetting>(
            this IServiceCollection services, IConfiguration configuration, string settingKey)
            where TSetting : class, new() => AddSettings<TSetting>(services,
            ServiceLifetime.Singleton, configuration, settingKey);
    }
}
