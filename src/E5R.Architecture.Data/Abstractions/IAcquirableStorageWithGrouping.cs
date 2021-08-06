// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    // TODO: Renomear todos os X<TUowProperty, TDataModel> para X<TUow...>
    public interface
        IAcquirableStorageWithGrouping<TUowProperty, TDataModel> : IAcquirableStorageWithGrouping<
            TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    public interface IAcquirableStorageWithGrouping<TDataModel> : IStorageSignature
        where TDataModel : IIdentifiable
    {
        IEnumerable<TSelect> GetAll<TGroup, TSelect>(
            IDataProjection<TDataModel, TGroup, TSelect> projection);

        PaginatedResult<TSelect> LimitedGet<TGroup, TSelect>(IDataLimiter<TDataModel> limiter,
            IDataProjection<TDataModel, TGroup, TSelect> projection);
    }
}
