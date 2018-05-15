// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    using Abstractions;

    public class StorageWriter<TDataModel> : IStorageWriter<TDataModel>
        where TDataModel : class, IDataModel
    {
        private readonly FullStorage<TDataModel> _base = new FullStorage<TDataModel>();

        public DbSet<TDataModel> Set => _base.Set;
        protected WriterDelegate Write => _base.Write;

        public void Configure(UnderlyingSession session) => _base.Configure(session);

        public TDataModel Create(TDataModel data) => _base.Create(data);

        public TDataModel Replace(TDataModel data) => _base.Replace(data);

        public void Remove(TDataModel data) => _base.Remove(data);
    }
}
