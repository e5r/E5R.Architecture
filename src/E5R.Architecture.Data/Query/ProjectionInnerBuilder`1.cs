// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.Query
{
    public class ProjectionInnerBuilder<TDataModel, TRootDataModel> : QueryBuilderElements<TRootDataModel>
        where TDataModel : IDataModel
        where TRootDataModel : IDataModel
    {
        internal ProjectionInnerBuilder(IStorageReader<TRootDataModel> storage,
            LinqDataFilter<TRootDataModel> filter,
            LinqDataLimiter<TRootDataModel> limiter,
            LinqDataProjection<TRootDataModel> projection)
            : base(storage, filter, limiter, projection)
        { }

        public ProjectionRootBuilder<TRootDataModel> Include(Expression<Func<TRootDataModel, object>> expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _projection.Include(expression as LambdaExpression);

            return new ProjectionRootBuilder<TRootDataModel>(_storage, _filter, _limiter, _projection);
        }

        public ProjectionInnerBuilder<TDataModel, TRootDataModel> ThenInclude(Expression<Func<TDataModel, object>> expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _projection.ThenInclude(expression as LambdaExpression);

            return this;
        }

        public ProjectionInnerBuilder<T, TRootDataModel> ThenInclude<T>(Expression<Func<TDataModel, object>> expression)
            where T : IDataModel
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _projection.ThenInclude(expression as LambdaExpression);

            return new ProjectionInnerBuilder<T, TRootDataModel>(_storage, _filter, _limiter, _projection);
        }

        public QueryBuilderWithProjection<TRootDataModel> Project()
            => new QueryBuilderWithProjection<TRootDataModel>(_storage, _filter, _limiter, _projection);

        // TODO: Implementar Project({})
    }
}
