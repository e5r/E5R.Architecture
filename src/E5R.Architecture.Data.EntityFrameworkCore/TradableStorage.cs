// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    using Core;
    using Abstractions;

    public class TradableStorage : ITradableObject
    {
        protected DbContext Context { get; private set; }

        public virtual void Configure(UnderlyingSession session)
        {
            Checker.NotNullArgument(session, nameof(session));

            Context = session.Get<DbContext>();

            if (Context == null)
            {
                // TODO: Implementar internacionalização
                throw new NullReferenceException(
                    $"The context is null. The session has not been configured.");
            }
        }
    }
}
