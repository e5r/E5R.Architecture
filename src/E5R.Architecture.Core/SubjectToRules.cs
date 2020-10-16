// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using E5R.Architecture.Core.Exceptions;

namespace E5R.Architecture.Core
{
    /// <summary>
    /// Object subject to a set of rules
    /// <example>For example:
    /// <code>
    ///    public class MyRule1 : RuleFor<MyModel>
    ///    {
    ///        public MyRule1()
    ///            : base("RA-1", "My rule 1")
    ///        {
    ///            Check(m =>
    ///            {
    ///                if (m.Id < 1 || m.Id > 99)
    ///                    return false;
    ///
    ///                if (m.Name != "Brazil")
    ///                    return false;
    ///
    ///                 return true;
    ///            });
    ///        }
    ///    }
    /// 
    ///    public class MyModel : SubjectToRules<MyModel>
    ///    {
    ///        public MyModel()
    ///        {
    ///            Conform<MyRule1>();
    ///        }
    ///    
    ///        public int Id { get; set; }
    ///        public string Name { get; set; }
    ///    }
    /// </code>
    /// </example>
    /// </summary>
    /// <typeparam name="TTargetObject">Type that inherits <see cref="SubjectToRules{}" /></typeparam>
    public class SubjectToRules<TTargetObject>
        where TTargetObject : class
    {
        private string _violatedExceptionMessageTemplate = null;
        private List<RuleFor<TTargetObject>> _rules = new List<RuleFor<TTargetObject>>();

        protected void Conform<TRule>()
            where TRule : RuleFor<TTargetObject>, new()
        {
            if (_rules.Any(r => r.GetType() == typeof(TRule)))
            {
                // TODO: Implementar internacionalização
                throw new InvalidOperationException($"Rule {typeof(TRule).FullName} has already been registered");
            }

            var rule = Activator.CreateInstance<TRule>();

            _rules.Add(rule);
        }

        /// <summary>
        /// Set a violated rule exception message template
        /// </summary>
        /// <param name="messageTemplate">
        /// Exception message template. Use "{0}" for <see cref="IRule.Code" /> and "{1} for <see cref="IRule.Description" />
        /// </param>
        protected void SetViolatedRuleMessageTemplate(string messageTemplate)
        {
            Checker.NotEmptyOrWhiteArgument(messageTemplate, nameof(messageTemplate));

            _violatedExceptionMessageTemplate = messageTemplate;
        }

        public virtual void EnsureRules()
        {
            _rules.ForEach(rule =>
            {
                // HACK: Conferir uma melhor forma com <Generic>
                if (!rule.IsOk(this as TTargetObject))
                {
                    if (!string.IsNullOrWhiteSpace(_violatedExceptionMessageTemplate))
                    {
                        throw new ViolatedRuleException(rule, _violatedExceptionMessageTemplate);
                    }
                    else
                    {
                        throw new ViolatedRuleException(rule);
                    }
                }
            });
        }
    }
}
