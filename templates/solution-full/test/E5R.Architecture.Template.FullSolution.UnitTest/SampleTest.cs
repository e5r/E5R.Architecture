using Xunit;

using static E5R.Architecture.Template.FullSolution.UnitTest.TraitUtil;

namespace E5R.Architecture.Template.FullSolution.UnitTest
{
    /// <summary>
    /// Remova este exemplo de teste unitário e faça os seus próprios
    /// </summary>
    /// <remarks>
    /// Neste arquivo de teste estamos exemplificando um padrão que você pode seguir
    /// ao formular seus testes (ou não). O uso de <see cref="TraitAttribute"/> ajuda
    /// a organizar seus testes nas interfaces de exploradores de testes nas IDE's
    /// mais modernas, além de permitir um filtro de testes durante suas rotinas
    /// de construção via integração contínua.
    /// </remarks>

    // Sempre defina o alvo sendo testado. Normalmente (mas nem sempre) uma classe
    // de teste para cada classe alvo.
    [Trait(nameof(Target), nameof(TraitUtil))]

    // É uma boa prática identificar o tipo do alvo nas unidades mais relevantes
    // de um projeto. Neste caso estamos dizendo que "TraitUtil" é uma regra
    // (o que obviamente não é verdadeiro, mas serve como exemplo)
    [Trait(nameof(Rule), nameof(TraitUtil))]

    // É uma boa prática categorizar seus testes
    [Trait(nameof(Category), "Exemplos")]
    public class SampleTest
    {
        // É uma boa prática configurar o nome de exibição para algo mais legível aos
        // humanos. Assim você não precisa se preocupar com os nomes dos métodos.
        [Fact(DisplayName = "Nomes são bem definidos")]
        public void Nomes_Sao_Bem_Definidos()
        {
            Assert.Equal("Category", nameof(Category));
            Assert.Equal("Entity", nameof(Entity));
            Assert.Equal("Feature", nameof(Feature));
            Assert.Equal("Handler", nameof(Handler));
            Assert.Equal("Rule", nameof(Rule));
            Assert.Equal("Service", nameof(Service));
            Assert.Equal("Target", nameof(Target));
        }
    }
}
