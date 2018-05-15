// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Core
{
    public class ArchitectureLayerException : Exception
    {
        private const string OwnerHelpLink = "https://github.com/e5r/E5R.Architecture.Docs";
        private ComponentInformation _info;

        private readonly ArchitectureLayerIdentifier _layerId;

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
            _layerId = layerId;

            MakeInformation();
        }

        public override string HelpLink => OwnerHelpLink;

        private void MakeInformation()
        {
            throw new NotImplementedException();
        }
    }
}
