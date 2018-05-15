// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Data.Exceptions
{
    using Core;
    using static Core.ArchitectureLayerDefaults;

    public class DataLayerException : ArchitectureLayerException
    {
        public DataLayerException(string message) : base(DataLayer, message)
        {
        }

        public DataLayerException(string message, Exception innerException) : base(
            DataLayer, message, innerException)
        {
        }

        public DataLayerException(Exception exception) : base(DataLayer, exception)
        {
        }
    }
}
