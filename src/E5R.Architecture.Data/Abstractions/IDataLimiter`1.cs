﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    /// <inheritdoc />
    public interface IDataLimiter<TDataModel>
        where TDataModel : IIdentifiable
    {
        uint? OffsetBegin { get; }
        uint? OffsetLimit { get; }

        IEnumerable<IDataSorter<TDataModel>> GetSorters();
    }
}
