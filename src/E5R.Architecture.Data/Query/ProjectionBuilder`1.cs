// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.Query
{
    public class ProjectionBuilder<TDataModel> : QueryBuilderElements<TDataModel>
        where TDataModel : IDataModel
    {
        internal ProjectionBuilder(IStorageReader<TDataModel> storage,
            LinqDataFilter<TDataModel> filter,
            LinqDataLimiter<TDataModel> limiter,
            LinqDataProjection<TDataModel> projection)
            : base(storage, filter, limiter, projection)
        { }

        // TODO: Implementar includes
        public ProjectionBuilder<TDataModel> Include(Expression<Func<TDataModel, object>> expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _projection.Include(expression as LambdaExpression);

            return this;
        }

        public QueryBuilderWithProjection<TDataModel> Project()
            => new QueryBuilderWithProjection<TDataModel>(_storage, _filter, _limiter, _projection);

        // TODO: Implementar Project({})
    }
}
