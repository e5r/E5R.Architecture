// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Data;
using E5R.Architecture.Data.Abstractions;

namespace UsingData
{
    public class BlogModule : DataModule<BlogStorage>
    {
        public BlogModule(IUnitOfWork uow, BlogStorage storage)
        {
            (Storage1 = storage).Configure(uow.Session);
        }

        public BlogStorage Blog => Storage1;
    }
}
