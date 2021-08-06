// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E5R.Architecture.Infrastructure.Abstractions
{
    public interface ICrossCuttingRegistrar
    {
        void Register(IServiceCollection services, IConfiguration configuration);
    }
}
