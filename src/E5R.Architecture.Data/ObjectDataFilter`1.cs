// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;
using E5R.Architecture.Core.Exceptions;
using E5R.Architecture.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Object implementation for IDataFilter
    /// </summary>
    /// <typeparam name="TDataModel">Model type</typeparam>
    public class ObjectDataFilter<TDataModel> : IDataFilter<TDataModel>
        where TDataModel : IIdentifiable
    {
        private readonly object _objectFilter;

        public ObjectDataFilter(object objectFilter)
        {
            Checker.NotNullArgument(objectFilter, nameof(objectFilter));

            _objectFilter = objectFilter;
        }

        /// <summary>
        /// Get a filter expression (Where)
        /// </summary>
        /// <returns>List of reducer expression</returns>
        public IEnumerable<Expression<Func<TDataModel, bool>>> GetExpressionFilter()
        {
            // TODO: Implementar i18n/l10n
            throw new DataLayerException("Expression filter not supported");
        }

        /// <summary>
        /// Get a filter object
        /// </summary>
        /// <typeparam name="TObject">Filter type</typeparam>
        /// <returns>An object with filter data</returns>
        public TObject GetObjectFilter<TObject>() where TObject : class
        {
            if (_objectFilter.GetType() != typeof(TObject))
            {
                throw new DataLayerException("Filter object type invalid");
            }

            return _objectFilter as TObject;
        }
    }
}
