// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace E5R.Architecture.Core
{
    public class RuleValidatableObject<TTarget> : IValidatableObject
        where TTarget : class
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            Checker.NotNullArgument(validationContext, nameof(validationContext));

            if (validationContext.ObjectType != typeof(TTarget))
            {
                // TODO: Implementar i18n/l10n
                throw new InvalidOperationException(
                    "Target type for validation different from linked rule target type");
            }

            var rules =
                validationContext.GetService(typeof(IEnumerable<IRuleFor<TTarget>>)) as
                    IEnumerable<IRuleFor<TTarget>>;

            if (rules == null)
            {
                return Enumerable.Empty<ValidationResult>();
            }

            return rules
                .Select(r =>
                {
                    // TODO: Revisar a necessisdade do try-catch aqui
                    // Se tivermos a possibilidade de usar IRuleSet<X> que retorne a lista de
                    // resultados (RuleCheckResult), e no resultado conter a regra vinculada, aqui
                    // não precisaremos do try-catch, tendo em vista que isso já é feito no método
                    // implementado de IRuleSet<X> que garante sempre uma validação silenciosa quando
                    // a exceções não tratadas.
                    try
                    {
                        return (r,
                            r.CheckAsync(validationContext.ObjectInstance as TTarget).GetAwaiter()
                                .GetResult());
                    }
                    catch (Exception ex)
                    {
                        return (r, new RuleCheckResult(ex));
                    }
                })
                .Where(w => !w.Item2.IsSuccess)
                .SelectMany(sm =>
                {
                    var results = new List<ValidationResult>();

                    results.AddRange(sm.Item2.Unconformities.Select(s =>
                        new ValidationResult(s.Value, new[] {s.Key})));

                    if (sm.Item2.UnexpectedException != null)
                    {
                        // TODO: Revisar formato da mensagem, talvez até dando ao usuário a possibilidade de definir o formato
                        results.Add(new ValidationResult(
                            $"{sm.r.Code}: {sm.r.Description}/{sm.Item2.UnexpectedException.Message}"));
                    }
                    
                    // Caso a regra tenha simplesmente falhado sem disponibilizar uma exceção não esperada
                    // ou uma lista de inconformidades, falhamos a validação da mesma forma, porém somente
                    // com a menção da regra que falhou
                    if (sm.Item2.UnexpectedException == null &&
                        !(bool) sm.Item2.Unconformities?.Any())
                    {
                        // TODO: Revisar formato da mensagem, talvez até dando ao usuário a possibilidade de definir o formato
                        results.Add(new ValidationResult($"{sm.r.Code}: {sm.r.Description}"));
                    }

                    return results;
                });
        }
    }
}
