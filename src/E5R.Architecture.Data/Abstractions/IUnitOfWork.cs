// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

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
