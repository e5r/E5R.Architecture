﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using Microsoft.Extensions.DependencyInjection;

namespace E5R.Architecture.Infrastructure.Abstractions
{
    public static class DILifetimeExtensions
    {
        public static ServiceLifetime ToServiceLifetime(this DILifetime lifetime)
        {
            switch (lifetime)
            {
                case DILifetime.Transient:
                    return ServiceLifetime.Transient;

                case DILifetime.Scoped:
                    return ServiceLifetime.Scoped;

                case DILifetime.Singleton:
                    return ServiceLifetime.Singleton;

                default:
                    throw new InvalidCastException();
            }
        }
    }
}
