---
title: Notas de Lançamento
---

Notas de Lançamento
===================

## 0.8.0 (dev)

Novos recursos:

* Adiciona genérico `LazyGroup<>` para agrupar objetos carregados preguiçosamente
    - Objetiva ser utilizado para construir objetos de fachada
    - Registra automaticamente objetos que o herdam, se usar `AddInfrastructure()`
* Agora é possível obter um valor de `Enum` através de um valor de `MetaTag`

Breaking changes:

* `IDataModel` agora é `IIdentifiable` e agora está em `Core` ao invés de `Data`
    - Também a propriedade com os valores foi renomeada de `IdentifierValues` para `Identifiers` somente
* O genérico `BusinessFacade<>` foi removido em favor de `LazyGroup<>`

## 0.7.0

Novos recursos:

* Adiciona conceito de `BusinessFeature` e `BusinessFacade`
    - Leia o tutorial [Escrevendo características de negócio](tutorials/writing-business-feature.md)

Refatorações:

* RuleFor<> agora aceita injeção de dependências
* RuleSet<>  reformulado para agrupar regras via injeção de dependência
* NotificationManager agora valida as mensagens de acordo com RuleSet<>

Breaking changes:

* RuleSet<> não é mais para agrupar manualmente regras
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

Novos recursos:

* Api fluente para gravar dados armazenados
    - Leia o tutorial [Escrevendo dados com API Fluente](tutorials/writing-with-fluent-api.md)
* UniqueIdentifier é um novo objeto do core para geração de identificadores únicos
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

Adiciona novas assinaturas a IDataStorage.Remove()

* Remove(object identifier);
* Remove(object[] identifiers);

Cria tutorial "Introdução a manipulação de dados" na documentação oficial:

https://e5r.github.io/E5R.Architecture

## 0.3.0

Nesta versão temos tantas novidades que não dá pra detalhar tanto, então ficam algumas notas.

* Adicionamos o conceito de `IRule` ao "core"
* Adicionamos o conceito de `NotificationManage` ao "core
* PaginatedResult<> result agora está no "Core" e é usado no lugar de `DataLimiterResult<>`
* Agora temos um local para documentação ao vivo (https://e5r.github.io/E5R.Architecture)
* UnitOfWork agora é implementado usando duas estratégias: `ByProperty` e `TransactionScope`
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

O Suporte a UnitOfWork está completo usando a estratégia de propriedades.

Várias outras refatorações foram feitas:

* Conceito de reducer passa a ser filter
* Conceito de IoC passa a ser DI
* ComponentInformation é obtido via dados do Assembly
* Exceções agora estão centralizadas no componente Core
* DataFilter e DataLimiter ganham uma implementação padrão em Linq

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
