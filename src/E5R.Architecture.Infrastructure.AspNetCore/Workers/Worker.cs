// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Threading;
using System.Threading.Tasks;
using E5R.Architecture.Core;

namespace E5R.Architecture.Infrastructure.AspNetCore.Workers
{
    public abstract class Worker : IWorker
    {
        protected Worker(string workerName)
        {
            Checker.NotEmptyOrWhiteArgument(workerName, nameof(workerName));

            WorkerName = workerName;
        }
        public string WorkerName { get; }
        public abstract Task<bool> DoWorkAsync(CancellationToken cancellationToken);
    }
}
