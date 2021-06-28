// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Data.Abstractions.Alias;
using ByProperty = E5R.Architecture.Data.EntityFrameworkCore.Strategy.ByProperty;
using TransactionScope = E5R.Architecture.Data.EntityFrameworkCore.Strategy.TransactionScope;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStoragePropertyStrategy(this IServiceCollection serviceCollection)
        {
            // Objetos de armazenamento principais
            serviceCollection.AddScoped(typeof(IStorage<>), typeof(ByProperty.Storage<>));
            serviceCollection.AddScoped(typeof(IStorageReader<>), typeof(ByProperty.StorageReader<>));
            serviceCollection.AddScoped(typeof(IFindableStorage<>), typeof(ByProperty.StorageReader<>));
            serviceCollection.AddScoped(typeof(ICountableStorage<>), typeof(ByProperty.StorageReader<>));
            serviceCollection.AddScoped(typeof(IStorageTransportable<>), typeof(ByProperty.StorageReader<>));
            serviceCollection.AddScoped(typeof(ISearchableStorage<>), typeof(ByProperty.StorageReader<>));
            serviceCollection.AddScoped(typeof(IStorageWriter<>), typeof(ByProperty.StorageWriter<>));
            serviceCollection.AddScoped(typeof(IStorageBulkWriter<>), typeof(ByProperty.StorageBulkWriter<>));

            serviceCollection.AddScoped(typeof(IStorage<,>), typeof(ByProperty.Storage<,>));
            serviceCollection.AddScoped(typeof(IStorageReader<,>), typeof(ByProperty.StorageReader<,>));
            serviceCollection.AddScoped(typeof(IFindableStorage<,>), typeof(ByProperty.StorageReader<,>));
            serviceCollection.AddScoped(typeof(ICountableStorage<,>), typeof(ByProperty.StorageReader<,>));
            serviceCollection.AddScoped(typeof(IStorageTransportable<,>), typeof(ByProperty.StorageReader<,>));
            serviceCollection.AddScoped(typeof(ISearchableStorage<,>), typeof(ByProperty.StorageReader<,>));
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

        public static IServiceCollection AddStorageTransactionScopeStrategy(this IServiceCollection serviceCollection)
        {
            // Objetos de armazenamento principais
            serviceCollection.AddScoped(typeof(IStorage<>), typeof(TransactionScope.Storage<>));
            serviceCollection.AddScoped(typeof(IStorageReader<>), typeof(TransactionScope.StorageReader<>));
            serviceCollection.AddScoped(typeof(IFindableStorage<>), typeof(TransactionScope.StorageReader<>));
            serviceCollection.AddScoped(typeof(ICountableStorage<>), typeof(TransactionScope.StorageReader<>));
            serviceCollection.AddScoped(typeof(IStorageTransportable<>), typeof(TransactionScope.StorageReader<>));
            serviceCollection.AddScoped(typeof(ISearchableStorage<>), typeof(TransactionScope.StorageReader<>));
            serviceCollection.AddScoped(typeof(IStorageWriter<>), typeof(TransactionScope.StorageWriter<>));
            serviceCollection.AddScoped(typeof(IStorageBulkWriter<>), typeof(TransactionScope.StorageBulkWriter<>));

            serviceCollection.AddScoped(typeof(IStorage<,>), typeof(TransactionScope.Storage<,>));
            serviceCollection.AddScoped(typeof(IStorageReader<,>), typeof(TransactionScope.StorageReader<,>));
            serviceCollection.AddScoped(typeof(IFindableStorage<,>), typeof(TransactionScope.StorageReader<,>));
            serviceCollection.AddScoped(typeof(ICountableStorage<,>), typeof(TransactionScope.StorageReader<,>));
            serviceCollection.AddScoped(typeof(IStorageTransportable<,>), typeof(TransactionScope.StorageReader<,>));
            serviceCollection.AddScoped(typeof(ISearchableStorage<,>), typeof(TransactionScope.StorageReader<,>));
            serviceCollection.AddScoped(typeof(IStorageWriter<,>), typeof(TransactionScope.StorageWriter<,>));
            serviceCollection.AddScoped(typeof(IStorageBulkWriter<,>), typeof(TransactionScope.StorageBulkWriter<,>));

            // Alias [Repository] dos objetos de armazenamento
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(TransactionScope.Storage<>));
            serviceCollection.AddScoped(typeof(IRepositoryReader<>), typeof(TransactionScope.StorageReader<>));
            serviceCollection.AddScoped(typeof(IRepositoryWriter<>), typeof(TransactionScope.StorageWriter<>));
            serviceCollection.AddScoped(typeof(IRepositoryBulkWriter<>), typeof(TransactionScope.StorageBulkWriter<>));

            serviceCollection.AddScoped(typeof(IRepository<,>), typeof(TransactionScope.Storage<,>));
            serviceCollection.AddScoped(typeof(IRepositoryReader<,>), typeof(TransactionScope.StorageReader<,>));
            serviceCollection.AddScoped(typeof(IRepositoryWriter<,>), typeof(TransactionScope.StorageWriter<,>));
            serviceCollection.AddScoped(typeof(IRepositoryBulkWriter<,>), typeof(TransactionScope.StorageBulkWriter<,>));

            // Alias [Store] dos objetos de armazenamento
            serviceCollection.AddScoped(typeof(IStore<>), typeof(TransactionScope.Storage<>));
            serviceCollection.AddScoped(typeof(IStoreReader<>), typeof(TransactionScope.StorageReader<>));
            serviceCollection.AddScoped(typeof(IStoreWriter<>), typeof(TransactionScope.StorageWriter<>));
            serviceCollection.AddScoped(typeof(IStoreBulkWriter<>), typeof(TransactionScope.StorageBulkWriter<>));

            serviceCollection.AddScoped(typeof(IStore<,>), typeof(TransactionScope.Storage<,>));
            serviceCollection.AddScoped(typeof(IStoreReader<,>), typeof(TransactionScope.StorageReader<,>));
            serviceCollection.AddScoped(typeof(IStoreWriter<,>), typeof(TransactionScope.StorageWriter<,>));
            serviceCollection.AddScoped(typeof(IStoreBulkWriter<,>), typeof(TransactionScope.StorageBulkWriter<,>));

            return serviceCollection;
        }
    }
}
