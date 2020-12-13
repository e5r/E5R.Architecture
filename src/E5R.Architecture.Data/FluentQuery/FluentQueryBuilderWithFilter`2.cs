// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.FluentQuery
{
    public class FluentQueryBuilderWithFilter<TDataModel, TSelect> : FluentQueryBuilderElements<TDataModel>
        where TDataModel : IDataModel
    {
        private new readonly LinqDataProjection<TDataModel, TSelect> _projection;

        internal FluentQueryBuilderWithFilter(IStorageReader<TDataModel> storage,
            LinqDataFilter<TDataModel> filter,
            LinqDataLimiter<TDataModel> limiter,
            LinqDataProjection<TDataModel, TSelect> projection)
            : base(storage, filter, limiter, projection.GetOnlyIncludeProjection())
        {
            _projection = projection;
        }

        public FluentQueryBuilderWithFilter<TDataModel, TSelect> Filter(Expression<Func<TDataModel, bool>> filter)
        {
            _filter.AddFilter(filter);

            return this;
        }

        #region Storage Actions

        public IEnumerable<TSelect> Search() => _storage.Search<TSelect>(_filter, _projection);

        #endregion
    }
}
