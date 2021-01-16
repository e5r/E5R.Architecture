// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.Fluent.Writer
{
    public class FluentWriterBuilderElements<TDataModel>
        where TDataModel : IDataModel
    {
        internal readonly IStorageWriter<TDataModel> _storage;
        internal readonly List<object> _identifiers;

        internal FluentWriterBuilderElements(
            IStorageWriter<TDataModel> storage,
            List<object> identifiers)
        {
            Checker.NotNullArgument(storage, nameof(storage));
            Checker.NotNullArgument(identifiers, nameof(identifiers));

            _storage = storage;
            _identifiers = identifiers;
        }
    }
}
