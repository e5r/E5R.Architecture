// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

namespace E5R.Architecture.Infrastructure.Abstractions
{
    /// <summary>
    /// Unit of Work
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves the pending work
        /// </summary>
        void SaveWork();

        /// <summary>
        /// Discard the pending work
        /// </summary>
        void DiscardWork();
    }
}
