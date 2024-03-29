﻿// Copyright (c) E5R Development Team. All rights reserved.
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
    public class ProjectionRootBuilder<TDataModel> : FluentQueryBuilderElements<TDataModel>
        where TDataModel : IIdentifiable
    {
        internal ProjectionRootBuilder(IStorageReader<TDataModel> storage,
            DataFilter<TDataModel> filter,
            DataLimiter<TDataModel> limiter,
            DataIncludes<TDataModel> includes)
            : base(storage, filter, limiter, includes)
        { }

        public ProjectionRootBuilder<TDataModel> Include(Expression<Func<TDataModel, object>> expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _includes.Include(expression as LambdaExpression);

            return this;
        }

        public ProjectionInnerBuilder<T, TDataModel> Include<T>(Expression<Func<TDataModel, object>> expression)
            where T : IIdentifiable
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _includes.Include(expression as LambdaExpression);

            return new ProjectionInnerBuilder<T, TDataModel>(_storage, _filter, _limiter, _includes);
        }

        public ProjectionRootBuilder<TDataModel, TSelect> Map<TSelect>(Expression<Func<TDataModel, TSelect>> select)
            => new ProjectionRootBuilder<TDataModel, TSelect>(_storage, _filter, _limiter, _includes, select);
        
        public ProjectionRootBuilder<TDataModel, TGroup, TSelect>
            GroupAndMap<TGroup, TSelect>(Expression<Func<TDataModel, TGroup>> group,
                Expression<Func<IGrouping<TGroup, TDataModel>, TSelect>> select)
            => new ProjectionRootBuilder<TDataModel, TGroup, TSelect>(_storage,
                _filter, _limiter, _includes, group, select);

        public FluentQueryBuilderWithProjection<TDataModel> Project()
            => new FluentQueryBuilderWithProjection<TDataModel>(_storage, _filter, _limiter, _includes);
    }
}
