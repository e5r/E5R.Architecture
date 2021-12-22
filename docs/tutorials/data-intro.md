---
title: Tutorial - Introdução a manipulação de dados
---

Introdução a manipulação de dados
=================================

Para acompanhar este tutorial, sugiro que você tenha uma aplicação qualquer
que esteja utilizando o [EntityFrameworkCore](https://github.com/dotnet/efcore),
você também precisará instalar o componente `E5R.Architecture.Data.EntityFrameworkCore`
nesse seu projeto.

> Normalmente utilizamos o acesso ao banco seguindo o padrão [Unit Of Work](https://martinfowler.com/eaaCatalog/unitOfWork.html)
> mas aqui vamos nos concentrar somente nos métodos de acesso e manipulação
> dos dados em si. Em outro tutorial podemos falar sobre o conceito de
> [Unit Of Work](./unit-of-work-intro.md) conforme costumamos abordar nessa arquitetura.

## Operações CRUD 

As operações de CRUD estão disparadas na frente como sendo as partes de código
que mais passamos tempo escrevendo em nossas aplicações corporativas de software.
E normalmente costumamos realizar essas operações seguindo o padrão [Repository](https://martinfowler.com/eaaCatalog/repository.html).

O nome **CRUD** é uma sigla formada pela junção das palavras:

* **C**reate - Para criar novos objetos
* **R**ead - Para ler (ou carregar) objetos armazenados
* **U**pdate - Para atualizar (alterar ou substituir) objetos armazenados
* **D**elete - Para remover (apagar, ou _deletar_) objetos armazenados

O componente gernérico `IStorage<TDataModel>` que se encontra no namespace `E5R.Architecture.Data.Abstractions`
nos fornece algumas funcionalidades convenientes para tornar essa tarefa mais fácil.
Aqui no exemplo vamos utilizar a implementação para [EntityFrameworkCore](https://github.com/dotnet/efcore)
que é fornecida pelo componente `E5R.Architecture.Data.EntityFrameworkCore`.

E antes de mais nada, para que daqui em diante possamos nos concentrar mais no código
do exemplo em si e menos nos outros detalhes, vamos combinar que os exemplos de código apresentados
residem em uma ação qualquer de uma controller padrão do [ASP.NET](https://github.com/dotnet/aspnetcore),
e que as dependências foram injetadas por parâmetro ([Injeção de Dependências](https://martinfowler.com/articles/injection.html#FormsOfDependencyInjection)).

Então segue aqui nosso esboço com controlador e um modelo de dado:

```cs
// AlunoController.cs
public class AlunoController : Controller
{
    private ILogger<AlunoController> _logger;
    private readonly IStorage<AlunoDataModel> _storage;

    public AlunoController(ILogger<AlunoController> logger, IStorage<Aluno> storage)
    {
        Checker.NotNullArgument(logger, nameof(logger));
        Checker.NotNullArgument(storage, nameof(storage));

        _logger_ = logger;
        _storage = storage;
    }

    public IActionResult MyAction()
    {
        // Nosso código de exemplo aqui
    }
}

// AlunoDataModel.cs
public class AlunoDataModel : IDataModel
{
    public int AlunoId { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public DateTime DataInscricao { get; set; }

    public object[] IdentifierValues => new[] { (object)AlunoId };
}
```

## Create (Criar)

A primeira operação que vamos executar é **criar** um novo objeto em nosso
armazenamento, isso seria um _INSERT_ a nível de banco de dados relacional.

Então você pode criar um novo aluno assim:
```cs
var alunoCriado = _storage.Create(new Aluno
    {
        Nome = "Erlimar",
        Sobrenome = "Silva Campos",
        DataInscricao = DateTime.Now
    });

_logger.LogDebug($"Novo aluno criado com ID: {alunoCriado.AlunoId}");
```

Essa é a assinatura de nosso método `Create()`:
[!code-csharp[](../../src/E5R.Architecture.Data/Abstractions/ICreatableStorage.cs?range=23)]

Sem mais _delongas_ e é isso aí! Você acaba de criar um aluno no armazenamento.

## Read (Ler)

Agora que já temos um aluno criado, podemos recuperá-lo a qualquer momento
de forma direta, desde que saibamos seu identificador, no caso, o campo **AlunoId**.
Vamos supor que o aluno que acabamos de criar tem o identificador `1`.

Recupere-o simplesmente assim:
```cs
var aluno = _storage.Find(1);

if (aluno == null)
{
    _logger.LogDebug("Aluno não encontrado!");
}

_logger.LogDebug($"Encontramos o aluno {aluno.Nome} {aluno.Sobrenome");
```

Essa é a assinatura de nosso método `Find()`:
[!code-csharp[](../../src/E5R.Architecture.Data/Abstractions/IFindableStorage.cs?range=24)]

Mas temos extensões que permitem chamar o método com somente um objeto (como no exemplo) ou
usando a própria entidade:
[!code-csharp[](../../src/E5R.Architecture.Data/Extensions/StorageExtensions.cs?range=92,108)]

> Ignore por enquanto o parâmetro `IDataIncludes includes`, até porque ele é opcional,
> e também porque estaremos vendo sobre isso em outro tutorial.

A primeira opção (`Find(object identifier)`) é bem mais simples, por isso a usamos, e não tem porque
não usá-la sempre que possível. Mas você ainda poderia recuperá-lo de outras formas sabendo o
identificador. Essas outras alternativas estão aí para te ajudar em cenários específicos.

Por exemplo, quando você tem um dado com [chave composta](https://pt.wikipedia.org/wiki/Chave_prim%C3%A1ria)
faz sentido usar a lista de chaves:
```cs
// Passando um array com os identificadores
_storage.Find(new[] { 2, 7 });
```

Se forem muitas chaves, esse código talvez fique não muito claro:
```cs
_storage.Find(new[] { 2, 7, 8, 12 });
```

Nesse caso talvez faça sentido usar a assinatura que usa o modelo:
```cs
// Passando um modelo de dados com o campo (ou campos) de identificação preenchidos
_storage.Find(new MatriculaDataModel
{
    AlunoId = 2,
    CursoId = 7,
    PeriodoId = 8,
    InscricaoId = 12
});
```

Obviamente a parte **Read** do _CRUD_ é a mais extensa que temos para tratar,
porque dificilmente você irá ler objetos somente de forma direta por seu
_identificador_, natualmente você estará listando todos os registros para formar
uma lista; ou estará fazendo uma pesquisa e então necessitará filtrar os registros;
e ainda tem ordenamento, agrupamento, inclusão de dados relacionados, paginação e etc.

Não vou tratar de todos esses detalhes neste tutorial para não deixá-lo extenso
demais, vamos focar nas coisas mais básicas e em outros tutoriais continuamos nos
aprofundando em cada uma das possibilidades.

Mas para não ficar somente no `Find()`, vamos ver pelo menos mais uma última opção.
Então, se você quiser listar todos os alunos, pode fazê-lo assim:

```cs
var todosAlunos = _storage.GetAll();
```

O método `GetAll()` retorna um enumerável contendo todos os alunos contidos no
armazenamento.

E essa é a assinatura do método `GetAll()`:
[!code-csharp[](../../src/E5R.Architecture.Data/Abstractions/IAcquirableStorage.cs?range=37)]

> Mais uma vez desconsideremos o parâmetro `IDataIncludes includes` por enquanto.

Veremos as outras opções em outros tutoriais, por enquanto você já sabe como
obter um objeto específico por seu identificador, e também já consegue listar
todos os objetos armazenados, ou seja, já sabe ler objetos.

## Update (Atualizar)

Quando nos referimos a **Update** ao falar sobre **CRUD** na verdade queremos dizer
alguma das duas opções:

1) **Atualizar** as informações, e isso quer dizer que iremos alterar _parte_ dos dados,
o que nos leva a entender que não precisamos informar todos os dados, mas somente
aqueles que desejamos alterar.

2) Mas também por vezes queremos **Substituir** o objeto inteiro, o que nos leva a
entender que todas as informações devem ser fornecidas.

Logo, se você quisesse substituir completamente as informações do aluno que criamos
logo no início deste tutorial, poderia fazê-lo assim:
```cs
var aluno = _storage.Replace(new Aluno
    {
        AlunoId = 1,
        Nome = "Erlimar",
        Sobrenome = "Campos",
        DataInscricao = DateTime.Now
    });

_logger.LogDebug($"O aluno agora se chama {aluno.Nome} {aluno.Sobrenome}");
```

Na prática, essa ação é feita em conjunto com uma consulta anterior para obter um
objeto no banco, normalmente encontrada por seu identificador, e logo em seguida
fazemos a substituição. Assim:
```cs
var aluno = _storage.Find(1);

aluno.Sobrenome = "Campos";

_storage.Replace(aluno);

_logger.LogDebug($"O aluno agora se chama {aluno.Nome} {aluno.Sobrenome}");
```

Observe que no exemplo que acabamos de usar, na prática só estamos alterando o `Sobrenome`
do aluno, a `DataInscricao` não faria sentido alterar aqui. O que indica que na verdade não
precisaríamos _substituir_ o objeto, mas somente _atualizar_ alguns dados.

E isso poderia ser feito de forma mais simples, assim:
```cs
var aluno = _storage.Update(1, new 
    {
        Sobrenome = "Campos"
    });

_logger.LogDebug($"O aluno agora se chama {aluno.Nome} {aluno.Sobrenome}");
```

Aqui nós informamos o **identificador** do aluno e em seguida um objeto anônimo contendo
as propriedades que desejamos atualizar, com isso os dados desnecessários podem ser omitidos.

> Vale ressaltar que as propriedades que serão atualizadas devem ser passados com mesmo
> nome e tipo, e caso algum desses detalhes não seja observado o registro não sofrerá
> atualização, e neste caso uma exceção será levantada informando que, não existem dados
> para atualização.

Você também pode compor os dados que serão atualizados com os dados atuais do próprio objeto.
Imagine a situação onde você tem um objeto _conta_ que representa uma conta bancária
e que deseja adicionar saldo a ela, e que o saldo em si é um campo numérico, que por sua vez
já tem um saldo anterior.

Logo a ação de atualização poderia ser feita assim:
```cs
var conta = _storage.Find(1);

conta.Saldo += 100.00f;

_storage.Replace(conta);
```

Ou simplesmente assim:
```cs
_storage.Update(1, c => new {
    Saldo = c.Saldo + 100.00f
});
```

Veja as assinaturas dos métodos:
[!code-csharp[](../../src/E5R.Architecture.Data/Abstractions/IReplaceableStorage.cs?range=26)]
[!code-csharp[](../../src/E5R.Architecture.Data/Abstractions/IUpdatableStorage.cs?range=27,36-37)]


## Delete (Remover)

Por fim, outra coisa que podemos fazer é **Remover** objetos armazenados, o que também
é conhecido por _excluir_, _apagar_ e até mesmo _deletar_, fique a vontade para chamar como
preferir.

Para remover um objeto é tão simples quanto escrever a linha abaixo:
```cs
_storage.Remove(1);
```

Se você observar bem, seguimos a mesma lógica do método `Find()`, que na prática, faz sentido
porque para apagar um objeto ele precisa primeiro existir. Assim, primeiro encontramos um
objeto armazenado e o removemos em seguida.

Logo, você tem a mesma assinatura do método `Find()` e pode usar como preferir. Ou seja,
se precisar remover um objeto de [chave composta](https://pt.wikipedia.org/wiki/Chave_prim%C3%A1ria):
```cs
_storage.Remove(new[] { 2, 7 });
```

Ou, pode apagar usando a assinatura que usa o modelo:
```cs
_storage.Remove(new MatriculaDataModel
{
    AlunoId = 2,
    CursoId = 7,
    PeriodoId = 8,
    InscricaoId = 12
});
```

## Conclusão

Por enquanto isso é suficiente para os propósitos deste tutorial. Agora você
já pode fazer um CRUD simples utilizando os utilitários fornecidos por **E5R Architecture**.

Ainda tem muitos detalhes que precisamos falar só quanto a persistência de dados,
sem falar em vários outros assuntos que a ferramenta nos auxilia e as próprias idéias
de organização em si, mas deixemos isso para outros tutoriais.
