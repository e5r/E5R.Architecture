// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Linq implementation for IDataIncludes
    /// </summary>
    public class DataIncludes<TDataModel> : IDataIncludes
        where TDataModel : IDataModel
    {
        internal IList<DataProjectionIncludeMember> _includes;

        public DataIncludes()
        {
            _includes = new List<DataProjectionIncludeMember>();
        }

        internal DataIncludes(IList<DataProjectionIncludeMember> includes)
        {
            Checker.NotNullArgument(includes, nameof(includes));

            _includes = includes;
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

            _includes.Add(new DataProjectionIncludeMember((expression.Body as MemberExpression).Member));
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
}
