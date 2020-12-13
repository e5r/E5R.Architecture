﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.FluentQuery
{
    public class ProjectionInnerBuilder<TDataModel, TRootDataModel, TSelect> : FluentQueryBuilderElements<TRootDataModel>
        where TDataModel : IDataModel
        where TRootDataModel : IDataModel
    {
        private readonly Expression<Func<TRootDataModel, TSelect>> _select;

        internal ProjectionInnerBuilder(IStorageReader<TRootDataModel> storage,
            LinqDataFilter<TRootDataModel> filter,
            LinqDataLimiter<TRootDataModel> limiter,
            LinqDataProjection<TRootDataModel> projection,
            Expression<Func<TRootDataModel, TSelect>> select)
            : base(storage, filter, limiter, projection)
        {
            Checker.NotNullArgument(select, nameof(select));

            _select = select;
        }

        public ProjectionRootBuilder<TRootDataModel, TSelect> Include(Expression<Func<TRootDataModel, object>> expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _projection.Include(expression as LambdaExpression);

            return new ProjectionRootBuilder<TRootDataModel, TSelect>(_storage, _filter, _limiter, _projection, _select);
        }

        public ProjectionInnerBuilder<TDataModel, TRootDataModel, TSelect> ThenInclude(Expression<Func<TDataModel, object>> expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _projection.ThenInclude(expression as LambdaExpression);

            return this;
        }

        public ProjectionInnerBuilder<T, TRootDataModel, TSelect> ThenInclude<T>(Expression<Func<TDataModel, object>> expression)
            where T : IDataModel
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _projection.ThenInclude(expression as LambdaExpression);

            return new ProjectionInnerBuilder<T, TRootDataModel, TSelect>(_storage, _filter, _limiter, _projection, _select);
        }

        public FluentQueryBuilderWithProjection<TRootDataModel, TSelect> Project()
            => new FluentQueryBuilderWithProjection<TRootDataModel, TSelect>(_storage, _filter, _limiter, _projection, _select);
    }
}