using E5R.Architecture.Data;
using E5R.Architecture.Data.Abstractions;

namespace UsingData
{
    using static MemorySession;

    public class MemoryUnitOfWork : IUnitOfWork
    {
        private UnderlyingSession _session;

        public void SaveWork()
        {
            /* SILENT PASS THRU */
        }

        public UnderlyingSession Session
            => _session ?? (_session = CreateSession());
    }
}