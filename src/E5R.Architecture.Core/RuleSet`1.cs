// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E5R.Architecture.Core.Exceptions;
using static E5R.Architecture.Core.RuleCheckResult;

namespace E5R.Architecture.Core
{
    public class RuleSet<TTarget> : IRuleSet<TTarget> where TTarget : class
    {
        public RuleSet(IServiceProvider serviceProvider)
        {
            Checker.NotNullArgument(serviceProvider, nameof(serviceProvider));

            var services = serviceProvider.GetService(typeof(IEnumerable<IRuleFor<TTarget>>));

            Rules = services as IEnumerable<IRuleFor<TTarget>> ?? Enumerable.Empty<IRuleFor<TTarget>>();
        }

        private RuleSet(IEnumerable<IRuleFor<TTarget>> rules)
        {
            Checker.NotNullArgument(rules, nameof(rules));

            Rules = rules;
        }
        
        private IEnumerable<IRuleFor<TTarget>> Rules { get; set; }

        public IRuleSet<TTarget> ByCode(string code)
        {
            Checker.NotEmptyOrWhiteArgument(code, nameof(code));

            return ByCode(new[] {code});
        }

        public IRuleSet<TTarget> ByCode(string[] codes)
        {
            Checker.NotNullOrEmptyArgument(codes, nameof(codes));
            
            var allMatchedRules = new List<IRuleFor<TTarget>>();
            var matchedCodes = new List<string>();

            codes.ToList().ForEach(code =>
            {
                var matchedRules = Rules
                    .Where(w => string.Equals(w.Code, code))
                    .ToList();

                if (matchedRules.Any())
                {
                    matchedCodes.Add(code);
                }

                allMatchedRules.AddRange(matchedRules);
            });

            if (matchedCodes.Count != codes.Length)
            {
                var noMatchedCodes = codes.Where(c => !matchedCodes.Contains(c));
                    
                // TODO: Implementar i18n/l10n
                throw new InfrastructureLayerException(
                    $"No matches found for {string.Join(", ", noMatchedCodes)}");
            }

            return new RuleSet<TTarget>(allMatchedRules);
        }

        public IRuleSet<TTarget> ByDefaultCategory() => ByCategory(null);
        
        public IRuleSet<TTarget> ByCategory(string category)
        {
            // Não validamos se o parâmetro é nulo propositalmente porque
            // uma categoria nula é considerada categoria padrão
            
            var matchedRules = Rules
                .Where(w => string.Equals(w.Category, category))
                .ToList();

            if (!matchedRules.Any())
            {
                // TODO: Implementar i18n/l10n
                throw new InfrastructureLayerException(
                    $"No matches found for {category ?? "default"} category");
            }

            return new RuleSet<TTarget>(matchedRules);
        }

        public async Task<RuleCheckResult> CheckAsync(TTarget target)
        {
            var unconformities = new Dictionary<string, string>();

            foreach (var rule in Rules)
            {
                try
                {
                    var result = await rule.CheckAsync(target);

                    if (!result.IsSuccess)
                    {
                        if (result.Unconformities != null && result.Unconformities.Any())
                        {
                            result.Unconformities
                                .ToList()
                                .ForEach(u => unconformities.Add($"{rule.Code}:{u.Key}", u.Value));
                        }
                        else
                        {
                            unconformities.Add($"{rule.Code}", nameof(Fail));
                        }
                    }
                }
                catch (Exception ex)
                {
                    unconformities.Add($"{rule.Code}:$exception", ex.Message);
                }
            }

            if (!unconformities.Any())
            {
                return Success;
            }

            return new RuleCheckResult(false, unconformities);
        }

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
            var exceptions = new List<Exception>();

            foreach (var rule in Rules)
            {
                try
                {
                    await EnsureRuleAsync(rule, target, exceptionMessageTemplate);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }
        
        private async Task EnsureRuleAsync(IRuleFor<TTarget> rule, TTarget target,
            string exceptionMessageTemplate = null)
        {
            RuleCheckResult result = null;

            try
            {
                result = await rule.CheckAsync(target);
            }
            catch (Exception ex)
            {
                result = new RuleCheckResult(false, new Dictionary<string, string>
                {
                    {"$exception", ex.Message}
                });
            }

            if (result.IsSuccess)
            {
                return;
            }

            throw !string.IsNullOrWhiteSpace(exceptionMessageTemplate)
                ? new ViolatedRuleException(rule, result.Unconformities, exceptionMessageTemplate)
                : new ViolatedRuleException(rule, result.Unconformities);
        }
    }
}
