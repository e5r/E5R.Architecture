// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Core.Exceptions
{
    using Core;
    using static Core.ArchitectureLayerDefaults;

    public class CrossCuttingLayerException : ArchitectureLayerException
    {
        public CrossCuttingLayerException(string message) : base(CrossCuttingLayer, message)
        {
        }

        public CrossCuttingLayerException(string message, Exception innerException) : base(
            CrossCuttingLayer, message, innerException)
        {
        }

        public CrossCuttingLayerException(Exception exception) : base(CrossCuttingLayer, exception)
        {
        }
    }
}
