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
    public class ProjectionInnerBuilder<TDataModel, TRootDataModel> : FluentQueryBuilderElements<TRootDataModel>
        where TDataModel : IDataModel
        where TRootDataModel : IDataModel
    {
        internal ProjectionInnerBuilder(IStorageReader<TRootDataModel> storage,
            DataFilter<TRootDataModel> filter,
            DataLimiter<TRootDataModel> limiter,
            DataIncludes<TRootDataModel> includes)
            : base(storage, filter, limiter, includes)
        { }

        public ProjectionRootBuilder<TRootDataModel> Include(Expression<Func<TRootDataModel, object>> expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _includes.Include(expression as LambdaExpression);

            return new ProjectionRootBuilder<TRootDataModel>(_storage, _filter, _limiter, _includes);
        }

        public ProjectionInnerBuilder<TDataModel, TRootDataModel> ThenInclude(Expression<Func<TDataModel, object>> expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _includes.ThenInclude(expression as LambdaExpression);

            return this;
        }

        public ProjectionInnerBuilder<T, TRootDataModel> ThenInclude<T>(Expression<Func<TDataModel, object>> expression)
            where T : IDataModel
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _includes.ThenInclude(expression as LambdaExpression);

            return new ProjectionInnerBuilder<T, TRootDataModel>(_storage, _filter, _limiter, _includes);
        }

        public ProjectionInnerBuilder<TDataModel, TRootDataModel, TSelect> Map<TSelect>(Expression<Func<TRootDataModel, TSelect>> select)
            => new ProjectionInnerBuilder<TDataModel, TRootDataModel, TSelect>(_storage, _filter, _limiter, _includes, select);

        public ProjectionInnerBuilder<TDataModel, TRootDataModel, TGroup, TSelect>
            GroupAndMap<TGroup, TSelect>(Expression<Func<TRootDataModel, TGroup>> group,
                Expression<Func<IGrouping<TGroup, TRootDataModel>, TSelect>> select)
            => new ProjectionInnerBuilder<TDataModel, TRootDataModel, TGroup, TSelect>(_storage,
                _filter, _limiter, _includes, group, select);

        public FluentQueryBuilderWithProjection<TRootDataModel> Project()
            => new FluentQueryBuilderWithProjection<TRootDataModel>(_storage, _filter, _limiter, _includes);
    }
}
