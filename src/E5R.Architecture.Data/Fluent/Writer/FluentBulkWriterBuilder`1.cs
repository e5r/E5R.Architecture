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
    public class FluentBulkWriterBuilder<TDataModel> : FluentBulkWriterBuilderElements<TDataModel>
        where TDataModel : IIdentifiable
    {
        public FluentBulkWriterBuilder(IStorageBulkWriter<TDataModel> storage) : base(storage,
            new ExpressionDataFilter<TDataModel>())
        { }

        internal FluentBulkWriterBuilder(IStorageBulkWriter<TDataModel> storage,
            ExpressionDataFilter<TDataModel> filter) : base(storage, filter)
        { }

        #region Makers

        public FluentBulkWriterBuilderWithFilter<TDataModel> Filter(
            Expression<Func<TDataModel, bool>> filterExpression)
            => new FluentBulkWriterBuilderWithFilter<TDataModel>(_storage, _filter).Filter(
                filterExpression);

        #endregion

        #region Storage Actions

        public IEnumerable<TDataModel> BulkCreate(IEnumerable<TDataModel> data) =>
            _storage.BulkCreate(data);

        public IEnumerable<TDataModel> BulkReplace(IEnumerable<TDataModel> data) =>
            _storage.BulkReplace(data);

        public void BulkRemove(IEnumerable<TDataModel> data) => _storage.BulkRemove(data);

        #endregion
    }
}
