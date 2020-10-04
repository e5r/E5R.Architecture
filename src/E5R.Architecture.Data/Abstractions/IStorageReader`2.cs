// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;

namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageReader<TDataModel> : IStorageSignature, ITradableObject
        where TDataModel : IDataModel
    {
        TDataModel Find(TDataModel data);
        DataLimiterResult<TDataModel> Get(DataLimiter<TDataModel> limiter);
        IEnumerable<TDataModel> Search(DataFilter<TDataModel> filter);

        DataLimiterResult<TDataModel> LimitedSearch(DataFilter<TDataModel> filter,
            DataLimiter<TDataModel> limiter);
    }
}
