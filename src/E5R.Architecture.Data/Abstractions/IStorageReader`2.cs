// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageReader<TUowProperty, TDataModel> : IStorageReader<TDataModel>
        where TDataModel : IDataModel
    { }

    public interface IStorageReader<TDataModel> : IStorageSignature
        where TDataModel : IDataModel
    {
        #region TDataModel operations

        TDataModel Find(object identifier, IDataProjection projection = null);
        TDataModel Find(object[] identifiers, IDataProjection projection = null);
        TDataModel Find(TDataModel data, IDataProjection projection = null);
        IEnumerable<TDataModel> GetAll(IDataProjection projection = null);
        PaginatedResult<TDataModel> LimitedGet(IDataLimiter<TDataModel> limiter, IDataProjection projection = null);
        IEnumerable<TDataModel> Search(IDataFilter<TDataModel> filter, IDataProjection projection = null);
        PaginatedResult<TDataModel> LimitedSearch(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataProjection projection = null);

        #endregion

        #region TSelect operations

        TSelect Find<TSelect>(object identifier, IDataProjection<TDataModel, TSelect> projection);
        TSelect Find<TSelect>(object[] identifiers, IDataProjection<TDataModel, TSelect> projection);
        TSelect Find<TSelect>(TDataModel data, IDataProjection<TDataModel, TSelect> projection);
        IEnumerable<TSelect> GetAll<TSelect>(IDataProjection<TDataModel, TSelect> projection);
        PaginatedResult<TSelect> LimitedGet<TSelect>(IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel, TSelect> projection);
        IEnumerable<TSelect> Search<TSelect>(IDataFilter<TDataModel> filter, IDataProjection<TDataModel, TSelect> projection);
        PaginatedResult<TSelect> LimitedSearch<TSelect>(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel, TSelect> projection);

        #endregion
    }
}
