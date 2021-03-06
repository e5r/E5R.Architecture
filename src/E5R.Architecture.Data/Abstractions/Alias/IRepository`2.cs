﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

namespace E5R.Architecture.Data.Abstractions.Alias
{
    public interface IRepository<TUowProperty, TDataModel> : IRepository<TDataModel>
        where TDataModel : IDataModel
    { }

    public interface IRepository<TDataModel> : IStorage<TDataModel>
        where TDataModel : IDataModel
    { }
}
