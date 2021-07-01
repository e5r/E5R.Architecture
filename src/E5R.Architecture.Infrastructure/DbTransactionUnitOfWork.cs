// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Data.Common;

namespace E5R.Architecture.Infrastructure
{
    public class DbTransactionUnitOfWork : UnitOfWorkByProperty
    {
        public override void CommitWork()
        {
            var transaction = Property<DbTransaction>();

            if (transaction != null)
            {
                transaction.Commit();
            }
        }
    }
}
