using System;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public class TradableStorage<TFluentResult> : ITradableObject<TFluentResult>
        where TFluentResult : class
    {
        protected DbContext Context { get; private set; }

        public virtual TFluentResult Configure(UnderlyingSession session)
        {
            Checker.NotNullArgument(session, nameof(session));

            Context = session.Get<DbContext>();

            if (Context == null)
            {
                // TODO: Implementar internacionalização
                throw new NullReferenceException(
                    $"The context is null. The session has not been configured.");
            }

            return this as TFluentResult;
        }
    }
}
