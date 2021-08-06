// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Threading.Tasks;
using System.Collections.Generic;
using E5R.Architecture.Core.Exceptions;
using System;

namespace E5R.Architecture.Core
{
    public abstract class RuleFor<TTarget> : IRuleFor<TTarget>
        where TTarget : class
    {
        public RuleFor(string code, string description)
        {
            Checker.NotEmptyOrWhiteArgument(code, nameof(code));
            Checker.NotEmptyOrWhiteArgument(description, nameof(description));

            Code = code;
            Category = null;
            Description = description;
        }
        
        public RuleFor(string code, string category, string description)
        {
            Checker.NotEmptyOrWhiteArgument(code, nameof(code));
            Checker.NotEmptyOrWhiteArgument(category, nameof(category));
            Checker.NotEmptyOrWhiteArgument(description, nameof(description));

            Code = code;
            Category = category;
            Description = description;
        }

        public string Code { get; private set; }

        public string Category { get; private set; }
        
        public string Description { get; private set; }

        public abstract Task<RuleCheckResult> CheckAsync(TTarget target);
        
        public RuleCheckResult Check(TTarget target)
        {
            try
            {
                var task = CheckAsync(target);

                task.Wait();

                return task.Result;
            }
            catch (Exception ex)
            {
                return new RuleCheckResult(false, new Dictionary<string, string>
                {
                    {"$exception", ex.Message}
                });
            }
        }

        public void Ensure(TTarget target, string exceptionMessageTemplate = null)
            => EnsureAsync(target, exceptionMessageTemplate).Wait();

        public async Task EnsureAsync(TTarget target, string exceptionMessageTemplate = null)
        {
            RuleCheckResult result = null;

            try
            {
                result = await CheckAsync(target);
            }
            catch (Exception ex)
            {
                result = new RuleCheckResult(false, new Dictionary<string, string>
                {
                    { "$exception", ex.Message }
                });
            }

            if (result.IsSuccess)
            {
                return;
            }

            throw !string.IsNullOrWhiteSpace(exceptionMessageTemplate)
                ? new ViolatedRuleException(this, result.Unconformities, exceptionMessageTemplate)
                : new ViolatedRuleException(this, result.Unconformities);
        }

        protected Task<RuleCheckResult> Success() => Task.FromResult(RuleCheckResult.Success);
        
        protected Task<RuleCheckResult> FailWithoutUnconformities() => Task.FromResult(RuleCheckResult.Fail);

        protected Task<RuleCheckResult> Fail()
        {
            return Task.FromResult(new RuleCheckResult(false,
                new Dictionary<string, string> {{Code, Description}}));
        }

        protected Task<RuleCheckResult> Fail(string unconformityKey, string unconformityValue)
        {
            Checker.NotEmptyOrWhiteArgument(unconformityKey, nameof(unconformityKey));
            Checker.NotEmptyOrWhiteArgument(unconformityValue, nameof(unconformityValue));
            
            return Task.FromResult(new RuleCheckResult(false,
                new Dictionary<string, string> {{unconformityKey, unconformityValue}}));
        }
        
        protected Task<RuleCheckResult> Fail(IReadOnlyDictionary<string,string> unconfomities)
        {
            Checker.NotNullArgument(unconfomities, nameof(unconfomities));

            return Task.FromResult(new RuleCheckResult(false, unconfomities));
        }
    }
}
