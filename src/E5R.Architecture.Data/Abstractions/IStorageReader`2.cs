// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;

namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageReader<TUowProperty, TDataModel> : IStorageReader<TDataModel>
        where TDataModel : IDataModel
    { }

    public interface IStorageReader<TDataModel> : IStorageSignature
        where TDataModel : IDataModel
    {
        TDataModel Find(TDataModel data);
        DataLimiterResult<TDataModel> Get(IDataLimiter<TDataModel> limiter);
        IEnumerable<TDataModel> Search(IDataFilter<TDataModel> filter);

        DataLimiterResult<TDataModel> LimitedSearch(IDataFilter<TDataModel> filter,
            IDataLimiter<TDataModel> limiter);
    }
}
