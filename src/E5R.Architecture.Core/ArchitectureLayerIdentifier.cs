// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

namespace E5R.Architecture.Core
{
    public class ArchitectureLayerIdentifier
    {
        private static ArchitectureLayerIdentifier _businessLayer;
        private static ArchitectureLayerIdentifier _dataLayer;
        private static ArchitectureLayerIdentifier _presentationLayer;
        private static ArchitectureLayerIdentifier _crossCuttingLayer;

        public string Id { get; set; }
        public string Desctiption { get; set; }

        public static ArchitectureLayerIdentifier BusinessLayer
            => _businessLayer ?? (_businessLayer = new ArchitectureLayerIdentifier
            {
                Id = "ArchitectureLayer.Business",
                Desctiption = "Business Layer"
            });

        public static ArchitectureLayerIdentifier DataLayer
            => _dataLayer ?? (_dataLayer = new ArchitectureLayerIdentifier
            {
                Id = "ArchitectureLayer.Data",
                Desctiption = "Data Layer"
            });

        public static ArchitectureLayerIdentifier InfrastructureLayer
            => _dataLayer ?? (_dataLayer = new ArchitectureLayerIdentifier
            {
                Id = "ArchitectureLayer.Infrastructure",
                Desctiption = "Infrastructure Layer"
            });

        public static ArchitectureLayerIdentifier PresentationLayer
            => _presentationLayer ?? (_presentationLayer = new ArchitectureLayerIdentifier
            {
                Id = "ArchitectureLayer.Presentation",
                Desctiption = "Presentation Layer"
            });

        public static ArchitectureLayerIdentifier CrossCuttingLayer
            => _crossCuttingLayer ?? (_crossCuttingLayer = new ArchitectureLayerIdentifier
            {
                Id = "ArchitectureLayer.CrossCutting",
                Desctiption = "Cross Cutting Layer"
            });
    }
}
