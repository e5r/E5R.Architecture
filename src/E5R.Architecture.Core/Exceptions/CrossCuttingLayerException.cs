// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using static E5R.Architecture.Core.ArchitectureLayerIdentifier;

namespace E5R.Architecture.Core.Exceptions
{
    public class CrossCuttingLayerException : ArchitectureLayerException
    {
        public CrossCuttingLayerException(string message) : base(CrossCuttingLayer, message)
        { }

        public CrossCuttingLayerException(string message, Exception innerException) : base(
            CrossCuttingLayer, message, innerException)
        { }

        public CrossCuttingLayerException(Exception exception) : base(CrossCuttingLayer, exception)
        { }
    }
}
