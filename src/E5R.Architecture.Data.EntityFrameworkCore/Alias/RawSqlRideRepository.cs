// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using Microsoft.EntityFrameworkCore;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.EntityFrameworkCore.Alias
{
    public class RawSqlRideRepository<TDataModel> : RawSqlRideStorage<TDataModel>
        where TDataModel : class, IDataModel
    {
        public RawSqlRideRepository(DbContext context, string sql, params object[] parameters)
            : base(context, sql, parameters)
        { }

        public RawSqlRideRepository(DbContext context, FormattableString sql)
            : base(context, sql)
        { }
    }
}
