---
title: Notas de Lançamento
---

Notas de Lançamento
===================

## 0.10.0

### Novos recursos:

* Nova interface `IdentifiableExpressionMaker` 
  - Para auxiliar na construção de objetos de filtro
  - Mas pode ser usado em qualquer lugar que precise converter um objeto identificável
    em uma expressão para o tipo identificável
* Novo tipo `AttributableValue` para valores _atribuíveis_
  - Também temos um utilitário `AttributableValueUtil` para uso do `Assigned` inclusive
    em valores nulos e de forma estática
  - As vezes só o `Nullable<x>` ou `x?` não é o suficiente
  - E `Nullable<Nullable<x>>` ou `x??` não é permitido ainda
```c#
public class Pessoa
{
    public string Nome {get; set; }
    public int? Idade { get; set; }
}

public class PessoaFiltro
{
    public string PorNome {get; set; }
    public int? PorIdade { get; set; }
}

// As duas classes para simular que você tem um objeto e deseja fazer um
// filtro no banco de dados por esse objeto.
// Vamos focar apenas no campo "Idade".
//
// Como filtrar por uma idade específica?
// Simples: Se o campo `PorIdade` tiver um valor (PorIdade.HasValue)
// aplicamos o filtro, em outro caso não.
//
// Mas veja que a propriedade "Idade" é opcional no banco de dados,
// isso quer dizer que também podemos ter campos sem idade informada.
//
// Como filtrar por itens que não tem idade informada?
// Não é mais simples: Já usamos a opção "PorIdade.HasValue" para saber se
// devemos ou não aplicar o filtro. Então como representar um valor nulo agora?
//
// Simplificando novamente: Usamos o novo tipo `AttributableValue`

public class PessoaFiltro
{
    public string PorNome {get; set; }
    public AttributableValue<int?> PorIdade { get; set; }
}

// Agora podemos conferir se o valor está atribuído "PorIdade.Assigned"
// e em seguida, usar o valor interno "PorIdade.Value", que por sua vez é um
// "Nullable<int>". E agora temos uma simulação de "Nullable<Nullable<int>>"
// já que não podemos fazer isso diretamente na linguagem.
// Seria o mesmo que "int??".
```
* Com o novo `IDataFilter<>` combinado com `IIdentifiableExpressionMaker<>` agora podemos
  criar filtros personalizados por objeto
```C#
using static E5R.Architecture.Core.Utils.AttributableValueUtil;

public class ByLastNameFilter : IIdentifiableExpressionMaker<Student>
{
    public AttributableValue<string> LastName { get; set; }

    public Expression<Func<Student, bool>> MakeExpression()
    {
        return w => !Assigned(LastName) || string.Compare(w.LastName, LastName) == 0;
    }

    public string MyCreateSqlClause()
    {
        // Sua lógica para criar o SQL...
    }
}

// Com isso, nos métodos que recebem um IDataFilter, agora você pode usar
// tanto uma expressão diretamente:
var _ = MyStorage.AsFluentQuery()
    .Filter(f => string.Compare(f.LastName, "My Last Name") == 0)
    .Search();

// Quanto esse objeto personalizado
var _ = MyStorage.AsFluentQuery()
    .Filter(new ByLastNameFilter { LastName = "My Last Name"})
    .Search();
```
* Novos métodos de extensão para uso de `Task` em contextos síncronos
```c#
class MinhaClasse
{
    // Opção 1: Já conhecida e continua igual
    void MeuMetodoSincronoSemResultado()
    {
        ChamandoMetodoAssincrono().Wait();
    }

    // Opção1: Agora com métodos que retornam resultado
    int MeuMetodoSincronoComResultado()
    {
        Task t = ChamandoMetodoAssincrono();

        t.Wait();

        return t.Result;
    }

    // Opção 2: Com nova extensão
    int MeuMetodoSincronoComResultado()
    {
        return ChamandoMetodoAssincrono().WaitResult();
    }
}
```
* `ITransformationManager` ganha sobrecarga de método para transformar listas simples
  e listas paginadas de itens
```c#
var transformer = ITransformationManager;

var listOfA = new List<A>
{
    new A { AMessage = "Message A1" },
    new A { AMessage = "Message A2" },
    new A { AMessage = "Message A3" },
};

var paginatedListOfA = new PaginatedResult<A>(listOfA, 10, 3, 1000);
var listOfB = transformer.Transform<A, B>(listOfA);
var paginatedListOfB = transformer.Transform<A, B>(paginatedListOfA);

public class AToBTransformer : ITransformer<A, B>
{
    public B Transform(A @from)
    {
        return new B
        {
            BMessage = @from.AMessage
        };
    }
}
```
* `ITransformationManager` ganha novos métodos para transformar automaticamente objetos
```c#
// TTo AutoTransform<TFrom, TTo>(TFrom from) where TTo : new();
// IEnumerable<TTo> AutoTransform<TFrom, TTo>(IEnumerable<TFrom> from) where TTo : new();
// PaginatedResult<TTo> AutoTransform<TFrom, TTo>(PaginatedResult<TFrom> from) where TTo : new();

public class MyFrom
{
    public int IntegerValue { get; set; }
    public string StringValue { get; set; }
    public Guid GuidValue { get; set; }
}

public class MyTo
{
    public int IntegerValue { get; set; }
    public string StringValue { get; set; }
    public string NotCopiedString { get; set; }
    public Guid GuidValue { get; set; }
}
var transformer = ITransformationManager;

var from = new MyFrom { /* ... */ }
var to = transformer.AutoTransform<MyFrom, MyTo>(from);

// Se existir um ITransformer<MyFrom, MyTo> ele será usado, se não, as propriedades
// serão simplesmente copiadas entre os objetos.
// Na cópia todas as propriedades de mesmo nome e tipo serão copiadas e as demais não.
```
* Novo utilitário para tipo `object` que copia valores entre objetos
```c#
using E5R.Architecture.Core.Extensions;

var obj1 = new MyObjectOne();
var obj2 = new MyObjectTwo();

// Copia valores das propriedades de "obj1" para "obj2"
// "propriedades com mesmo nome e tipo"
var copiedCount = obj1.CopyPropertyValuesTo(obj2);

// O retorno é a quantidade de propriedades copiadas, assim
// você pode tomar alguma decisão caso nada tenha sido copiado.
```

### Breaking changes:

* `BusinessFeature` e seus derivados foram renomeados para `ActionHandler`
* `LazyGroup<>` foi renomeado para `LazyTuple`
  - Seus itens agora são públicos
  - Foi adicionado a tupla com único item
* O registro no assembly `AddAllLazyGroups()` não registra mais classes que herdam de `LazyGroup<>` (que agora se chama `LazyTuple<>`)
  - Ao invés disso registra diretamente `LazyTuple<>`
* `IDataFilter<>` tem nova assinagura:
```C#
public interface IDataFilter<TDataModel>
{
    // Sempre deve retornar uma lista de expressões
    IEnumerable<Expression<Func<TDataModel, bool>>> GetExpressions();

    // Quando não houver objetos de filtro, deve-se retornar as
    // próprias expressões como objetos
    IEnumerable<object> GetObjects();
}
```
* Algumas interfaces do componente `E5R.Architecture.Data` foram simplificadas
  - A interface `IRemovableStorage` agora só tem uma assinatura de método
```C#
public interface IRemovableStorage<TDataModel>
{
    void Remove(object[] identifiers);
}
```
  - A interface `IUpdatableStorage` agora só tem duas assinaturas de método
```c#
public interface IUpdatableStorage<TDataModel>
{
    TDataModel Update<TUpdated>(object[] identifiers, TUpdated updated);
    TDataModel Update<TUpdated>(object[] identifiers, Expression<Func<TDataModel, TUpdated>> updateExpression);
}
```
* As interfaces `IAcquirableStorage` e `IAcquirableStorageWithSelector` ganham novo método para retornar primeiro item à partir de um filtro
```c#
public interface IAcquirableStorage<TDataModel>
{
    TDataModel GetFirst(IDataFilter<TDataModel> filter, IDataIncludes includes = null);
}

public interface IAcquirableStorageWithSelector<TDataModel>
{
    TSelect GetFirst<TSelect>(IDataFilter<TDataModel> filter, IDataProjection<TDataModel, TSelect> projection);
}
```
* Os derivados de `RideStorage`, que inclui `RawSqlRideStorage` agora implementam o método `Find()`
* Agora no resultado de validação de uma regra temos a possibilidade de identificar uma falha inesperada
```c#
var rule = MyRuleFor();
var result = rule.Check(myModel);

if(!result.IsSuccess && result.UnexpectedException != null)
{
    // Use result.UnexpectedException...
}
```

## 0.9.0

### Novos recursos:

* Agora é possível registrar preferências com mecanismo *cross cutting*
```c#
public class CrossCuttingRegistrar : ICrossCuttingRegistrar
{
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSettings<MySettings>(ServiceLifetime.Scoped, configuration, MySettings.Key);
        services.AddTransientSettings<MySettings>(configuration, MySettings.Key);
        services.AddScopedSettings<MySettings>(configuration, MySettings.Key);
        services.AddSingletonSettings<MySettings>(configuration, MySettings.Key);
    }
}

// Neste caso a preferência MySettings estará disponível tanto como o próprio tipo como um `IOptions<>`
public class MyService
{
    public MyService(IOptions<MySettings> myOptions, MySettings mySettings)
    {
        // ...
    }
}
```
* Adiciona opção para usar serviços padrões em `AddInfrastructure()`
    - Isso irá aplicar `DefaultFileSystem` para `IFileSystem` e `DefaultSystemClock` para `ISystemClock`
    - Disponível no componente `E5R.Architecture.Infrastructure.Defaults`
```c#
services.AddInfrastructure(options => {
    options.UseDefaults();
});
```
* Agora temos abstrações segregadas para `IStorage<T>`
    + `ICountableStorage<T>` para objetos contáveis
    + `IFindableStorage<T>` para objetos encontráveis
        - `IFindableStorageWithSelector<T>` que permitem projeção de seleção
    + `ISearchableStorage<T>` para objetos pesquisáveis
        - `ISearchableStorageWithGrouping<T>` que permitem agrupamento
        - `ISearchableStorageWithSelector<T>` que permitem projeção de seleção
    + `IAcquirableStorage<T>` para objetos adquiríveis
        - `IAcquirableStorageWithGrouping<T>` que permitem agrupamento
        - `IAcquirableStorageWithSelector<T>` que permitem projeção de seleção
    + `ICreatableStorage` para objetos criáveis
        - `IBulkCreatableStorage` objetos criáveis em massa
    + `IRemovableStorage` para objetos removíveis
        - `IBulkRemovableStorage` objetos removíveis em massa
    + `IReplaceableStorage` para objetos substituíveis
        - `IBulkReplaceableStorage` objetos substituíveis em massa
    + `IUpdatableStorage` para objetos atualizáveis
        - `IBulkUpdatableStorage` objetos atualizáveis em massa
> Assim é possível implementar repositórios customizados somente como o que precisa.
> Não há suporte para "alias" porque o objetivo é remover as abstrações "alias" no futuro.
* Agora é possível utilizar uma mesma classe para vários transformadores
````c#
public class MultTransformer : ITransformer<B, A>, ITransformer<A, B>
{
    public A Transform(B @from)
    {
        return new A
        {
            AMessage = @from.BMessage
        };
    }

    public B Transform(A @from)
    {
        return new B
        {
            BMessage = @from.AMessage
        };
    }
}
````
* Agora é possível tentar obter o valor de um enum com base em metatag's de forma silenciosa
```c#
using E5R.Architecture.Core.Utils;
using Tag = E5R.Architecture.Core.MetaTagAttribute;

enum MyEnum
{
    [MetaTag(DescriptionKey, "My First Option")]
    FirstOption,

    [MetaTag(CustomIdKey, "second-option")]
    SecondOption
}

// Isso resulta em "MyEnum.SecondOption"
EnumUtil.FromTag<MyEnum>(Tag.CustomIdKey, "second-option");

// Isso levanta uma exceção
EnumUtil.FromTag<MyEnum>(Tag.DescriptionKey, "Invalid Description");

// Isso resulta em "false" e a variável resultado é preenchida com default(MyEnum)
EnumUtil.TryFromTag<MyEnum>(Tag.DescriptionKey, "Invalid Description", out MyEnum resultado);
```
* Adicionado gerenciamento de processos em segundo plano no componente Infrastructure.AspNetCore:
```c#
// Crie sua classe de trabalho
class MyWorker : Worker
    public Worker() : base(nameof(MyWorker)) {}
    
    public override Task<bool> DoWorkAsync(CancellationToken cancellationToken)
    {
        // TODO: Implemente seu trabalho
    }
}

// Em sua classe Startup.cs
// Habilite o gerenciamento de trabalhos
services.AddWorkManager();

// Registre seu trabalho
services.AddHostedWorker<MyWorker>();

// PS: Você tem dois sabores de trabalhadores:
//     Worker - Trabalhador comum
//     QueueWorker - Trabalhador de fila
```
* Adicionado método de extensão para preencher objeto com um dicionário de strings
```c#
using E5R.Architecture.Core.Extensions;

private class MyProperties
{
    public string MyProperty1 { get; set; }
    public string MyProperty2 { get; set; }
    public string MyProperty3 { get; set; }
    public string MyProperty4 { get; set; }
}

var myDictionary = new Dictionary<string, string>
{
    {"MyProperty1", "Exact name"},
    {"myProperty1", "camelCase name"},
    {"my_property1", "snake_case name"},
    {"My_Property1", "Snake_Case name"},
};

var myProperties1 = new MyProperties().Fill(myDictionary);
var myProperties2 = myDictionary.FillObject(new MyProperties());
var myProperties3 = myDictionary.Activate<MyProperties>();

Assert.Equal("Exact name", myProperties1.MyProperty1);
Assert.Equal("camelCase name", myProperties1.MyProperty2);
Assert.Equal("snake_case name", myProperties1.MyProperty3);
Assert.Equal("Snake_Case name", myProperties1.MyProperty4);

Assert.Equal("Exact name", myProperties2.MyProperty1);
Assert.Equal("camelCase name", myProperties2.MyProperty2);
Assert.Equal("snake_case name", myProperties2.MyProperty3);
Assert.Equal("Snake_Case name", myProperties2.MyProperty4);

Assert.Equal("Exact name", myProperties3.MyProperty1);
Assert.Equal("camelCase name", myProperties3.MyProperty2);
Assert.Equal("snake_case name", myProperties3.MyProperty3);
Assert.Equal("Snake_Case name", myProperties3.MyProperty4);
```
* Adiciona mais utilitários de enum
```c#
// É possível obter uma referência para tipo de enum por seu valor
// numérico integral diretamente
enum MyEnum
{
    Option0,
    Option1,
    Option2
}
MyEnum myEnumValue = EnumUtil.FromValue<MyEnum>(1));
Assert.Equal(MyEnum.Option1, myEnumValue);

// Se preferir pode testar se a conversão é válida
if(!EnumUtil.TryFromValue(7, out MyEnum _))
{
    throw new Exception("Identificador MyEnum inválido");
}
```
* Adiciona métodos utilitários protegidos em `RuleFor<T>` para melhorar a verificação assíncrona
```c#
class MyRuleFor : RuleFor<MyType>
{
    public override async Task<RuleCheckResult> CheckAsync(MyType target)
    {
        // Para indicar sucesso
        return await Success();

        // Para indicar falha (a inconformidade será o código e descrição da regra)
        return await Fail();
        
        // Para indicar falha sem nenhuma inconformidade
        return await FailWithoutUnconformities()
        
        // Para indicar falha com uma inconformidade personalizada
        return await Fail("codigo", "Descrição da inconformidade");
        
        // Para indicar falha com uma lista personalizada de inconformidades
        var inconformidades = new Dictionary<string, string>();
        // ...
        return await Fail(inconformidades);
    }
}
```
* Nova extensão que adiciona infraestrutura sem autocarregamento `AddInfrastructureWithoutAutoload()`
> Quando usamos `AddInfrastructure()` vários itens da infraestrutura são carregados
> automaticamente, mas se preferir pode não carregá-los.
```c#
// Mas você ainda tem todo controle no que carregar ou não
services.AddInfrastructureWithoutAutoload(config, options => {
    options.RegisterCrossCuttingAutomatically = true;
    options.RegisterRulesAutomatically = true;
    options.RegisterNotificationDispatchersAutomatically = true;
    options.RegisterTransformersAutomatically = true;
    options.RegisterLazyGroupsAutomatically = true;
});

// Quando você usa assim, todos são habilitados por padrão
services.AddInfrastructure(config);

// Você ainda pode carregar cada item separadamente direto dos seus assemblies e
// isso simplesmente vai registrar cada tipo no container de injeção de dependências
Assembly.GetExecutingAssembly()
    .AddAllRules(services)
    .AddAllTransformers(services)
    .AddAllLazyGroups(services)
    .AddAllNotificationDispatchers(services)
    .AddCrossCuttingRegistrar(services, config);
```
* Agora você pode informar vários assemblies ao mesmo tempo para carregamento automático de tipos
```c#
services.AddInfrastructure(config, options => {
    options.AddAssemblies(new[]
    {
        "Assembly1",
        "Assembly2"
    });
});
```

### Breaking changes:

* O tipo `BusinessFeature` agora não requer mais `ITransformationManager` no construtor
  - Um novo tipo `BusinessFeatureWithTransformer<>` foi introduzido para quando necessitar de `ITransformationManager`
* Aprimoramentos do mecanismo de *cross cutting*
  + `IDIRegistrar` foi renomeado para `ICrossCuttingRegistrar`
  + Agora usa o sistema padrão de injeção de dependência do .NET
    - Foram removidas as seguintes abstrações:
      - `DILifetime`
      - `IDIContainer`
    - A interface `ICrossCuttingRegistrar` espera um `IServiceCollection` junto a um `IConfiguration` ao invés de um `IDIContainer`
* A configuração de infraestrutura `AddInfrastructure()` agora requer pelo menos um `IConfiguration`

## 0.8.0

### Novos recursos:

* Adiciona genérico `LazyGroup<>` para agrupar objetos carregados preguiçosamente
    - Objetiva ser utilizado para construir objetos de fachada
    - Registra automaticamente objetos que o herdam, se usar `AddInfrastructure()`
* Agora é possível obter um valor de `Enum` através de um valor de `MetaTag`
* Novos métodos adicionados a interface `IStorage<>`
    - `int CountAll()` que retorna o total de registros
    - `int Count(IDataFilter<> filter)` que retorna o total de registros que obedeçam a um determinado filtro
    - Os métodos também estão disponíveis na api fluente `AsFluentQuery()`
* Adiciona capacidade de deduzir nome de parâmetro por expressões no utilitário Checker
```c#
void MyMethod(MyClass model)
{
    Checker.NotNullObject(model?.Inner?.Prop, () => model.Inner.Prop);
    // Equivalente a:
    Checker.NotNullObject(model?.Inner?.Prop, "model.Inner.Prop");
}
```
* Adiciona utilitários para calcular hash e assinaturas HMAC de `byte[]` e `string` na forma de extensões
```c#
using E5R.Architecture.Core.Extensions;

var myKey = Encoding.Default.GetBytes("secret");
var myBytes = new byte[]{ 1,2,3,4};
var myString = "My string text";

/* Hash e HMAC em bytes obtidos de um array de bytes

   - Md5(), Sha1(), Sha256(), Sha384(), Sha512()
   - HmacMd5(), HmacSha1(), HmacSha256(), HmacSha384(), HmacSha512()
*/
byte[] hashOfBytes = myBytes.Md5();
byte[] hmacOfBytes = myBytes.HmacSha1(myKey);

/* Hash e HMAC em string obtidos de um array de bytes

   - Md5Hex(), Sha1Hex(), Sha256Hex(), Sha384Hex(), Sha512Hex()
   - HmacMd5Hex(), HmacSha1Hex(), HmacSha256Hex(), HmacSha384Hex(), HmacSha512Hex()
*/
string hashHexOfBytes = myBytes.Sha256Hex();
string hmacHexOfBytes = myBytes.HmacSha384Hex(myKey);

/* Hash e HMAC em bytes obtidos de uma string */
byte[] hashOfString = myString.Sha512();
byte[] hmacOfString = myString.HmacMd5(myKey);

/* Hash e HMAC em string obtidos de uma string */
string hashHexOfString = myString.Sha1Hex();
string hmacHexOfString = myString.HmacSha256Hex(myKey);
```

### Breaking changes:

* `IDataModel` agora é `IIdentifiable` e foi movido de `Data` para `Core`
  - Também a propriedade com os valores foi renomeada de `IdentifierValues` para `Identifiers` somente
* O genérico `BusinessFacade<>` foi removido em favor de `LazyGroup<>`
* Remove método de extensão `AddTransformationManager()`.
  - Agora `ITransformationManager` é configurado automaticamente em `AddInfrastructure()`
* Renomeia método de extensão `AddBusinessFeatures()` para somente `AddBusiness()` e altera assinatura
* Altera assinatura de método de extensão `AddInfrastructure()`
```c#
// Agora temos várias opções de configuração da infraestrutura
services.AddInfrastructure(options => {
    options
        .AddAssembly("MyAssembly.Um")
        .AddAssembly("MyAssembly.Dois")
        .AddAssembly("MyAssembly.Tres")
        
        // Ao habilitar o modo de desenvolvimento, o tipo ILazy<> é resolvido
        // não de forma preguiçosa real, mas imediatamente, e isso é um lazy fake,
        // porém ajuda a encontrar falhas no grafo de injeção de dependências logo na
        // inicialização da aplicação. Quando uma dependência não foi devidamente
        // registrada, uma exceção é levantada imediatamente no startup da aplicação,
        // ao invés de somente quando uma chamada a funcionalidade é realizada.
        .EnableDeveloperMode();
});

// Agora não precisamos mais informar a lista de assemblies em AddBusiness(),
// pois essa lista já foi informada previamente via AddInfrastructure()
services.AddBusiness();
```

## 0.7.0

### Novos recursos:

* Adiciona conceito de `BusinessFeature` e `BusinessFacade`
    - Leia o tutorial [Escrevendo características de negócio](tutorials/writing-business-feature.md)
* `RuleFor<>` agora aceita injeção de dependências
* `NotificationManager` agora valida as mensagens de acordo com `RuleSet<>`

### Breaking changes:

* `RuleSet<>` reformulado para agrupar regras via injeção de dependência
* `RuleSet<>` não é mais para agrupar manualmente regras
```c#
// Antes: Você instanciava RuleSet<> e adicionava
//        as regras que queria estar em conformidade, e,
//        logo em seguida garantir a conformidade.
public class MyBusinessClass
{
    public void MyMethod1(MyModel model)
    {
        var rules = new RuleSet<MyModel>()
            .Conform<MyRule2>()
            .Conform<MyRule3>();
            
        rules.Ensure(model);
    }
}

// Agora: Você injeta IRuleSet<> de um modelo e todas as
//        regras criadas para aquele modelo serão avaliadas
//        automaticamente
public class MyBusinessClass
{
    public MyBusinessClass(IRuleSet<MyModel> ruleSet)
    { }
    
    public void MyMethod1(MyModel model)
    {
        ruleSet.Ensure(model);
    }
    
    // Caso queira avaliar só uma ou outra regra específica
    // você ainda pode fazer um filtro por códigos das
    // regras pretendidas antes da avaliação
    public void MyMethod1(MyModel model)
    {
        ruleSet.ByCode(new[]{"R2", "R3"}).Ensure(model);
    }
}
```

## 0.6.0

### Novos recursos:

* Api fluente para gravar dados armazenados
    - Leia o tutorial [Escrevendo dados com API Fluente](tutorials/writing-with-fluent-api.md)
* `UniqueIdentifier` é um novo objeto do core para geração de identificadores únicos
    - Leia o tutorial [Identificadores únicos](tutorials/uid.md)
* Transformação de dados entre tipos
    - Leia o tutorial [Transformando dados entre tipos](tutorials/transformer-intro.md)

## 0.5.0

Adicionamos alguns novos recursos:

* Lazy loading
* Atributos de "metatag"
* Abstração de data e hora do sistema
* Utilitários de Enum

## 0.4.0

Adiciona novas assinaturas a `IDataStorage.Remove()`

* Remove(object identifier);
* Remove(object[] identifiers);

Cria tutorial "Introdução a manipulação de dados" na documentação oficial:

https://e5r.github.io/E5R.Architecture

## 0.3.0

Nesta versão temos tantas novidades que não dá pra detalhar tanto, então ficam algumas notas.

* Adicionamos o conceito de `IRule` ao "core"
* Adicionamos o conceito de `NotificationManager` ao "core
* `PaginatedResult<>` result agora está no "Core" e é usado no lugar de `DataLimiterResult<>`
* Agora temos um local para documentação ao vivo (https://e5r.github.io/E5R.Architecture)
* `UnitOfWork` agora é implementado usando duas estratégias: `ByProperty` e `TransactionScope`
* Estabiliza API de `IStorageReader<>`
    - Find()
    - GetAll()
    - LimitedGet()
    - Search()
    - LimitedSearch()
* Estabiliza API de `IStorageWriter<>`
    - Create()
    - Replace()
    - Remove()
    - Update()
* Estabiliza API de `IStorageBulkWriter<>`
    - BulkCreate()
    - BulkReplace()
    - BulkRemove()
    - BulkUpdate()
* Adicionamos suporte a FluentQuery nos repositórios genéricos de consulta:
```cs
var result = _studentStore.AsFluentQuery()
    .Projection()
        .Include(i => i.Enrollments)
        .Include<Enrollment>(i => i.Enrollments)
            .ThenInclude<Course>(i => i.Course)
        .Map(m => new
        {
            StudentName = m.FirstMidName,
            TotalEnrollments = m.Enrollments.Count,
            Enrollments = m.Enrollments.Select(s => new
            {
                Grade = s.Grade,
                 Course = s.Course.Title
            })
        })
        .Project()
    .SortDescending(s => s.FirstMidName)
    .LimitedGet();
```
* Adiciona aliases `IRepository<>` e `IStore<>` a `IStorage<>`
* Adiciona conceito de `RideStorage` e `RawSqlRideStorage` para componente `Data.EntityFrameworkCore`

> Além de várias outras refatorações para limpeza e padronização de código.

## 0.2.0

O Suporte a `UnitOfWork` está completo usando a estratégia de propriedades.

Várias outras refatorações foram feitas:

* Conceito de reducer passa a ser filter
* Conceito de IoC passa a ser DI
* `ComponentInformation` é obtido via dados do Assembly
* Exceções agora estão centralizadas no componente Core
* `DataFilter` e `DataLimiter` ganham uma implementação padrão em Linq

O suporte a `E5R.Architecture.Core.Formatters` foi removido

> Além de várias outras refatorações para limpeza e padronização de código.

## 0.1.0-alpha

* classe E5R.Architecture.Core.**SemVer** básica
* classe E5R.Architecture.Data.**Data** temporária
* Formatador de string CPF
```csharp
using E5R.Architecture.Core.Formatters;

// code block
{
   string cpfFormatado = (FormattedCPF)"12345678901"; 
}

// output $cpfFormatado => "123.456.789-01"
```

* Desformatador de string de CPF
```csharp
using E5R.Architecture.Core.Formatters;

// code block
{
   string cpfDesformatado = (UnformattedCPF)"123.456.789-01"; 
}

// output $cpfDesformatado => "12345678901"
```
