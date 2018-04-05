using E5R.Architecture.Data;

namespace UsingData
{
    internal static class MemorySession
    {
        internal static UnderlyingSession CreateSession()
        {
            return new UnderlyingSession(new MemoryDatabase());
        }
    }
}