﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Data.Abstractions.Alias;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore.Strategy.TransactionScope
{
    public class StorageBulkWriter<TDataModel>
        : StorageBulkWriter<DbContext, TDataModel>
        where TDataModel : class, IIdentifiable
    {
        public StorageBulkWriter(DbContext context)
            : base(context) { }
    }

    public class StorageBulkWriter<TDbContext, TDataModel>
        : FullStorage<TDbContext, TDataModel>,
        IStorageBulkWriter<TDbContext, TDataModel>,
        IRepositoryBulkWriter<TDbContext, TDataModel>,
        IStoreBulkWriter<TDbContext, TDataModel>
        where TDataModel : class, IIdentifiable
        where TDbContext : DbContext
    {
        public StorageBulkWriter(TDbContext context)
            : base(context) { }
    }
}
