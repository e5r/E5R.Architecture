// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using E5R.Architecture.Core;
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
            serviceCollection.AddScoped(typeof(IStorage<,>), typeof(ByProperty.Storage<,>));
            
            // Variações de IStorageReader
            AddInBatch(serviceCollection, new[]
                {
                    typeof(IStorageReader<>),
                    typeof(IFindableStorage<>),
                    typeof(IFindableStorageWithSelector<>), 
                    typeof(ICountableStorage<>),
                    typeof(IAcquirableStorage<>), 
                    typeof(IAcquirableStorageWithGrouping<>),
                    typeof(IAcquirableStorageWithSelector<>), 
                    typeof(ISearchableStorage<>),
                    typeof(ISearchableStorageWithGrouping<>),
                    typeof(ISearchableStorageWithSelector<>)
                },
                typeof(ByProperty.StorageReader<>));

            AddInBatch(serviceCollection, new[]
                {
                    typeof(IStorageReader<,>),
                    typeof(IFindableStorage<,>),
                    typeof(IFindableStorageWithSelector<,>),
                    typeof(ICountableStorage<,>),
                    typeof(IAcquirableStorage<,>),
                    typeof(IAcquirableStorageWithGrouping<,>),
                    typeof(IAcquirableStorageWithSelector<,>),
                    typeof(ISearchableStorage<,>),
                    typeof(ISearchableStorageWithGrouping<,>),
                    typeof(ISearchableStorageWithSelector<,>),
                },
                typeof(ByProperty.StorageReader<,>));
            
            // Variações de IStorageWriter
            AddInBatch(serviceCollection, new[]
                {
                    typeof(IStorageWriter<>),
                    typeof(ICreatableStorage<>),
                    typeof(IRemovableStorage<>),
                    typeof(IReplaceableStorage<>),
                    typeof(IUpdatableStorage<>)
                },
                typeof(ByProperty.StorageWriter<>));
            
            AddInBatch(serviceCollection, new[]
                {
                    typeof(IBulkCreatableStorage<>),
                    typeof(IBulkRemovableStorage<>),
                    typeof(IBulkReplaceableStorage<>),
                    typeof(IBulkUpdatableStorage<>)
                },
                typeof(ByProperty.StorageBulkWriter<>));

            AddInBatch(serviceCollection, new[]
                {
                    typeof(IStorageWriter<,>),
                    typeof(ICreatableStorage<,>),
                    typeof(IRemovableStorage<,>),
                    typeof(IReplaceableStorage<,>),
                    typeof(IUpdatableStorage<,>)
                },
                typeof(ByProperty.StorageWriter<,>));

            AddInBatch(serviceCollection, new[]
                {
                    typeof(IBulkCreatableStorage<,>),
                    typeof(IBulkRemovableStorage<,>),
                    typeof(IBulkReplaceableStorage<,>),
                    typeof(IBulkUpdatableStorage<,>)
                },
                typeof(ByProperty.StorageBulkWriter<,>));

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
            serviceCollection.AddScoped(typeof(IStorage<,>), typeof(TransactionScope.Storage<,>));

            // Variações de IStorageReader
            AddInBatch(serviceCollection, new[]
                {
                    typeof(IStorageReader<>),
                    typeof(IFindableStorage<>),
                    typeof(IFindableStorageWithSelector<>),
                    typeof(ICountableStorage<>),
                    typeof(IAcquirableStorage<>),
                    typeof(IAcquirableStorageWithGrouping<>),
                    typeof(IAcquirableStorageWithSelector<>),
                    typeof(ISearchableStorage<>),
                    typeof(ISearchableStorageWithGrouping<>),
                    typeof(ISearchableStorageWithSelector<>)
                },
                typeof(TransactionScope.StorageReader<>));

            AddInBatch(serviceCollection, new[]
                {
                    typeof(IStorageReader<,>),
                    typeof(IFindableStorage<,>),
                    typeof(IFindableStorageWithSelector<,>),
                    typeof(ICountableStorage<,>),
                    typeof(IAcquirableStorage<,>),
                    typeof(IAcquirableStorageWithGrouping<,>),
                    typeof(IAcquirableStorageWithSelector<,>),
                    typeof(ISearchableStorage<,>),
                    typeof(ISearchableStorageWithGrouping<,>),
                    typeof(ISearchableStorageWithSelector<,>)
                },
                typeof(TransactionScope.StorageReader<,>));
            
            // Variações de IStorageWriter
            AddInBatch(serviceCollection, new[]
                {
                    typeof(IStorageWriter<>),
                    typeof(ICreatableStorage<>),
                    typeof(IRemovableStorage<>),
                    typeof(IReplaceableStorage<>),
                    typeof(IUpdatableStorage<>)
                },
                typeof(TransactionScope.StorageWriter<>));

            AddInBatch(serviceCollection, new[]
                {
                    typeof(IBulkCreatableStorage<>),
                    typeof(IBulkRemovableStorage<>),
                    typeof(IBulkReplaceableStorage<>),
                    typeof(IBulkUpdatableStorage<>)
                },
                typeof(TransactionScope.StorageBulkWriter<>));
            
            AddInBatch(serviceCollection, new[]
                {
                    typeof(IStorageWriter<,>),
                    typeof(ICreatableStorage<,>),
                    typeof(IRemovableStorage<,>),
                    typeof(IReplaceableStorage<,>),
                    typeof(IUpdatableStorage<,>)
                },
                typeof(TransactionScope.StorageWriter<,>));

            AddInBatch(serviceCollection, new[]
                {
                    typeof(IBulkCreatableStorage<,>),
                    typeof(IBulkRemovableStorage<,>),
                    typeof(IBulkReplaceableStorage<,>),
                    typeof(IBulkUpdatableStorage<,>)
                },
                typeof(TransactionScope.StorageBulkWriter<,>));
            
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
        
        private static void AddInBatch(IServiceCollection serviceCollection, Type[] serviceTypes,
            Type implementationType)
        {
            Checker.NotNullOrEmptyArgument(serviceTypes, nameof(serviceTypes));

            serviceTypes?.ToList().ForEach(s => serviceCollection.AddScoped(s, implementationType));
        }
    }
}
