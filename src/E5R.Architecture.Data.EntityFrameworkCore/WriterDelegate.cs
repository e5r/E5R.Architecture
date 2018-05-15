// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public delegate void WriterDelegate(object entity, Action<EntityEntryGraphNode> writer);
}
