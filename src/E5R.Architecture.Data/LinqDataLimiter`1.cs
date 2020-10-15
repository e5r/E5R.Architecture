﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    /// <inheritdoc />
    public class LinqDataLimiter<TDataModel> : IDataLimiter<TDataModel>
        where TDataModel : IDataModel
    {
        public uint? OffsetBegin { get; private set; }
        public uint? OffsetLimit { get; private set; }
        public IEnumerable<IDataSorter<TDataModel>> GetSorters() => _sorters;

        private List<IDataSorter<TDataModel>> _sorters = new List<IDataSorter<TDataModel>>();

        public LinqDataLimiter<TDataModel> Sort(Expression<Func<TDataModel, object>> sorter)
        {
            Checker.NotNullArgument(sorter, nameof(sorter));

            _sorters.Add(new LinqDataSorter<TDataModel>(sorter, false));

            return this;
        }

        public LinqDataLimiter<TDataModel> SortDescending(Expression<Func<TDataModel, object>> sorter)
        {
            Checker.NotNullArgument(sorter, nameof(sorter));

            _sorters.Add(new LinqDataSorter<TDataModel>(sorter, true));

            return this;
        }

        public LinqDataLimiter<TDataModel> BeginOffset(uint offset)
        {
            OffsetBegin = offset;

            return this;
        }

        public LinqDataLimiter<TDataModel> LimitOffset(uint offsetLimit)
        {
            OffsetLimit = offsetLimit;

            return this;
        }

        public LinqDataLimiter<TDataModel> Paginate(uint currentPage, uint limitPerPage)
        {
            if (currentPage < 1)
            {
                // TODO: Implementar internacionalização
                throw new IndexOutOfRangeException($"The parameter {nameof(currentPage)} must start at 1.");
            }

            OffsetBegin = --currentPage * limitPerPage;
            OffsetLimit = limitPerPage;

            return this;
        }
    }
}
