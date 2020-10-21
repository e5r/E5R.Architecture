// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Data.Abstractions.Alias;
using E5R.Architecture.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore.Strategy.ByProperty
{
    public class StorageWriter<TDataModel>
        : StorageWriter<DbContext, TDataModel>
        where TDataModel : class, IDataModel
    {
        public StorageWriter(UnitOfWorkProperty<DbContext> context)
            : base(context) { }
    }

    public class StorageWriter<TDbContext, TDataModel>
        : FullStorage<TDbContext, TDataModel>,
        IStorageWriter<TDbContext, TDataModel>,
        IRepositoryWriter<TDbContext, TDataModel>,
        IStoreWriter<TDbContext, TDataModel>
        where TDataModel : class, IDataModel
        where TDbContext : DbContext
    {
        public StorageWriter(UnitOfWorkProperty<TDbContext> context)
            : base(context) { }
    }
}
