// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Result of a search with <see cref="IDataLimiter{TDataModel}"/>
    /// </summary>
    /// <typeparam name="TDataModel">Data model type</typeparam>
    public class DataLimiterResult<TDataModel> : PaginatedResult<TDataModel>
        where TDataModel : IDataModel
    {
        public DataLimiterResult(IEnumerable<TDataModel> result, uint offset, uint limit, int total)
            : base(result, offset, limit, total)
        { }
    }
}
