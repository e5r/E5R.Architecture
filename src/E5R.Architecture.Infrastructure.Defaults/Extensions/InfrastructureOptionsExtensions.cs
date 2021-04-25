// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Infrastructure;
using E5R.Architecture.Infrastructure.Defaults;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureOptionsExtensions
    {
        public static InfrastructureOptions UseDefaults(this InfrastructureOptions options)
        {
            options.FileSystemType = typeof(DefaultFileSystem);
            options.SystemClockType = typeof(DefaultSystemClock);
            
            return options;
        }
    }
}
