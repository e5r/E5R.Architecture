using System.Threading.Tasks;
// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using E5R.Architecture.Core.Exceptions;

namespace E5R.Architecture.Core
{
    /// <summary>
    /// Rule for a target model
    /// <example>
    /// </example>
    /// Suppose you have a simple model like this:
    /// <code>
    /// public class MyModel
    /// {
    ///     public int Number { get; set; }
    ///     public string Name { get; set; }
    /// }
    /// </code>
    /// 
    /// And according to your rules, the number must be between 1 and 99, and the name
    /// must be "Brazil". You can express this in a rule like this:
    /// <code>
    /// public class MyRule1 : RuleFor<MyModel>
    /// {
    ///     public MyRule1()
    ///         : base("RN-1", "My rule 1")
    ///     { }
    /// 
    ///     protected override bool Check(MyModel target)
    ///     {
    ///         if (target.Number < 1 || target.Number > 99)
    ///             return false;
    /// 
    ///         if (target.Name != "Brazil")
    ///             return false;
    /// 
    ///         return true;
    ///     }
    /// }
    /// </code>
    /// 
    /// Now that you have a defined rule, just use it in your methods
    /// and business objects like this:
    /// <code>
    /// public class MyBusinessClass
    /// {
    ///     public void MyMethod1(MyModel model)
    ///     {
    ///         new MyRule1().Ensure(model);
    ///     }
    /// }
    /// </code>
    /// </summary>
    /// <typeparam name="TTarget"></typeparam>
    public abstract class RuleFor<TTarget> : IRule
        where TTarget : class
    {
        public RuleFor(string code, string description)
        {
            Checker.NotEmptyOrWhiteArgument(code, nameof(code));
            Checker.NotEmptyOrWhiteArgument(description, nameof(description));

            Code = code;
            Description = description;
        }

        public string Code { get; private set; }

        public string Description { get; private set; }

        protected abstract Task<bool> CheckAsync(TTarget target, out IDictionary<string, string> unconformities);

        /// <summary>
        /// Ensures the object's compliance with the rules
        /// </summary>
        /// <param name="target">Target object instance</param>
        /// <param name="exceptionMessageTemplate">
        /// The exception message is usually "Rule violation {0}: {1}", where "{0}" is
        /// the rule code, and "{1}" is the rule description.
        /// But you can customize this message by passing the message template
        /// in this parameter.
        /// </param>
        /// <exception cref="ViolatedRuleException">
        /// When the object does not comply with the rules, an exception
        /// </exception>
        public void Ensure(TTarget target, string exceptionMessageTemplate = null)
            => EnsureAsync(target, exceptionMessageTemplate).Wait();

        public async Task EnsureAsync(TTarget target, string exceptionMessageTemplate = null)
        {
            if (!await CheckAsync(target, out IDictionary<string, string> unconformities))
            {
                if (!string.IsNullOrWhiteSpace(exceptionMessageTemplate))
                {
                    throw new ViolatedRuleException(this, unconformities, exceptionMessageTemplate);
                }
                else
                {
                    throw new ViolatedRuleException(this, unconformities);
                }
            }
        }
    }
}
