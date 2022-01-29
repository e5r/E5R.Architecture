// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E5R.Architecture.Core
{
    public interface IRuleModelValidator
    {
        /// <summary>
        /// Validates a model based on the rules linked to it
        /// </summary>
        /// <param name="model">The model instance</param>
        /// <returns>List of validation results</returns>
        IEnumerable<ValidationResult> Validate<TModel>(TModel model)
            where TModel : IValidatableObject;
    }
}
