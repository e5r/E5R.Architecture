// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Linq implementation for IDataSorter
    /// </summary>
    /// <typeparam name="TDataModel">Model type</typeparam>
    public class LinqDataSorter<TDataModel> : IDataSorter<TDataModel>
        where TDataModel : IDataModel
    {
        public LinqDataSorter(Expression<Func<TDataModel, object>> sorter, bool descending)
        {
            Checker.NotNullArgument(sorter, nameof(sorter));

            Sorter = sorter;
            Descending = descending;
        }

        public bool Descending { get; private set; }

        public Expression<Func<TDataModel, object>> Sorter { get; private set; }
    }
}
