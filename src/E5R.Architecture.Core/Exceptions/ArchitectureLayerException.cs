// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using static System.Reflection.Assembly;

namespace E5R.Architecture.Core.Exceptions
{
    public class ArchitectureLayerException : Exception
    {
        private const string OwnerHelpLink = "https://github.com/e5r/E5R.Architecture.Docs";

        public ArchitectureLayerException(ArchitectureLayerIdentifier layerId, string message)
            : this(layerId, new Exception(message))
        {
        }

        public ArchitectureLayerException(ArchitectureLayerIdentifier layerId, string message,
            Exception innerException)
            : this(layerId, new Exception(message, innerException))
        {
        }

        public ArchitectureLayerException(ArchitectureLayerIdentifier layerId, Exception exception)
            : base(exception.Message, exception.InnerException)
        {
            Identifier = layerId;
            ComponentInfo = ComponentInformation.MakeFromAssembly(GetCallingAssembly());
        }

        public override string HelpLink => OwnerHelpLink;

        public ComponentInformation ComponentInfo { get; private set; }

        public ArchitectureLayerIdentifier Identifier { get; private set; }
    }
}
