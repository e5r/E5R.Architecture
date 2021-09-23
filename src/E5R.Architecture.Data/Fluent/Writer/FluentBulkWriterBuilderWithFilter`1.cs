// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.Fluent.Writer
{
    public class FluentBulkWriterBuilderWithFilter<TDataModel> : FluentBulkWriterBuilderElements<TDataModel>
        where TDataModel : IIdentifiable
    {
        internal FluentBulkWriterBuilderWithFilter(IStorageBulkWriter<TDataModel> storage,
            DataFilter<TDataModel> filter) : base(storage, filter)
        { }

        public FluentBulkWriterBuilderWithFilter<TDataModel> Filter(
            Expression<Func<TDataModel, bool>> filterExpression)
        {
            Checker.NotNullArgument(filterExpression, nameof(filterExpression));

            _filter.AddFilter(filterExpression);

            return this;
        }

        public FluentBulkWriterBuilderWithFilter<TDataModel> Filter(
            IIdentifiableExpressionMaker<TDataModel> filterMaker)
        {
            Checker.NotNullArgument(filterMaker, nameof(filterMaker));

            _filter.AddFilter(filterMaker);

            return this;
        }

        #region Storage Actions

        public void BulkRemove() => _storage.BulkRemove(_filter);

        public IEnumerable<TDataModel> BulkUpdate<TUpdated>(TUpdated updated) =>
            _storage.BulkUpdate(_filter, updated);

        public IEnumerable<TDataModel> BulkUpdate<TUpdated>(
            Expression<Func<TDataModel, TUpdated>> updateExpression) =>
            _storage.BulkUpdate(_filter, updateExpression);

        #endregion
    }
}
