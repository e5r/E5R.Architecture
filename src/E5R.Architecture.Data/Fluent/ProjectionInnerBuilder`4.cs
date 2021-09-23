// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Data.Fluent.Query;

namespace E5R.Architecture.Data.Fluent
{
    public class ProjectionInnerBuilder<TDataModel, TRootDataModel, TGroup, TSelect> : FluentQueryBuilderElements<TRootDataModel>
        where TDataModel : IIdentifiable
        where TRootDataModel : IIdentifiable
    {
        private readonly Expression<Func<TRootDataModel, TGroup>> _group;
        private readonly Expression<Func<IGrouping<TGroup, TRootDataModel>, TSelect>> _select;

        internal ProjectionInnerBuilder(IStorageReader<TRootDataModel> storage,
            DataFilter<TRootDataModel> filter,
            DataLimiter<TRootDataModel> limiter,
            DataIncludes<TRootDataModel> includes,
            Expression<Func<TRootDataModel, TGroup>> group,
            Expression<Func<IGrouping<TGroup, TRootDataModel>, TSelect>> select)
            : base(storage, filter, limiter, includes)
        {
            Checker.NotNullArgument(group, nameof(group));
            Checker.NotNullArgument(select, nameof(select));

            _group = group;
            _select = select;
        }

        public ProjectionRootBuilder<TRootDataModel, TGroup, TSelect> Include(Expression<Func<TRootDataModel, object>> expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _includes.Include(expression as LambdaExpression);

            return new ProjectionRootBuilder<TRootDataModel, TGroup, TSelect>(_storage, _filter, _limiter, _includes, _group, _select);
        }

        public ProjectionInnerBuilder<TDataModel, TRootDataModel, TGroup, TSelect> ThenInclude(Expression<Func<TDataModel, object>> expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _includes.ThenInclude(expression as LambdaExpression);

            return this;
        }

        public ProjectionInnerBuilder<T, TRootDataModel, TGroup, TSelect> ThenInclude<T>(Expression<Func<TDataModel, object>> expression)
            where T : IIdentifiable
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _includes.ThenInclude(expression as LambdaExpression);

            return new ProjectionInnerBuilder<T, TRootDataModel, TGroup, TSelect>(_storage, _filter, _limiter, _includes, _group, _select);
        }

        public FluentQueryBuilderWithProjection<TRootDataModel, TGroup, TSelect> Project()
            => new FluentQueryBuilderWithProjection<TRootDataModel, TGroup, TSelect>(_storage, _filter, _limiter, _includes, _group, _select);
    }
}
