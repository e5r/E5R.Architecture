// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using static E5R.Architecture.Core.ArchitectureLayerIdentifier;

namespace E5R.Architecture.Core.Exceptions
{
    public class PresentationLayerException : ArchitectureLayerException
    {
        public PresentationLayerException(string message) : base(PresentationLayer, message)
        { }

        public PresentationLayerException(string message, Exception innerException) : base(
            PresentationLayer, message, innerException)
        { }

        public PresentationLayerException(Exception exception) : base(PresentationLayer, exception)
        { }
    }
}
