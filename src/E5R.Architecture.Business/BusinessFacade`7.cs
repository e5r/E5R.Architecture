// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;

namespace E5R.Architecture.Business
{
    public class BusinessFacade<TFeature1, TFeature2, TFeature3, TFeature4, TFeature5, TFeature6,
        TFeature7> : IBusinessFacadeSignature
        where TFeature1 : IBusinessFeatureSignature
        where TFeature2 : IBusinessFeatureSignature
        where TFeature3 : IBusinessFeatureSignature
        where TFeature4 : IBusinessFeatureSignature
        where TFeature5 : IBusinessFeatureSignature
        where TFeature6 : IBusinessFeatureSignature
        where TFeature7 : IBusinessFeatureSignature
    {
        private readonly ILazy<TFeature1> _feature1;
        private readonly ILazy<TFeature2> _feature2;
        private readonly ILazy<TFeature3> _feature3;
        private readonly ILazy<TFeature4> _feature4;
        private readonly ILazy<TFeature5> _feature5;
        private readonly ILazy<TFeature6> _feature6;
        private readonly ILazy<TFeature7> _feature7;

        public BusinessFacade(ILazy<TFeature1> feature1, ILazy<TFeature2> feature2,
            ILazy<TFeature3> feature3, ILazy<TFeature4> feature4, ILazy<TFeature5> feature5,
            ILazy<TFeature6> feature6, ILazy<TFeature7> feature7)
        {
            Checker.NotNullArgument(feature1, nameof(feature1));
            Checker.NotNullArgument(feature2, nameof(feature2));
            Checker.NotNullArgument(feature3, nameof(feature3));
            Checker.NotNullArgument(feature4, nameof(feature4));
            Checker.NotNullArgument(feature5, nameof(feature5));
            Checker.NotNullArgument(feature6, nameof(feature6));
            Checker.NotNullArgument(feature7, nameof(feature7));

            _feature1 = feature1;
            _feature2 = feature2;
            _feature3 = feature3;
            _feature4 = feature4;
            _feature5 = feature5;
            _feature6 = feature6;
            _feature7 = feature7;
        }

        protected TFeature1 Feature1 => _feature1.Value;
        protected TFeature2 Feature2 => _feature2.Value;
        protected TFeature3 Feature3 => _feature3.Value;
        protected TFeature4 Feature4 => _feature4.Value;
        protected TFeature5 Feature5 => _feature5.Value;
        protected TFeature6 Feature6 => _feature6.Value;
        protected TFeature7 Feature7 => _feature7.Value;
    }
}
