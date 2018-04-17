using System;
using E5R.Architecture.Core;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    using Abstractions;

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
            if (_context.ChangeTracker.HasChanges())
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

            if (_context.ChangeTracker.HasChanges())
            {
                _context.Database.CurrentTransaction.Rollback();
            }

            _session = null;
        }
    }
}
