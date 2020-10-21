// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Data.Abstractions.Alias;
using ByProperty = E5R.Architecture.Data.EntityFrameworkCore.Strategy.ByProperty;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStoragePropertyStrategy(this IServiceCollection serviceCollection)
        {
            // Objetos de armazenamento principais
            serviceCollection.AddScoped(typeof(IStorage<>), typeof(ByProperty.Storage<>));
            serviceCollection.AddScoped(typeof(IStorageReader<>), typeof(ByProperty.StorageReader<>));
            serviceCollection.AddScoped(typeof(IStorageWriter<>), typeof(ByProperty.StorageWriter<>));
            serviceCollection.AddScoped(typeof(IStorageBulkWriter<>), typeof(ByProperty.StorageBulkWriter<>));

            serviceCollection.AddScoped(typeof(IStorage<,>), typeof(ByProperty.Storage<,>));
            serviceCollection.AddScoped(typeof(IStorageReader<,>), typeof(ByProperty.StorageReader<,>));
            serviceCollection.AddScoped(typeof(IStorageWriter<,>), typeof(ByProperty.StorageWriter<,>));
            serviceCollection.AddScoped(typeof(IStorageBulkWriter<,>), typeof(ByProperty.StorageBulkWriter<,>));

            // Alias [Repository] dos objetos de armazenamento
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(ByProperty.Storage<>));
            serviceCollection.AddScoped(typeof(IRepositoryReader<>), typeof(ByProperty.StorageReader<>));
            serviceCollection.AddScoped(typeof(IRepositoryWriter<>), typeof(ByProperty.StorageWriter<>));
            serviceCollection.AddScoped(typeof(IRepositoryBulkWriter<>), typeof(ByProperty.StorageBulkWriter<>));

            serviceCollection.AddScoped(typeof(IRepository<,>), typeof(ByProperty.Storage<,>));
            serviceCollection.AddScoped(typeof(IRepositoryReader<,>), typeof(ByProperty.StorageReader<,>));
            serviceCollection.AddScoped(typeof(IRepositoryWriter<,>), typeof(ByProperty.StorageWriter<,>));
            serviceCollection.AddScoped(typeof(IRepositoryBulkWriter<,>), typeof(ByProperty.StorageBulkWriter<,>));

            // Alias [Store] dos objetos de armazenamento
            serviceCollection.AddScoped(typeof(IStore<>), typeof(ByProperty.Storage<>));
            serviceCollection.AddScoped(typeof(IStoreReader<>), typeof(ByProperty.StorageReader<>));
            serviceCollection.AddScoped(typeof(IStoreWriter<>), typeof(ByProperty.StorageWriter<>));
            serviceCollection.AddScoped(typeof(IStoreBulkWriter<>), typeof(ByProperty.StorageBulkWriter<>));

            serviceCollection.AddScoped(typeof(IStore<,>), typeof(ByProperty.Storage<,>));
            serviceCollection.AddScoped(typeof(IStoreReader<,>), typeof(ByProperty.StorageReader<,>));
            serviceCollection.AddScoped(typeof(IStoreWriter<,>), typeof(ByProperty.StorageWriter<,>));
            serviceCollection.AddScoped(typeof(IStoreBulkWriter<,>), typeof(ByProperty.StorageBulkWriter<,>));

            return serviceCollection;
        }
    }
}
