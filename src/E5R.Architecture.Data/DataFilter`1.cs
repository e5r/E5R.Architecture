// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Implementation for IDataFilter
    /// </summary>
    /// <typeparam name="TDataModel">Model type</typeparam>
    public class DataFilter<TDataModel> : IDataFilter<TDataModel>
        where TDataModel : IIdentifiable
    {
        private readonly Dictionary<object, Expression<Func<TDataModel, bool>>> _filterItems
            = new Dictionary<object, Expression<Func<TDataModel, bool>>>();

        public DataFilter<TDataModel> AddFilter(Expression<Func<TDataModel, bool>> filterExpression)
        {
            _filterItems.Add(filterExpression, filterExpression);

            return this;
        }

        public DataFilter<TDataModel> AddFilter(IIdentifiableExpressionMaker<TDataModel> filterMakerr)
        {
            _filterItems.Add(filterMakerr, filterMakerr.MakeExpression());

            return this;
        }

        public IEnumerable<Expression<Func<TDataModel, bool>>> GetExpressions() => _filterItems.Values;

        public IEnumerable<object> GetObjects() => _filterItems.Keys;
    }
}
