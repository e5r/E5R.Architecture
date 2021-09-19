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
    /// Linq implementation for IDataFilter
    /// </summary>
    /// <typeparam name="TDataModel">Model type</typeparam>
    public class ExpressionDataFilter<TDataModel> : IDataFilter<TDataModel>
        where TDataModel : IIdentifiable
    {
        private readonly List<Expression<Func<TDataModel, bool>>> _filters
            = new List<Expression<Func<TDataModel, bool>>>();

        public ExpressionDataFilter<TDataModel> AddFilter(Expression<Func<TDataModel, bool>> filter)
        {
            _filters.Add(filter);

            return this;
        }

        /// <summary>
        /// Get a filter expression (Where)
        /// </summary>
        /// <returns>List of reducer expression</returns>
        public IEnumerable<Expression<Func<TDataModel, bool>>> GetExpressionFilter() => _filters;

        /// <summary>
        /// Get a filter object
        /// </summary>
        /// <typeparam name="TObject">Filter type</typeparam>
        /// <returns>An object with filter data</returns>
        public TObject GetObjectFilter<TObject>() where TObject : class
        {
            // TODO: Implementar i18n/l10n
            throw new DataLayerException("Object filter not supported");
        }
    }
}
