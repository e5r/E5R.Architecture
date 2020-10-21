// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public class StorageBase<TDataModel>
        where TDataModel : class, IDataModel
    {
        protected IQueryable<TDataModel> QuerySearch(
            IQueryable<TDataModel> origin,
            IDataFilter<TDataModel> filter,
            IEnumerable<IDataProjection> projections)
        {
            Checker.NotNullArgument(origin, nameof(origin));
            Checker.NotNullArgument(filter, nameof(filter));

            var filterList = filter.GetFilter();

            Checker.NotNullObject(filterList, $"filter.{nameof(filter.GetFilter)}()");

            var query = filterList.Aggregate(origin, (q, w) => q.Where(w));

            return TryApplyProjections(query, projections);
        }

        protected DataLimiterResult<TDataModel> QueryLimitResult(
            IDataLimiter<TDataModel> limiter,
            IQueryable<TDataModel> origin,
            IEnumerable<IDataProjection> projections)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));
            Checker.NotNullArgument(origin, nameof(origin));

            // Ensures offset range
            if (limiter.OffsetLimit.HasValue &&
                (limiter.OffsetLimit.Value == 0 || limiter.OffsetLimit.Value > int.MaxValue))
            {
                throw new ArgumentOutOfRangeException($"limiter.{limiter.OffsetLimit}");
            }

            var result = TryApplyProjections(origin, projections);

            IOrderedQueryable<TDataModel> orderBy = null;

            foreach (var sorter in limiter.GetSorters())
            {
                if (orderBy == null)
                {
                    if (sorter.Descending)
                        orderBy = result.OrderByDescending(sorter.Sorter);
                    else
                        orderBy = result.OrderBy(sorter.Sorter);
                }
                else
                {
                    if (sorter.Descending)
                        orderBy = orderBy.ThenByDescending(sorter.Sorter);
                    else
                        orderBy = orderBy.ThenBy(sorter.Sorter);
                }
            }

            result = orderBy ?? result;

            uint offset = 0;
            uint limit = 0;
            int total = result.Count();

            if (limiter.OffsetBegin.HasValue)
            {
                offset = limiter.OffsetBegin.Value;
                result = result.Skip(Convert.ToInt32(offset));
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

            return new DataLimiterResult<TDataModel>(result, offset, limit, total);
        }

        protected IQueryable<TDataModel> TryApplyProjections(
            IQueryable<TDataModel> query,
            IEnumerable<IDataProjection> projections)
        {
            foreach (var projection in (projections ?? Enumerable.Empty<IDataProjection>()))
            {
                query = query.Include(projection.GetProjectionString());
            }

            return query;
        }
    }
}
