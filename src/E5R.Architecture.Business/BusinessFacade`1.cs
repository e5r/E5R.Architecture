// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;

namespace E5R.Architecture.Business
{
    public class BusinessFacade<TFeature1> : IBusinessFacadeSignature
        where TFeature1 : IBusinessFeatureSignature
    {
        private readonly ILazy<TFeature1> _feature1;

        public BusinessFacade(ILazy<TFeature1> feature1)
        {
            Checker.NotNullArgument(feature1, nameof(feature1));

            _feature1 = feature1;
        }

        protected TFeature1 Feature1 => _feature1.Value;
    }
}
