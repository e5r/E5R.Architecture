// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public class RawSqlRideStorage<TDataModel> : RideStorage<TDataModel>
        where TDataModel : class, IDataModel
    {
        private readonly DbSet<TDataModel> _set;

        public RawSqlRideStorage(DbContext context, string sql, params object[] parameters)
            : base(context.Set<TDataModel>().FromSqlRaw(sql, parameters))
        { }
    }
}
