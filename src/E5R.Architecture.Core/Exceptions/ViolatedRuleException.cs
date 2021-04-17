// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace E5R.Architecture.Core.Exceptions
{
    // TODO: Implementar i18n/l10n para evitar os 2 contrutores
    public class ViolatedRuleException : Exception
    {
        const string DEFAULT_MESSAGE_TEMPLATE = "Rule violation {0}: {1}";

        /// <summary>
        /// Create instance of ViolatedRuleException
        /// </summary>
        /// <param name="rule">Violated rule</param>
        public ViolatedRuleException(IRule rule)
            : this(rule, null, DEFAULT_MESSAGE_TEMPLATE)
        { }

        /// <summary>
        /// Create instance of <see cref="ViolatedRuleException" /> with check errors.
        /// </summary>
        /// <param name="rule">Violated rule</param>
        /// <param name="unconformities">Unconformities</param>
        public ViolatedRuleException(IRule rule, IReadOnlyDictionary<string, string> unconformities)
            : this(rule, unconformities, DEFAULT_MESSAGE_TEMPLATE)
        { }

        /// <summary>
        /// Create instance of ViolatedRuleException
        /// </summary>
        /// <param name="rule">Violated rule instance</param>
        /// <param name="messageTemplate">
        /// Exception message template. Use "{0}" for <see cref="IRule.Code" /> and "{1} for <see cref="IRule.Description" />
        /// </param>
        public ViolatedRuleException(IRule rule, string messageTemplate)
            : this(rule, null, messageTemplate)
        { }

        public ViolatedRuleException(IRule rule, IReadOnlyDictionary<string, string> unconformities, string messageTemplate)
            : base(string.Format(messageTemplate, rule.Code, rule.Description))
        {
            Rule = rule;
            Unconformities = unconformities ?? new Dictionary<string, string>();
        }

        public override IDictionary Data => Unconformities.ToDictionary(t => t.Key);

        public IRule Rule { get; }
        public IReadOnlyDictionary<string, string> Unconformities { get; private set; }
    }
}
