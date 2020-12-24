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
    /// Linq implementation for <see cref="IDataProjection{TDataModel, TGroup, TSelect}" />
    /// </summary>
    public class DataProjection<TDataModel, TGroup, TSelect> : IDataProjection<TDataModel, TGroup, TSelect>
        where TDataModel : IDataModel
    {
        private readonly IList<DataProjectionIncludeMember> _includes;
        private readonly Expression<Func<IGrouping<TGroup, TDataModel>, TSelect>> _select;
        private readonly Expression<Func<TDataModel, TGroup>> _group;

        public DataProjection(
            Expression<Func<TDataModel, TGroup>> group,
            Expression<Func<IGrouping<TGroup, TDataModel>, TSelect>> select)
        {
            Checker.NotNullArgument(group, nameof(group));
            Checker.NotNullArgument(select, nameof(select));

            _includes = new List<DataProjectionIncludeMember>();
            _group = group;
            _select = select;
        }

        internal DataProjection<TDataModel> GetOnlyIncludeProjection()
            => new DataProjection<TDataModel>(_includes);

        public DataProjection(
            IEnumerable<DataProjectionIncludeMember> includes,
            Expression<Func<TDataModel, TGroup>> group,
            Expression<Func<IGrouping<TGroup, TDataModel>, TSelect>> select)
        {
            Checker.NotNullArgument(includes, nameof(includes));
            Checker.NotNullArgument(group, nameof(group));
            Checker.NotNullArgument(select, nameof(select));

            _includes = includes.ToList();
            _group = group;
            _select = select;
        }

        public Expression<Func<IGrouping<TGroup, TDataModel>, TSelect>> Select => _select;

        public Expression<Func<TDataModel, TGroup>> Group => _group;

        public IEnumerable<string> Includes => _includes.Select(s => s.ToString());
    }
}
