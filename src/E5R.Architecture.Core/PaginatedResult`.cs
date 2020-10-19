// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;

namespace E5R.Architecture.Core
{
    /// <summary>
    /// Generic paginated result of items
    /// </summary>
    /// <typeparam name="TResult">Result item type</typeparam>
    public class PaginatedResult<TResult>
    {
        public PaginatedResult(IEnumerable<TResult> result, uint offset, uint limit, int total)
        {
            Checker.NotNullArgument(result, nameof(result));

            Result = result;
            Offset = offset;
            Limit = limit;
            Total = total;
        }

        // TODO: Rever conceito de Offset/Limit e Page

        public IEnumerable<TResult> Result { get; private set; }
        public uint Offset { get; private set; }
        public uint Limit { get; private set; }
        public int Total { get; private set; }
        public int PageCount => Limit > 0
            ? Convert.ToInt32(Math.Ceiling((double)Total / Limit))
            : 0;
        public int CurrentPage => Limit > 0
            ? Convert.ToInt32(Math.Floor((double)Offset / Limit)) + 1
            : 0;
        public int? NextPage => CurrentPage < PageCount
            ? CurrentPage + 1
            : (int?)null;
        public int? PreviousPage => CurrentPage > 1
            ? CurrentPage - 1
            : (int?)null;
    }
}
