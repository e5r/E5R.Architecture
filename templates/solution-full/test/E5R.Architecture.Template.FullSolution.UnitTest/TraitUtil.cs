// Copyright (c) E5R.Architecture.Template.FullSolution. Todos os direitos reservados.
// Configure suas notas de cabeçalho no arquivo ".editorconfig" na raiz da solução.

using Xunit;

namespace E5R.Architecture.Template.FullSolution.UnitTest
{
    /// <summary>
    /// Utilitário para definição de nomes padronizados em <see cref="TraitAttribute"/>
    /// </summary>
    public enum TraitUtil
    {
        /// <summary>
        /// Característica para identificar alvo do teste
        /// </summary>
        Target,

        /// <summary>
        /// Característica para identificar categoria do teste
        /// </summary>
        Category,

        /// <summary>
        /// Característica para teste de entidade
        /// </summary>
        Entity,

        /// <summary>
        /// Característica para teste de serviço
        /// </summary>
        Service,

        /// <summary>
        /// Característica para teste de manipulador (handler)
        /// </summary>
        Handler,

        /// <summary>
        /// Característica para teste de funcionalidade (feature - alias para Handler)
        /// </summary>
        Feature,

        /// <summary>
        /// Característica para teste de regra
        /// </summary>
        Rule
    }
}
