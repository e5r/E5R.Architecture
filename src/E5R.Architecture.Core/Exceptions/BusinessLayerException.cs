// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Core.Exceptions
{
    using Core;
    using static Core.ArchitectureLayerIdentifier;

    public class BusinessLayerException : ArchitectureLayerException
    {
        public BusinessLayerException(string message) : base(BusinessLayer, message)
        {
        }

        public BusinessLayerException(string message, Exception innerException) : base(
            BusinessLayer, message, innerException)
        {
        }

        public BusinessLayerException(Exception exception) : base(BusinessLayer, exception)
        {
        }
    }
}
