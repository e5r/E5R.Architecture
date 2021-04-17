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
    public class FluentWriterBuilderWithIdentifier<TDataModel> : FluentWriterBuilderElements<TDataModel>
        where TDataModel : IIdentifiable
    {
        internal FluentWriterBuilderWithIdentifier(IStorageWriter<TDataModel> storage,
            List<object> identifiers) : base(storage, identifiers)
        { }

        public FluentWriterBuilderWithIdentifier<TDataModel> Identifier(object identifier)
        {
            Checker.NotNullArgument(identifier, nameof(identifier));
            
            _identifiers.Clear();
            _identifiers.Add(identifier);

            return this;
        }
        
        public FluentWriterBuilderWithIdentifier<TDataModel> Identifiers(object[] identifiers)
        {
            Checker.NotNullArgument(identifiers, nameof(identifiers));
            
            _identifiers.Clear();
            _identifiers.AddRange(identifiers);

            return this;
        }

        #region Storage Actions

        public void Remove() => _storage.Remove(_identifiers.ToArray());

        public TDataModel Update<TUpdated>(TUpdated updated) =>
            _storage.Update(_identifiers.ToArray(), updated);

        public TDataModel
            Update<TUpdated>(Expression<Func<TDataModel, TUpdated>> updateExpression) =>
            _storage.Update(_identifiers.ToArray(), updateExpression);

        #endregion
    }
}
