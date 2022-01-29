// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E5R.Architecture.Core
{
    public class RuleModelValidator : IRuleModelValidator
    {
        private readonly IServiceProvider _serviceProvider;
        
        public RuleModelValidator(IServiceProvider serviceProvider)
        {
            Checker.NotNullArgument(serviceProvider, nameof(serviceProvider));
            
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<ValidationResult> Validate<TModel>(TModel model)
            where TModel : IValidatableObject
        {
            Checker.NotNullArgument(model, nameof(model));

            var validationContext = new ValidationContext(model, _serviceProvider, null);

            return model.Validate(validationContext);
        }
    }
}
