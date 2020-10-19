// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

namespace E5R.Architecture.Data.Abstractions.Alias
{
    public interface IRepositoryReader<TUowProperty, TDataModel> : IRepositoryReader<TDataModel>
        where TDataModel : IDataModel
    { }

    public interface IRepositoryReader<TDataModel> : IStorageReader<TDataModel>
        where TDataModel : IDataModel
    { }
}
