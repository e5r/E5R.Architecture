﻿// Copyright (c) E5R Development Team. All rights reserved.
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
    /// Linq implementation for IDataProjection
    /// </summary>
    public class LinqDataProjection<TDataModel> : IDataProjection
        where TDataModel : IDataModel
    {
        internal IList<LinqDataProjectionIncludeMember> _includes;

        public LinqDataProjection()
        {
            _includes = new List<LinqDataProjectionIncludeMember>();
        }

        public void Include(Expression<Func<TDataModel, object>> expression)
            => Include(expression as Expression<Func<object, object>>);

        public void ThenInclude(Expression<Func<TDataModel, object>> expression)
            => ThenInclude(expression as Expression<Func<object, object>>);

        public IEnumerable<string> Includes => _includes.Select(s => s.ToString());

        internal void Include(LambdaExpression expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            if (expression.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new InvalidOperationException($"Lambda expression used in [Include] is not valid.");
            }

            _includes.Add(new LinqDataProjectionIncludeMember((expression.Body as MemberExpression).Member));
        }

        internal void ThenInclude(LambdaExpression expression)
        {
            Checker.NotNullArgument(expression, nameof(expression));

            if (expression.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new InvalidOperationException($"Lambda expression used in [ThenInclude] is not valid.");
            }

            var lastIncludeMember = _includes.LastOrDefault();

            if (lastIncludeMember != null)
            {
                var lastIdx = _includes.IndexOf(lastIncludeMember);

                _includes[lastIdx] = lastIncludeMember.Append((expression.Body as MemberExpression).Member);
            }
            else
            {
                Include(expression);
            }
        }
    }

    /// <summary>
    /// Linq implementation for <see cref="IDataProjection{TDataModel, TSelect}" />
    /// </summary>
    public class LinqDataProjection<TDataModel, TSelect> : IDataProjection<TDataModel, TSelect>
        where TDataModel : IDataModel
    {
        private readonly IList<LinqDataProjectionIncludeMember> _includes;
        private readonly Expression<Func<TDataModel, TSelect>> _select;

        public LinqDataProjection(Expression<Func<TDataModel, TSelect>> select)
        {
            Checker.NotNullArgument(select, nameof(select));

            _includes = new List<LinqDataProjectionIncludeMember>();
            _select = select;
        }

        public LinqDataProjection(IEnumerable<LinqDataProjectionIncludeMember> includes, Expression<Func<TDataModel, TSelect>> select)
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