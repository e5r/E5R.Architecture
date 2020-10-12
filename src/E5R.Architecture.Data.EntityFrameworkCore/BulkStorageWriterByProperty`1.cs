﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public class BulkStorageWriterByProperty<TDataModel>
        : BulkStorageWriterByProperty<DbContext, TDataModel>
        where TDataModel : class, IDataModel
    {
        public BulkStorageWriterByProperty(UnitOfWorkProperty<DbContext> context)
            : base(context) { }
    }

    public class BulkStorageWriterByProperty<TDbContext, TDataModel>
        : FullStorageByProperty<TDbContext, TDataModel>, IBulkStorageWriter<TDbContext, TDataModel>
        where TDataModel : class, IDataModel
        where TDbContext : DbContext
    {
        public BulkStorageWriterByProperty(UnitOfWorkProperty<TDbContext> context)
            : base(context) { }
    }
}