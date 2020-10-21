// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Transactions;

namespace E5R.Architecture.Infrastructure
{
    using Abstractions;

    public class UnitOfWorkByTransactionScope : IUnitOfWork, IDisposable
    {
        private readonly TransactionScope _scope;
        private bool _disposed;

        public UnitOfWorkByTransactionScope()
        {
            // TODO: Fornecer mais opções de controle da transação
            // TODO: Criar versão que já receba uma Transaction construída
            _scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }
            );
        }

        public void SaveWork() => _scope.Complete();

        public void DiscardWork()
        {
            // Nada precisa ser feito aqui, porque o TransactionManager se encarrega
            // do resto. Só precisamos implementar o método porque é requerido pela
            // interface IUnitOfWork
        }

        public void Dispose() => Dispose(true);

        void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _scope.Dispose();
            }

            _disposed = true;
        }
    }
}
