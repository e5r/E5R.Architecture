// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Data.Abstractions.Alias;
using E5R.Architecture.Data.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStoragePropertyStrategy(this IServiceCollection serviceCollection)
        {
            // Objetos de armazenamento principais
            serviceCollection.AddScoped(typeof(IStorage<>), typeof(StorageByProperty<>));
            serviceCollection.AddScoped(typeof(IStorageReader<>), typeof(StorageReaderByProperty<>));
            serviceCollection.AddScoped(typeof(IStorageWriter<>), typeof(StorageWriterByProperty<>));
            serviceCollection.AddScoped(typeof(IBulkStorageWriter<>), typeof(BulkStorageWriterByProperty<>));

            serviceCollection.AddScoped(typeof(IStorage<,>), typeof(StorageByProperty<,>));
            serviceCollection.AddScoped(typeof(IStorageReader<,>), typeof(StorageReaderByProperty<,>));
            serviceCollection.AddScoped(typeof(IStorageWriter<,>), typeof(StorageWriterByProperty<,>));
            serviceCollection.AddScoped(typeof(IBulkStorageWriter<,>), typeof(BulkStorageWriterByProperty<,>));

            // Alias [Repository] dos objetos de armazenamento
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(StorageByProperty<>));
            serviceCollection.AddScoped(typeof(IRepositoryReader<>), typeof(StorageReaderByProperty<>));
            serviceCollection.AddScoped(typeof(IRepositoryWriter<>), typeof(StorageWriterByProperty<>));
            serviceCollection.AddScoped(typeof(IRepositoryBulkWriter<>), typeof(BulkStorageWriterByProperty<>));

            serviceCollection.AddScoped(typeof(IRepository<,>), typeof(StorageByProperty<,>));
            serviceCollection.AddScoped(typeof(IRepositoryReader<,>), typeof(StorageReaderByProperty<,>));
            serviceCollection.AddScoped(typeof(IRepositoryWriter<,>), typeof(StorageWriterByProperty<,>));
            serviceCollection.AddScoped(typeof(IRepositoryBulkWriter<,>), typeof(BulkStorageWriterByProperty<,>));

            // Alias [Store] dos objetos de armazenamento
            serviceCollection.AddScoped(typeof(IStore<>), typeof(StorageByProperty<>));
            serviceCollection.AddScoped(typeof(IStoreReader<>), typeof(StorageReaderByProperty<>));
            serviceCollection.AddScoped(typeof(IStoreWriter<>), typeof(StorageWriterByProperty<>));
            serviceCollection.AddScoped(typeof(IStoreBulkWriter<>), typeof(BulkStorageWriterByProperty<>));

            serviceCollection.AddScoped(typeof(IStore<,>), typeof(StorageByProperty<,>));
            serviceCollection.AddScoped(typeof(IStoreReader<,>), typeof(StorageReaderByProperty<,>));
            serviceCollection.AddScoped(typeof(IStoreWriter<,>), typeof(StorageWriterByProperty<,>));
            serviceCollection.AddScoped(typeof(IStoreBulkWriter<,>), typeof(BulkStorageWriterByProperty<,>));

            return serviceCollection;
        }
    }
}
