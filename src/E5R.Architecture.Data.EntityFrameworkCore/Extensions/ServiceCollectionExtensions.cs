// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Data.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStoragePropertyStrategy(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IStorageReader<>), typeof(StorageReaderByProperty<>));
            serviceCollection.AddScoped(typeof(IStorageReader<,>), typeof(StorageReaderByProperty<,>));

            return serviceCollection;
        }
    }
}
