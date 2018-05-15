// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;

namespace E5R.Architecture.Data
{
    using Abstractions;

    /// <summary>
    /// Result of a search with <see cref="DataLimiter{TDataModel}"/>
    /// </summary>
    /// <typeparam name="TDataModel">Data model type</typeparam>
    public class DataLimiterResult<TDataModel>
        where TDataModel : IDataModel
    {
        public DataLimiterResult(IEnumerable<TDataModel> result, int count)
        {
            Result = result;
            Count = count;
        }

        public int Count { get; private set; }
        public IEnumerable<TDataModel> Result { get; private set; }
    }
}
