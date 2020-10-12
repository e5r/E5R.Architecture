// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Result of a search with <see cref="IDataLimiter{TDataModel}"/>
    /// </summary>
    /// <typeparam name="TDataModel">Data model type</typeparam>
    public class DataLimiterResult<TDataModel>
        where TDataModel : IDataModel
    {
        public DataLimiterResult(IEnumerable<TDataModel> result, uint offset, uint limit, int total)
        {
            Checker.NotNullArgument(result, nameof(result));

            Result = result;
            Offset = offset;
            Limit = limit;
            Total = total;
        }

        // TODO: Rever conceito de Offset/Limit e Page

        public IEnumerable<TDataModel> Result { get; private set; }
        public uint Offset { get; private set; }
        public uint Limit { get; private set; }
        public int Total { get; private set; }
        public int PageCount
        {
            get
            {
                if (Limit < 1)
                {
                    return 0;
                }
                return Convert.ToInt32(Math.Ceiling((double)Total / Limit));
            }
        }
        public int CurrentPage
        {
            get
            {
                if (Limit < 1)
                {
                    return 0;
                }
                return Convert.ToInt32(Math.Floor((double)Offset / Limit)) + 1;
            }
        }
        public int? NextPage
        {
            get
            {
                if (CurrentPage >= PageCount)
                    return null;

                return CurrentPage + 1;
            }
        }
        public int? PreviousPage
        {
            get
            {
                if (CurrentPage <= 1)
                    return null;

                return CurrentPage - 1;
            }
        }

    }
}
