namespace E5R.Architecture.Core
{
    public static class ArchitectureLayerDefaults
    {
        private static ArchitectureLayerIdentifier _businessLayer;
        private static ArchitectureLayerIdentifier _dataLayer;
        private static ArchitectureLayerIdentifier _presentationLayer;
        private static ArchitectureLayerIdentifier _crossCuttingLayer;

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
