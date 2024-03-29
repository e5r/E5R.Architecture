﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.Fluent.Writer
{
    public class FluentBulkWriterBuilderElements<TDataModel>
        where TDataModel : IIdentifiable
    {
        internal readonly IStorageBulkWriter<TDataModel> _storage;
        internal readonly DataFilter<TDataModel> _filter;

        internal FluentBulkWriterBuilderElements(
            IStorageBulkWriter<TDataModel> storage,
            DataFilter<TDataModel> filter)
        {
            Checker.NotNullArgument(storage, nameof(storage));
            Checker.NotNullArgument(filter, nameof(filter));

            _storage = storage;
            _filter = filter;
        }
    }
}
