// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace E5R.Architecture.Business.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessFeatures(
            this IServiceCollection serviceCollection,
            string[] customServiceAssemblies = null)
        {
            // Forçamos o carregamento dos assemblies informados.
            //
            // NOTE: Isso é necessário porque o otimizador de compiladores como
            //       Roslyn "removem" referências de objetos não utilizados.
            //       O efeito colateral disto é que mesmo que você tenha um projeto
            //       referenciado mas não utilize explicitamente nenhum objeto dessa
            //       referência, o assembly não estará disponível no AppDomain.
            //       Com isso, não conseguiríamos encontrar objetos para registrar
            //       aqui. Por isso, carregamos assemblies customizados.
            customServiceAssemblies?.ToList().ForEach(n => AppDomain.CurrentDomain.Load(n));

            // Garantimos o registro pelo menos do TransformationManager e da infraestrutura padrão
            // porque as características de negócio, bem como as fachadas dependem de
            // ITransformationManager e ILazy<>
            serviceCollection.AddTransformationManager();
            serviceCollection.AddInfrastructure(customServiceAssemblies);
            
            AppDomain.CurrentDomain.AddBusinessFeatures(serviceCollection);

            return serviceCollection;
        }
    }
}
