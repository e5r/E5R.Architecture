// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Core
{
    public class RuleFor<TTarget> : IRule
        where TTarget : class
    {
        private Func<TTarget, bool> _checker = (_) => throw new NotImplementedException();

        public RuleFor(string code, string description)
        {
            Checker.NotEmptyOrWhiteArgument(code, nameof(code));
            Checker.NotEmptyOrWhiteArgument(description, nameof(description));

            Code = code;
            Description = description;
        }

        protected void Check(Func<TTarget, bool> checker)
        {
            Checker.NotNullArgument(checker, nameof(checker));

            _checker = checker;
        }

        public string Code { get; private set; }

        public string Description { get; private set; }

        public bool IsOk(TTarget target) => _checker(target);
    }
}
