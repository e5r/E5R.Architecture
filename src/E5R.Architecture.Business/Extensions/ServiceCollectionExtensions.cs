// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace E5R.Architecture.Business.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection serviceCollection)
        {
            AppDomain.CurrentDomain.AddAllBusinessFeatures(serviceCollection);

            return serviceCollection;
        }
    }
}
