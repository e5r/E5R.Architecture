// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    using Core;
    using Infrastructure;
    using Infrastructure.Abstractions;

    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private readonly TContext _context;

        private UnderlyingSession _session;
        private bool _disposed;
        private bool _sessionStarted;

        public UnitOfWork(TContext context)
        {
            Checker.NotNullArgument(context, nameof(context));

            _context = context;
        }

        public void SaveWork()
        {
            if (_sessionStarted)
            {
                _context.Database.CurrentTransaction.Commit();
            }

            _session = null;
            _sessionStarted = false;
        }

        public UnderlyingSession Session
        {
            get
            {
                if (_session == null)
                {
                    _session = new UnderlyingSession(_context);
                }

                if (_sessionStarted)
                {
                    return _session;
                }

                _context.Database.BeginTransaction();
                _sessionStarted = true;

                return _session;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            _disposed = true;

            if (!disposing) return;

            if (_sessionStarted)
            {
                _context.Database.CurrentTransaction.Rollback();
            }

            _session = null;
            _sessionStarted = false;
        }
    }
}
