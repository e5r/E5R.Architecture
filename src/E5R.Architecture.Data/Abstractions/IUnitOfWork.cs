using System;

namespace E5R.Architecture.Data.Abstractions
{
    /// <summary>
    /// Unit of Work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves the pending work
        /// </summary>
        void SaveWork();

        /// <summary>
        /// The session object
        /// </summary>
        /// <remarks>Creates if there is not</remarks>
        UnderlyingSession Session { get; }
    }
}
