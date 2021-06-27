// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Threading;
using System.Threading.Tasks;

namespace E5R.Architecture.Infrastructure.AspNetCore.Workers
{
    public interface IWorker
    {
        string WorkerName { get; }

        Task<bool> DoWorkAsync(CancellationToken cancellationToken);
    }
}
