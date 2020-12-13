﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.FluentQuery
{
    public class FluentQueryBuilderWithProjection<TDataModel> : FluentQueryBuilderElements<TDataModel>
        where TDataModel : IDataModel
    {
        internal FluentQueryBuilderWithProjection(IStorageReader<TDataModel> storage,
            LinqDataFilter<TDataModel> filter,
            LinqDataLimiter<TDataModel> limiter,
            LinqDataProjection<TDataModel> projection)
            : base(storage, filter, limiter, projection)
        { }

        public FluentQueryBuilderWithFilter<TDataModel> Filter(Expression<Func<TDataModel, bool>> filterExpression)
            => new FluentQueryBuilderWithFilter<TDataModel>(_storage, _filter, _limiter, _projection)
                .Filter(filterExpression);

        // TODO: Adicionar métodos de limiter

        #region Storage Actions

        public TDataModel Find(object identifier) => _storage.Find(identifier, _projection);

        public TDataModel Find(object[] identifiers) => _storage.Find(identifiers, _projection);

        public TDataModel Find(TDataModel data) => _storage.Find(data, _projection);

        #endregion
    }
}