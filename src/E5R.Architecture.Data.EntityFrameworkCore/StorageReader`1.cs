// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    using Abstractions;

    public class StorageReader<TDataModel> : IStorageReader<TDataModel>
        where TDataModel : class, IDataModel
    {
        private readonly FullStorage<TDataModel> _base = new FullStorage<TDataModel>();

        protected DbSet<TDataModel> Set => _base.Set;
        protected IQueryable<TDataModel> Query => _base.Query;

        public void Configure(UnderlyingSession session) => _base.Configure(session);

        public TDataModel Find(TDataModel data) => _base.Find(data);

        public DataLimiterResult<TDataModel> Get(DataLimiter<TDataModel> limiter) => _base.Get(limiter);

        public IEnumerable<TDataModel> Search(DataReducer<TDataModel> reducer) => _base.Search(reducer);

        public DataLimiterResult<TDataModel> LimitedSearch(DataReducer<TDataModel> reducer,
            DataLimiter<TDataModel> limiter) => _base.LimitedSearch(reducer, limiter);
    }
}
