// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using E5R.Architecture.Core;
using E5R.Architecture.Infrastructure.AspNetCore.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWorkManager(
            this IServiceCollection services,
            Action<WorkManagerOptions> config)
        {
            Checker.NotNullArgument(config, nameof(config));

            var options = new WorkManagerOptions
            {
                DelayMinimum = 5,
                DelayMaximum = 60,
                DelayIncrement = 5
            };

            config(options);

            return AddWorkManager(services, options);
        }

        public static IServiceCollection AddWorkManager(this IServiceCollection services,
            WorkManagerOptions options)
        {
            Checker.NotNullArgument(options, nameof(options));
            
            services.TryAddSingleton(typeof(WorkManagerOptions), _ => options);

            return services;
        }

        public static IServiceCollection AddHostedWorker<TWorker>(this IServiceCollection services)
            where TWorker : IWorker
        {
            services.TryAddScoped(typeof(TWorker));

            return services.AddHostedService<WorkManager<TWorker>>();
        }
    }
}
