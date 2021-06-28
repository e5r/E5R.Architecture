// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    public interface
        IStorageReader<TUowProperty, TDataModel> : IFindableStorage<TUowProperty, TDataModel>,
            ICountableStorage<TUowProperty, TDataModel>,
            IStorageTransportable<TUowProperty, TDataModel>,
            IStorageSearchable<TUowProperty, TDataModel>,
            IStorageReader<TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    public interface IStorageReader<TDataModel> : IFindableStorage<TDataModel>,
        ICountableStorage<TDataModel>, IStorageTransportable<TDataModel>,
        IStorageSearchable<TDataModel>
        where TDataModel : IIdentifiable
    {
        #region TSelect operations

        TSelect Find<TSelect>(object identifier, IDataProjection<TDataModel, TSelect> projection);

        TSelect Find<TSelect>(object[] identifiers,
            IDataProjection<TDataModel, TSelect> projection);

        TSelect Find<TSelect>(TDataModel data, IDataProjection<TDataModel, TSelect> projection);
        IEnumerable<TSelect> GetAll<TSelect>(IDataProjection<TDataModel, TSelect> projection);

        PaginatedResult<TSelect> LimitedGet<TSelect>(IDataLimiter<TDataModel> limiter,
            IDataProjection<TDataModel, TSelect> projection);

        IEnumerable<TSelect> Search<TSelect>(IDataFilter<TDataModel> filter,
            IDataProjection<TDataModel, TSelect> projection);

        PaginatedResult<TSelect> LimitedSearch<TSelect>(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter, IDataProjection<TDataModel, TSelect> projection);

        #endregion

        #region TGroup operations

        IEnumerable<TSelect> GetAll<TGroup, TSelect>(
            IDataProjection<TDataModel, TGroup, TSelect> projection);

        PaginatedResult<TSelect> LimitedGet<TGroup, TSelect>(IDataLimiter<TDataModel> limiter,
            IDataProjection<TDataModel, TGroup, TSelect> projection);

        IEnumerable<TSelect> Search<TGroup, TSelect>(IDataFilter<TDataModel> filter,
            IDataProjection<TDataModel, TGroup, TSelect> projection);

        PaginatedResult<TSelect> LimitedSearch<TGroup, TSelect>(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter,
            IDataProjection<TDataModel, TGroup, TSelect> projection);

        #endregion
    }
}
