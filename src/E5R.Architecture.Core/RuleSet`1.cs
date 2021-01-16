// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;

namespace E5R.Architecture.Core
{
    /// <summary>
    /// Rule set for a target model
    /// <example>
    /// You can create a base class that encapsulates all the necessary rules.
    /// <code>
    /// public class MyRuleSet1 : RuleSet<MyModel>
    /// {
    ///     public MyRuleSet1()
    ///     {
    ///         Conform<MyRule1>();
    ///         Conform<MyRule2>();
    ///         Conform<MyRule3>();
    ///     }
    /// }
    /// </code>
    /// 
    /// You can now use this class to check the rules on any object.
    /// <code>
    /// public class MyBusinessClass
    /// {
    ///     public void MyMethod1(MyModel model)
    ///     {
    ///         new MyRuleSet1().Ensure(model);
    ///     }
    /// }
    /// </code>
    /// 
    /// If you prefer, you can create an <see cref="RuleSet{}" /> and
    /// use it immediately with the rules you prefer.
    /// <code>
    /// public class MyBusinessClass
    /// {
    ///     public void MyMethod1(MyModel model)
    ///     {
    ///         var rules = new RuleSet<MyModel>()
    ///             .Conform<MyRule2>()
    ///             .Conform<MyRule3>();
    ///
    ///         rules.Ensure(model);
    ///     }
    /// }
    /// </code>
    /// </example>
    /// </summary>
    /// <typeparam name="TTarget">Type that must comply with the rules</typeparam>
    public class RuleSet<TTarget>
        where TTarget : class
    {
        private readonly List<RuleFor<TTarget>> _rules = new List<RuleFor<TTarget>>();

        public RuleSet<TTarget> Conform<TRule>()
            where TRule : RuleFor<TTarget>, new()
        {
            _rules.Add(
                Activator.CreateInstance(typeof(TRule)) as RuleFor<TTarget>
            );

            return this;
        }

        public void Ensure(TTarget target, string exceptionMessageTemplate = null)
            => _rules.ForEach(r => r.Ensure(target, exceptionMessageTemplate));
    }
}
