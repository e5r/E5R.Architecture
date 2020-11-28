// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data
{
    public class LinqStorageProjectionActionsQueryBuilder<TDataModel, TSelect>
        where TDataModel : IDataModel
    {
        private readonly LinqStorageQueryBuilder<TDataModel> _rootBuilder;
        private readonly LinqDataProjection<TDataModel, TSelect> _projection;

        public LinqStorageProjectionActionsQueryBuilder(LinqStorageQueryBuilder<TDataModel> rootBuilder, Expression<Func<TDataModel, TSelect>> select)
        {
            Checker.NotNullArgument(rootBuilder, nameof(rootBuilder));
            Checker.NotNullArgument(select, nameof(select));

            _rootBuilder = rootBuilder;
            _projection = new LinqDataProjection<TDataModel, TSelect>(_rootBuilder.Projection._includes, select);
        }

        #region Actions
        public TSelect Find(object identifier) => _rootBuilder._storage.Find<TSelect>(identifier, _projection);

        public TSelect Find(object[] identifiers) => _rootBuilder._storage.Find<TSelect>(identifiers, _projection);

        public TSelect Find(TDataModel data) => _rootBuilder._storage.Find<TSelect>(data, _projection);

        public PaginatedResult<TSelect> Get() => _rootBuilder._storage.Get<TSelect>(_rootBuilder.Limiter, _projection);

        public IEnumerable<TSelect> Search() => _rootBuilder._storage.Search<TSelect>(_rootBuilder.Filter, _projection);

        public PaginatedResult<TSelect> LimitedSearch() => _rootBuilder._storage.LimitedSearch<TSelect>(_rootBuilder.Filter, _rootBuilder.Limiter, _projection);
        #endregion
    }
}
