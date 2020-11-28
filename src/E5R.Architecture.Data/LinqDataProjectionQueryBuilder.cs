// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data
{
    public class LinqDataProjectionQueryBuilder<TDataModel>
        where TDataModel : IDataModel
    {
        private readonly LinqStorageQueryBuilder<TDataModel> _storageQueryBuilder;

        public LinqDataProjectionQueryBuilder(LinqStorageQueryBuilder<TDataModel> storageQueryBuilder)
        {
            Checker.NotNullArgument(storageQueryBuilder, nameof(storageQueryBuilder));

            _storageQueryBuilder = storageQueryBuilder;
        }

        public LinqDataProjectionQueryBuilder<TDataModel> Include(Expression<Func<TDataModel, object>> expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _storageQueryBuilder.Projection.Include(expression as LambdaExpression);

            return this;
        }

        public LinqDataProjectionInnerQueryBuilder<T, TDataModel> Include<T>(Expression<Func<TDataModel, object>> expression)
            where T : IDataModel
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _storageQueryBuilder.Projection.Include(expression as LambdaExpression);

            return new LinqDataProjectionInnerQueryBuilder<T, TDataModel>(_storageQueryBuilder, this);
        }

        public LinqStorageQueryBuilder<TDataModel> Project() => _storageQueryBuilder;
        public LinqStorageProjectionActionsQueryBuilder<TDataModel, TSelect> Project<TSelect>(Expression<Func<TDataModel, TSelect>> select)
            => new LinqStorageProjectionActionsQueryBuilder<TDataModel, TSelect>(_storageQueryBuilder, select);
    }

    public class LinqDataProjectionInnerQueryBuilder<TDataModel, TRootDataModel>
        where TDataModel : IDataModel
        where TRootDataModel : IDataModel
    {
        private readonly LinqStorageQueryBuilder<TRootDataModel> _storageQueryBuilder;
        private readonly LinqDataProjectionQueryBuilder<TRootDataModel> _root;

        public LinqDataProjectionInnerQueryBuilder(LinqStorageQueryBuilder<TRootDataModel> storageQueryBuilder, LinqDataProjectionQueryBuilder<TRootDataModel> root)
        {
            Checker.NotNullArgument(storageQueryBuilder, nameof(storageQueryBuilder));
            Checker.NotNullArgument(root, nameof(root));

            _storageQueryBuilder = storageQueryBuilder;
            _root = root;
        }

        public LinqDataProjectionQueryBuilder<TRootDataModel> Include(Expression<Func<TRootDataModel, object>> expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _storageQueryBuilder.Projection.Include(expression as LambdaExpression);

            return _root;
        }

        public LinqDataProjectionInnerQueryBuilder<TDataModel, TRootDataModel> ThenInclude(Expression<Func<TDataModel, object>> expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _storageQueryBuilder.Projection.ThenInclude(expression as LambdaExpression);

            return this;
        }

        public LinqDataProjectionInnerQueryBuilder<T, TRootDataModel> ThenInclude<T>(Expression<Func<TDataModel, object>> expression)
            where T : IDataModel
        {
            Checker.NotNullArgument(expression, nameof(expression));

            _storageQueryBuilder.Projection.ThenInclude(expression as LambdaExpression);

            return new LinqDataProjectionInnerQueryBuilder<T, TRootDataModel>(_storageQueryBuilder, _root);
        }

        public LinqStorageQueryBuilder<TRootDataModel> Project() => _storageQueryBuilder;
    }
}
