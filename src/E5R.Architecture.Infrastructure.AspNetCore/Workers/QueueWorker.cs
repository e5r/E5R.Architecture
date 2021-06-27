// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Threading;
using System.Threading.Tasks;
using E5R.Architecture.Core;
using Microsoft.Extensions.Logging;

namespace E5R.Architecture.Infrastructure.AspNetCore.Workers
{
    public abstract class QueueWorker<TQueueObject> : Worker where TQueueObject : class
    {
        protected QueueWorker(ILogger logger, string workerName) : base(workerName)
        {
            Checker.NotNullArgument(logger, nameof(logger));

            Logger = logger;
        }

        protected ILogger Logger { get; }

        protected abstract Task<TQueueObject> PopAsync(CancellationToken cancellationToken);

        protected abstract Task<bool> DoWorkAsync(TQueueObject @object,
            CancellationToken cancellationToken);

        public override async Task<bool> DoWorkAsync(CancellationToken cancellationToken)
        {
            var @object = await PopAsync(cancellationToken);

            if (@object != null)
            {
                return await DoWorkAsync(@object, cancellationToken);
            }

            // TODO: Implementar i18n/l10n
            Logger.LogTrace("No objects in queue");

            return false;
        }
    }
}
