// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Data.Abstractions.Alias;
using E5R.Architecture.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore.Strategy.TransactionScope
{
    public class StorageReader<TDataModel>
        : StorageReader<DbContext, TDataModel>
        where TDataModel : class, IDataModel
    {
        public StorageReader(DbContext context)
            : base(context) { }
    }

    public class StorageReader<TDbContext, TDataModel>
        : FullStorage<TDbContext, TDataModel>,
        IStorageReader<TDbContext, TDataModel>,
        IRepositoryReader<TDbContext, TDataModel>,
        IStoreReader<TDbContext, TDataModel>
        where TDataModel : class, IDataModel
        where TDbContext : DbContext
    {
        public StorageReader(TDbContext context)
            : base(context) { }
    }
}
