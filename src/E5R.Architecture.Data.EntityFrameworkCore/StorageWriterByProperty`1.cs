// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    using Abstractions;
    using Infrastructure;

    public class StorageWriterByProperty<TDataModel>
        : StorageReaderByProperty<DbContext, TDataModel>
        where TDataModel : class, IDataModel
    {
        public StorageWriterByProperty(UnitOfWorkProperty<DbContext> context)
            : base(context) { }
    }

    public class StorageWriterByProperty<TDbContext, TDataModel>
        : FullStorageByProperty<TDbContext, TDataModel>, IStorageWriter<TDataModel>
        where TDataModel : class, IDataModel
        where TDbContext : DbContext
    {
        public StorageWriterByProperty(UnitOfWorkProperty<TDbContext> context)
            : base(context) { }
    }
}
