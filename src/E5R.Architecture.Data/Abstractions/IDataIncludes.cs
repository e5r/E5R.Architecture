﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;

namespace E5R.Architecture.Data.Abstractions
{
    /// <summary>
    /// Data includes for data model
    /// </summary>
    public interface IDataIncludes
    {
        /// <summary>
        /// A navigation property paths (separated by ".")
        /// </summary>
        IEnumerable<string> Includes { get; }
    }
}
