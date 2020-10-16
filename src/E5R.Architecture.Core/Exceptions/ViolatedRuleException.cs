// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Core.Exceptions
{
    // TODO: Implementar internacionalização para evitar os 2 contrutores
    public class ViolatedRuleException : Exception
    {
        public IRule Rule { get; }

        /// <summary>
        /// Create instance of ViolatedRuleException
        /// </summary>
        /// <param name="rule">Violated rule instance</param>
        public ViolatedRuleException(IRule rule)
            : base($"Rule violation { rule.Code }: { rule.Description }")
        {
            Rule = rule;
        }

        /// <summary>
        /// Create instance of ViolatedRuleException
        /// </summary>
        /// <param name="rule">Violated rule instance</param>
        /// <param name="messageTemplate">
        /// Exception message template. Use "{0}" for <see cref="IRule.Code" /> and "{1} for <see cref="IRule.Description" />
        /// </param>
        public ViolatedRuleException(IRule rule, string messageTemplate)
            : base(string.Format(messageTemplate, rule.Code, rule.Description))
        {
            Rule = rule;
        }
    }
}
