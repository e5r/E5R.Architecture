// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    // TODO: Renomear todos os X<TUowProperty, TDataModel> para X<TUow...>
    public interface ISearchableStorage<TUowProperty, TDataModel> : ISearchableStorage<TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    public interface ISearchableStorage<TDataModel> : IStorageSignature
        where TDataModel : IIdentifiable
    {
        IEnumerable<TDataModel> Search(IDataFilter<TDataModel> filter,
            IDataIncludes includes = null);

        PaginatedResult<TDataModel> LimitedSearch(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataIncludes includes = null);
    }
}
