﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data.Fluent.Writer
{
    public class FluentWriterBuilder<TDataModel> : FluentWriterBuilderElements<TDataModel>
        where TDataModel : IIdentifiable
    {
        public FluentWriterBuilder(IStorageWriter<TDataModel> storage) : base(storage,
            new List<object>())
        { }

        internal FluentWriterBuilder(IStorageWriter<TDataModel> storage, List<object> identifiers) : base(storage, identifiers)
        { }

        #region Makers

        public FluentWriterBuilderWithIdentifier<TDataModel> Identifier(object identifier)
            => new FluentWriterBuilderWithIdentifier<TDataModel>(_storage, _identifiers)
                .Identifier(identifier);
        
        public FluentWriterBuilderWithIdentifier<TDataModel> Identifiers(object[] identifiers)
            => new FluentWriterBuilderWithIdentifier<TDataModel>(_storage, _identifiers)
                .Identifiers(identifiers);

        #endregion

        #region Storage Actions

        public TDataModel Create(TDataModel data) => _storage.Create(data);
        public TDataModel Replace(TDataModel data) => _storage.Replace(data);
        public void Remove(TDataModel data) => _storage.Remove(data);

        #endregion
    }
}
