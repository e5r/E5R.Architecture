// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;

namespace E5R.Architecture.Business
{
    public class BusinessFacade<TFeature1, TFeature2> : IBusinessFacadeSignature
        where TFeature1 : IBusinessFeatureSignature
        where TFeature2 : IBusinessFeatureSignature
    {
        private readonly ILazy<TFeature1> _feature1;
        private readonly ILazy<TFeature2> _feature2;

        public BusinessFacade(ILazy<TFeature1> feature1, ILazy<TFeature2> feature2)
        {
            Checker.NotNullArgument(feature1, nameof(feature1));
            Checker.NotNullArgument(feature2, nameof(feature2));

            _feature1 = feature1;
            _feature2 = feature2;
        }

        protected TFeature1 Feature1 => _feature1.Value;
        protected TFeature2 Feature2 => _feature2.Value;
    }
}
