// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data
{
    public class LinqDataProjectionQueryBuilder<TDataModel>
        where TDataModel : IDataModel
    {
        private LinqStorageQueryBuilder<TDataModel> _storageQueryBuilder;
        private string _projection = null;

        public LinqDataProjectionQueryBuilder(LinqStorageQueryBuilder<TDataModel> storageQueryBuilder)
        {
            Checker.NotNullArgument(storageQueryBuilder, nameof(storageQueryBuilder));

            _storageQueryBuilder = storageQueryBuilder;
        }

        public LinqDataProjectionQueryBuilder<TDataModel> Include(string projection)
        {
            Checker.NotEmptyOrWhiteArgument(projection, nameof(projection));

            _projection = $"{(_projection != null ? $"{_projection}." : string.Empty)}{projection}";

            return this;
        }

        public LinqStorageQueryBuilder<TDataModel> Project()
        {
            // TODO: Definir uma melhor mensagem de exceção
            Checker.NotEmptyOrWhiteArgument(_projection, nameof(_projection));

            _storageQueryBuilder.AddProjection(new LinqDataProjection(_projection));

            return _storageQueryBuilder;
        }
    }
}
