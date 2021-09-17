// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.Fluent.Query
{
    public class FluentQueryBuilderElements<TDataModel>
        where TDataModel : IIdentifiable
    {
        internal readonly IStorageReader<TDataModel> _storage;
        internal readonly ExpressionDataFilter<TDataModel> _filter;
        internal readonly DataLimiter<TDataModel> _limiter;
        internal readonly DataIncludes<TDataModel> _includes;

        internal FluentQueryBuilderElements(
            IStorageReader<TDataModel> storage,
            ExpressionDataFilter<TDataModel> filter,
            DataLimiter<TDataModel> limiter,
            DataIncludes<TDataModel> includes)
        {
            Checker.NotNullArgument(storage, nameof(storage));
            Checker.NotNullArgument(filter, nameof(filter));
            Checker.NotNullArgument(limiter, nameof(limiter));
            Checker.NotNullArgument(includes, nameof(includes));

            _storage = storage;
            _filter = filter;
            _limiter = limiter;
            _includes = includes;
        }
    }
}
