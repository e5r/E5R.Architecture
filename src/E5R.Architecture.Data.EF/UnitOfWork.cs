using System;
using E5R.Architecture.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace E5R.Architecture.Data.EF
{
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private UnderlyingSession _session;
        private readonly TContext _context;

        public UnitOfWork(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _session = null;
        }

        public void SaveWork()
        {
            if (_context.ChangeTracker.HasChanges())
            {
                _session.Get<IDbContextTransaction>().Commit();
            }

            _session = null;
        }

        public UnderlyingSession Session =>
            _session ??
            (_session =
                new UnderlyingSession(_context.Database
                    .BeginTransaction()));
    }
}
