// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;

namespace E5R.Architecture.Data.Abstractions
{
    // TODO: Renomear todos os X<TUowProperty, TDataModel> para X<TUow...>
    public interface
        IFindableStorageWithSelector<TUowProperty, TDataModel> : IFindableStorageWithSelector<
            TDataModel>
        where TDataModel : IIdentifiable
    {
    }

    // TODO: Find(object...) e Find(TDataModel...) para métodos de extensão sobre IStorageReader<> e deixar apenas Find(object[]...)
    public interface IFindableStorageWithSelector<TDataModel> : IStorageSignature
        where TDataModel : IIdentifiable
    {
        TSelect Find<TSelect>(object identifier, IDataProjection<TDataModel, TSelect> projection);

        TSelect Find<TSelect>(object[] identifiers,
            IDataProjection<TDataModel, TSelect> projection);

        TSelect Find<TSelect>(TDataModel data, IDataProjection<TDataModel, TSelect> projection);
    }
}
