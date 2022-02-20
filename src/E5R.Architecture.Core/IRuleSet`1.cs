// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Threading.Tasks;

namespace E5R.Architecture.Core
{
    public interface IRuleSet<TTarget>
        where TTarget : class
    {
        Task<RuleCheckResult> CheckAsync(TTarget target);

        RuleCheckResult Check(TTarget target);

        void Ensure(TTarget target, string exceptionMessageTemplate = null);

        Task EnsureAsync(TTarget target, string exceptionMessageTemplate = null);

        IRuleSet<TTarget> ByCode(string code);
        IRuleSet<TTarget> OnlyCode(string code);
        
        IRuleSet<TTarget> ByCode(string[] codes);
        IRuleSet<TTarget> OnlyCode(string[] codes);
        
        IRuleSet<TTarget> ByDefaultCategory();
        IRuleSet<TTarget> OnlyDefaultCategory();

        IRuleSet<TTarget> ByCategory(string category);
        IRuleSet<TTarget> OnlyCategory(string category);
    }
}
