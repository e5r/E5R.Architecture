// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Data.Fluent.Query;
using E5R.Architecture.Data.Fluent.Writer;

namespace E5R.Architecture.Data.Abstractions
{
    public static class StorageExtensions
    {
        public static FluentQueryBuilder<TDataModel> AsFluentQuery<TDataModel>(
            this IStorageReader<TDataModel> storage)
            where TDataModel : IDataModel
        {
            return new FluentQueryBuilder<TDataModel>(storage);
        }

        public static FluentWriterBuilder<TDataModel> AsFluentWriter<TDataModel>(
            this IStorageWriter<TDataModel> storage)
            where TDataModel : IDataModel
        {
            return new FluentWriterBuilder<TDataModel>(storage);
        }
        
        public static FluentBulkWriterBuilder<TDataModel> AsFluentBulkWriter<TDataModel>(
            this IStorageBulkWriter<TDataModel> storage)
            where TDataModel : IDataModel
        {
            return new FluentBulkWriterBuilder<TDataModel>(storage);
        }
    }
}
