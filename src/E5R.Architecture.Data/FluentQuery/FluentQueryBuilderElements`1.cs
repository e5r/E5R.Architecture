// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.FluentQuery
{
    public class FluentQueryBuilderElements<TDataModel>
        where TDataModel : IDataModel
    {
        internal readonly IStorageReader<TDataModel> _storage;
        internal readonly DataFilter<TDataModel> _filter;
        internal readonly DataLimiter<TDataModel> _limiter;
        internal readonly DataProjection<TDataModel> _projection;

        internal FluentQueryBuilderElements(
            IStorageReader<TDataModel> storage,
            DataFilter<TDataModel> filter,
            DataLimiter<TDataModel> limiter,
            DataProjection<TDataModel> projection)
        {
            Checker.NotNullArgument(storage, nameof(storage));
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(limiter, nameof(limiter));
            Checker.NotNullArgument(projection, nameof(projection));

            _storage = storage;
            _filter = filter;
            _limiter = limiter;
            _projection = projection;
        }
    }
}
