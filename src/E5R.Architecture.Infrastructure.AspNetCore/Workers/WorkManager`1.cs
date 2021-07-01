// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using E5R.Architecture.Core;
using E5R.Architecture.Infrastructure.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace E5R.Architecture.Infrastructure.AspNetCore.Workers
{
    public class WorkManager<TWorker> : BackgroundService where TWorker:IWorker
    {
        private ILogger<WorkManager<TWorker>> Logger { get; }
        private IServiceProvider ServiceProvider { get; }
        private DynamicDelay Delay { get; }

        public WorkManager(ILogger<WorkManager<TWorker>> logger, IServiceProvider serviceProvider, WorkManagerOptions options)
        {
            Checker.NotNullArgument(logger, nameof(logger));
            Checker.NotNullArgument(serviceProvider, nameof(serviceProvider));
            
            Logger = logger;
            ServiceProvider = serviceProvider;
            Delay = new DynamicDelay(min: options.DelayMinimum, max: options.DelayMaximum,
                increment: options.DelayIncrement);
        }
        
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Type workerType =
                typeof(WorkManager<TWorker>).GenericTypeArguments?.FirstOrDefault() ??
                typeof(WorkManager<TWorker>);

            cancellationToken.Register(() =>
            {
                // TODO: Implementar i18n/l10n
                Logger.LogTrace(
                    $"Cancellation request for WorkManager(for: {workerType.Name}) detected");
            });

            // TODO: Implementar i18n/l10n
            Logger.LogTrace($"WorkManager(for: {workerType.Name}) started execution");
            
            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = ServiceProvider.CreateScope())
                {
                    var worker = scope.ServiceProvider.GetRequiredService<TWorker>();
                    var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    try
                    {
                        if (await worker.DoWorkAsync(cancellationToken))
                        {
                            Delay.Reset();
                        }
                        else
                        {
                            await Delay.Wait();
                        }
                        
                        uow.CommitWork();

                        continue;
                    }
                    catch (Exception e)
                    {
                        // TODO: Implementar i18n/l10n
                        Logger.LogError(e,
                            $"An unexpected error occurred while executing worker {worker.WorkerName}");

                        await Delay.WaitMinimum();
                    }
                }
            }
        }
    }
}
