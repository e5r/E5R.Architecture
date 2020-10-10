// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using E5R.Architecture.Infrastructure;
using E5R.Architecture.Infrastructure.Abstractions;
using E5R.Architecture.Infrastructure.AspNetCore;

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
    }
}
