﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using System.Linq.Expressions;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public class StorageBase<TDataModel>
        where TDataModel : class, IIdentifiable
    {
        protected IQueryable<TDataModel> QueryFind(
            IEntityType entityType,
            IQueryable<TDataModel> origin,
            object[] identifiers,
            IDataIncludes includes)
        {
            Checker.NotNullArgument(origin, nameof(origin));
            Checker.NotNullArgument(entityType, nameof(entityType));
            Checker.NotNullArgument(identifiers, nameof(identifiers));

            var primaryKeys = entityType.FindPrimaryKey().Properties;

            if (identifiers.Length < 1)
            {
                // TODO: Implementar i18n/l10n
                throw new InvalidOperationException("At least one identifier must be informed.");
            }

            if (primaryKeys.Count() != identifiers.Count())
            {
                // TODO: Implementar i18n/l10n
                throw new InvalidOperationException($"Number of primary keys configured in {typeof(TDataModel)} different than expected.");
            }

            var filter = new DataFilter<TDataModel>();
            var param = Expression.Parameter(typeof(TDataModel), "e");
            Expression<Func<TDataModel, bool>> filterExpression = null;

            foreach (var (pk, idx) in primaryKeys.Select((pk, idx) => (pk, idx)))
            {
                if (filterExpression != null)
                {
                    filterExpression = Expression.Lambda<Func<TDataModel, bool>>(
                        Expression.AndAlso(
                            filterExpression.Body,
                            Expression.Equal(
                                Expression.PropertyOrField(param, pk.Name),
                                Expression.Constant(identifiers[idx])
                        )),
                        param
                        );
                }
                else
                {
                    filterExpression = Expression.Lambda<Func<TDataModel, bool>>(
                        Expression.Equal(
                            Expression.PropertyOrField(param, pk.Name),
                            Expression.Constant(identifiers[idx])
                        ),
                        param
                    );
                }
            }

            filter.AddFilter(filterExpression);

            return QuerySearch(origin, filter, includes);
        }

        protected IQueryable<TDataModel> QuerySearch(
            IQueryable<TDataModel> origin,
            IDataFilter<TDataModel> filter,
            IDataIncludes includes)
        {
            Checker.NotNullArgument(origin, nameof(origin));
            Checker.NotNullArgument(filter, nameof(filter));

            var filterExpressions = filter.GetExpressions();

            Checker.NotNullObject(filterExpressions, $"filter.{nameof(filter.GetExpressions)}()");

            var query = filterExpressions.Aggregate(origin, (q, w) => q.Where(w));

            return TryApplyIncludes(query, includes);
        }

        /// <summary>
        /// Query for operations that must return an <see cref="PaginatedResult{TDataModel}" />
        /// </summary>
        /// <param name="limiter">Data limiter</param>
        /// <param name="origin">Original data query</param>
        /// <param name="includes">Data includes</param>
        /// <returns>Tuple of (offset, limit, total, result)</returns>
        protected (uint, uint, int, IQueryable<TDataModel>) QueryPreLimitResult(
            IQueryable<TDataModel> origin,
            IDataLimiter<TDataModel> limiter,
            IDataIncludes includes)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));
            Checker.NotNullArgument(origin, nameof(origin));

            // Ensures offset range
            if (limiter.OffsetLimit.HasValue &&
                (limiter.OffsetLimit.Value == 0 || limiter.OffsetLimit.Value > int.MaxValue))
            {
                throw new ArgumentOutOfRangeException($"limiter.{limiter.OffsetLimit}");
            }

            var result = TryApplyIncludes(origin, includes);

            IOrderedQueryable<TDataModel> orderBy = null;

            foreach (var sorter in limiter.GetSorters())
            {
                if (orderBy == null)
                {
                    orderBy = sorter.Descending 
                        ? result.OrderByDescending(sorter.Sorter) 
                        : result.OrderBy(sorter.Sorter);
                }
                else
                {
                    orderBy = sorter.Descending 
                        ? orderBy.ThenByDescending(sorter.Sorter) 
                        : orderBy.ThenBy(sorter.Sorter);
                }
            }

            result = orderBy ?? result;

            uint offset;
            uint limit;
            
            var total = result.Count();

            if (limiter.OffsetBegin.HasValue)
            {
                offset = limiter.OffsetBegin.Value;
                result = result.Skip(Convert.ToInt32(offset));
            }
            else
            {
                offset = 0;
            }

            if (limiter.OffsetLimit.HasValue)
            {
                limit = limiter.OffsetLimit.Value;
                result = result.Take(Convert.ToInt32(limit));
            }
            else
            {
                limit = Convert.ToUInt32(total);
            }

            return (offset, limit, total, result);
        }

        protected IQueryable<TDataModel> TryApplyIncludes(
            IQueryable<TDataModel> query,
            IDataIncludes includes)
        {
            return includes != null
                ? includes.Includes.Aggregate(query, (q, i) => q.Include(i))
                : query;
        }
    }
}
