// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// A underlying session representation of Unit of Work 
    /// </summary>
    public class UnderlyingSession
    {
        private readonly object _session;

        public UnderlyingSession(object session)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        /// <summary>
        /// Converts internal object to expected type
        /// </summary>
        /// <returns>Reference to internal object converted to expected type</returns>
        /// <exception cref="InvalidCastException">When internal type does not match</exception>
        public TExpected Get<TExpected>() where TExpected : class
        {
            return (TExpected) _session;
        }
    }
}
