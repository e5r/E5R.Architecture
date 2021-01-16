// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Data.Fluent.Query;

namespace E5R.Architecture.Data.Fluent
{
    public class ProjectionInnerBuilder<TDataModel, TRootDataModel, TSelect> : FluentQueryBuilderElements<TRootDataModel>
        where TDataModel : IDataModel
        where TRootDataModel : IDataModel
    {
        private readonly Expression<Func<TRootDataModel, TSelect>> _select;

        internal ProjectionInnerBuilder(IStorageReader<TRootDataModel> storage,
            DataFilter<TRootDataModel> filter,
            DataLimiter<TRootDataModel> limiter,
            DataIncludes<TRootDataModel> includes,
            Expression<Func<TRootDataModel, TSelect>> select)
            : base(storage, filter, limiter, includes)
        {
            Checker.NotNullArgument(select, nameof(select));

            _select = select;
        }

        public ProjectionRootBuilder<TRootDataModel, TSelect> Include(Expression<Func<TRootDataModel, object>> expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _includes.Include(expression as LambdaExpression);

            return new ProjectionRootBuilder<TRootDataModel, TSelect>(_storage, _filter, _limiter, _includes, _select);
        }

        public ProjectionInnerBuilder<TDataModel, TRootDataModel, TSelect> ThenInclude(Expression<Func<TDataModel, object>> expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _includes.ThenInclude(expression as LambdaExpression);

            return this;
        }

        public ProjectionInnerBuilder<T, TRootDataModel, TSelect> ThenInclude<T>(Expression<Func<TDataModel, object>> expression)
            where T : IDataModel
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _includes.ThenInclude(expression as LambdaExpression);

            return new ProjectionInnerBuilder<T, TRootDataModel, TSelect>(_storage, _filter, _limiter, _includes, _select);
        }

        public FluentQueryBuilderWithProjection<TRootDataModel, TSelect> Project()
            => new FluentQueryBuilderWithProjection<TRootDataModel, TSelect>(_storage, _filter, _limiter, _includes, _select);
    }
}
