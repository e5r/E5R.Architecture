// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.FluentQuery
{
    public class ProjectionRootBuilder<TDataModel, TSelect> : FluentQueryBuilderElements<TDataModel>
        where TDataModel : IDataModel
    {
        private readonly Expression<Func<TDataModel, TSelect>> _select;

        internal ProjectionRootBuilder(IStorageReader<TDataModel> storage,
            DataFilter<TDataModel> filter,
            DataLimiter<TDataModel> limiter,
            DataProjection<TDataModel> projection,
            Expression<Func<TDataModel, TSelect>> select)
            : base(storage, filter, limiter, projection)
        {
            Checker.NotNullArgument(select, nameof(select));

            _select = select;
        }

        public ProjectionRootBuilder<TDataModel, TSelect> Include(Expression<Func<TDataModel, object>> expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _projection.Include(expression as LambdaExpression);

            return this;
        }

        public ProjectionInnerBuilder<T, TDataModel> Include<T>(Expression<Func<TDataModel, object>> expression)
            where T : IDataModel
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _projection.Include(expression as LambdaExpression);

            return new ProjectionInnerBuilder<T, TDataModel>(_storage, _filter, _limiter, _projection);
        }

        public FluentQueryBuilderWithProjection<TDataModel, TSelect> Project()
            => new FluentQueryBuilderWithProjection<TDataModel, TSelect>(_storage, _filter, _limiter, _projection, _select);
    }
}
