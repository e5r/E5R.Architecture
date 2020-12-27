// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Linq implementation for <see cref="IDataProjection{TDataModel, TSelect}" />
    /// </summary>
    public class DataProjection<TDataModel, TSelect> : IDataProjection<TDataModel, TSelect>
        where TDataModel : IDataModel
    {
        private readonly IList<DataProjectionIncludeMember> _includes;
        private readonly Expression<Func<TDataModel, TSelect>> _select;

        public DataProjection(Expression<Func<TDataModel, TSelect>> select)
        {
            Checker.NotNullArgument(select, nameof(select));

            _includes = new List<DataProjectionIncludeMember>();
            _select = select;
        }

        internal DataIncludes<TDataModel> GetDataIncludes()
            => new DataIncludes<TDataModel>(_includes);

        public DataProjection(IEnumerable<DataProjectionIncludeMember> includes, Expression<Func<TDataModel, TSelect>> select)
        {
            Checker.NotNullArgument(includes, nameof(includes));
            Checker.NotNullArgument(select, nameof(select));

            _includes = includes.ToList();
            _select = select;
        }

        public Expression<Func<TDataModel, TSelect>> Select => _select;

        public IEnumerable<string> Includes => _includes.Select(s => s.ToString());
    }
}
