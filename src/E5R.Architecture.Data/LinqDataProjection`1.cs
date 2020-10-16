// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Linq implementation for IDataProjection
    /// </summary>
    public class LinqDataProjection : IDataProjection
    {
        private readonly string _projection;

        public LinqDataProjection(string projection)
        {
            Checker.NotNullArgument(projection, nameof(projection));

            _projection = projection;
        }

        public string GetProjectionString() => _projection;
    }
}
