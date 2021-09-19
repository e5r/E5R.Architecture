// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace E5R.Architecture.Core
{
    public abstract class IdentifiableExpressionMaker<TIdentifiable> where TIdentifiable : class, IIdentifiable
    {
        protected abstract IEnumerable<Expression<Func<TIdentifiable, bool>>> MakeExpressions();

        public IQueryable<TIdentifiable> TryAggregate(IQueryable<TIdentifiable> query)
        {
            Checker.NotNullArgument(query, nameof(query));

            var expressionFilterList = MakeExpressions();

            if (expressionFilterList != null)
            {
                return expressionFilterList.Aggregate(query, (q, w) => q.Where(w));
            }

            return query;
        }
    }
}
