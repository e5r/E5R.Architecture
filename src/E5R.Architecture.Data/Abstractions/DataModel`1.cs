// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

namespace E5R.Architecture.Data.Abstractions
{
    /// <summary>
    /// Data model (Entity) representation with identifiers
    /// </summary>
    /// <typeparam name="TBusinessModel">Type of business model</typeparam>
    public abstract class DataModel<TBusinessModel> : IDataModel
        where TBusinessModel : new()
    {
        protected DataModel() => Business = new TBusinessModel();

        protected DataModel(TBusinessModel businessModel) => Business = businessModel;

        protected TBusinessModel Business { get; private set; }

        public virtual object[] IdentifierValues { get; }

        public TBusinessModel Unwrap() => Business;
    }
}
