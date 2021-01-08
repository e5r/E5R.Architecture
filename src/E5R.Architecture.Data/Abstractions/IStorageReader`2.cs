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

        TDataModel Find(object identifier, IDataIncludes includes = null);
        TDataModel Find(object[] identifiers, IDataIncludes includes = null);
        TDataModel Find(TDataModel data, IDataIncludes includes = null);
        IEnumerable<TDataModel> GetAll(IDataIncludes includes = null);
        PaginatedResult<TDataModel> LimitedGet(IDataLimiter<TDataModel> limiter, IDataIncludes includes = null);
        IEnumerable<TDataModel> Search(IDataFilter<TDataModel> filter, IDataIncludes includes = null);
        PaginatedResult<TDataModel> LimitedSearch(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataIncludes includes = null);

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

        #region TGroup operations

        IEnumerable<TSelect> GetAll<TGroup, TSelect>(IDataProjection<TDataModel, TGroup, TSelect> projection);
        PaginatedResult<TSelect> LimitedGet<TGroup, TSelect>(IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel, TGroup, TSelect> projection);
        IEnumerable<TSelect> Search<TGroup, TSelect>(IDataFilter<TDataModel> filter, IDataProjection<TDataModel, TGroup, TSelect> projection);
        PaginatedResult<TSelect> LimitedSearch<TGroup, TSelect>(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel, TGroup, TSelect> projection);

        #endregion
    }
}
