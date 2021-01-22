---
title: Tutorial - Transformando dados entre tipos
---

Transformando dados entre tipos
===============================

No tipo de arquitetura que estamos propondo aqui e que de certa forma você já deve estar acostumado
a trabalhor, costumamos ter muitas camadas da aplicação, e fazemos isso para abstrair e assim
conseguir garantir o desacoplamento da solução como um todo.

Porém uma das atividades que nos pegamos fazendo o tempo todo é transformar dados de um tipo para
outro. Transformamos *View Model's* para *DTO's* e/ou *DTO's* para *Domain Model's*, além de
*Domain Model's* para *Data Model's*, e vice-versa.

Obviamente temos várias bibliotecas já disponíveis no mercado para conseguir fazer isso de forma
bem simples, e também vários plugins de IDE que nos ajudam muito.
Sabemos que não há dificuldade alguma em fazer essas transformações usando tais bibliotecas ou
plugins.

Só pra ilustrar, vamos listar aqui algumas dessas ferramentas.

* [ValueInjecter](https://github.com/omuleanu/ValueInjecter)
* [Boxed.Mapping](https://github.com/Dotnet-Boxed/Framework)
* [MappingGenerator](https://github.com/cezarypiatek/MappingGenerator)
* [ExpressMapper](https://github.com/fluentsprings/ExpressMapper)
* [AgileMapper](https://github.com/agileobjects/AgileMapper)
* [AutoMapper](https://github.com/AutoMapper/AutoMapper)

> Dessas, talvez a ferramenta mais conhecida de todas seja a [AutoMapper](https://github.com/AutoMapper/AutoMapper)

Mas entendemos aqui também que não devemos *inchar* nossos projetos com todo tipo de
bibliotecas, por isso fornecemos uma funcionalidade mínima para ajudá-lo nesse processo de
transformação de um tipo de dado para outro, então se você precisar e quiser, o recurso está
aí a mão, basta usá-lo.

> [!NOTE]
> Perceba que usamos o termo **Transformar** ao invés de **Mapear**. Fazemos assim para
> deixar claro que estamos transformando um certo tipo de dado em outro, e isso porque
> por vezes (nem sempre isso é verdade) o que fazemos é criar uma nova instância de um tipo
> de dado à partir de uma outra instância já existente mas de outro tipo de dado.
> Obviamente que parte desse trabalho de transformação consiste em fazer um "de/para", ou
> seja, mapear e copiar propriedades de um para outro objeto.
> Então não se sinta confuso com os termos **Transformar** e **Mapear**, aqui eles são
> quase equivalentes.

## Mão na massa

Então para transformar seus dados você vai precisar fazer o seguite:

Imagine que você tem essas duas classes:
```c#
public class MyFrom
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class MyTo
{
    public int Identifier { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
```

> Vamos supor que se trata de uma aplicação [ASP.NET](https://asp.net) ou uma
aplicação [.NET](https://dot.net) qualquer que usa os recursos de
[injeção de dependência](https://docs.microsoft.com/pt-br/dotnet/core/extensions/dependency-injection).

Primeiro registre o gerente de transformações em sua classe `Startup`:
```c#
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransformationManager();
    }
}
```
 
> [!WARNING]
> Você precisará do componente `E5R.Architecture.Infrastructure` para isso.

Agora você precisa declarar sua classe de transformação:
```c#
public class MyFromToTransformer : ITransformer<MyFrom, MyTo>
{
    public MyTo Transform(MyFrom @from)
    {
        return new MyTo
        {
            Identifier = @from.Id,
            Name = $"{@from.LastName}, {@from.FirstName}",
            Age = CalculateAge(@from.DateOfBirth, DateTime.Now)
        }
    }
}
```

> [!WARNING]
> Você precisrá registrar esse transformador para o tipo `ITransformer<MyFrom, MyTo>`, porque
> internamente o `TransformationManager` (implementação padrão para `ITransformationManager`)
> usa o `IServiceProvider` para resolver os transformadores.

Pronto, agora quando precisar esses transformar dados, basta usar:
```c#
public class MyService
{
    private readonly ITransformationManager _transformer;
    
    public MyService(ITransformationManager transformer)
    {
        _transformer = transformer;
    }
    
    public void MyMethod(MyFrom myFrom)
    {
        var myTo = _transformer.Transform<MyTo>(myFrom);
    }
}
```

Uma outra coisa que você pode fazer, é transformar os dados de forma personalizada de
acordo com alguma operação específica. Essa operação pode ser definida através de um
enum.

Imagine que você vai transformar o dado de um tipo para outro, mas dependendo de onde
vai usar o objeto resultante, você precisará de umas propriedades e outras não, ou
até poderá escolher entre tipos derivados diferentes.

Então imagine as seguintes operações:
```c#
public enum MyOperation
{
    Create,
    Remove,
    Update
}
```

Como exemplo imagine, quando for criar (`Create`) um novo dado, você precisará
dos dados *Name* e *Age* mas não precisará de *Identifier*, quando for atualizar
(`Update`) precisará de todos os dados, mas quando for remover (`Remove`) somente
o *Identifier* é necessário.

Aqui nós iremos declarar nossa classe um pouco diferente:
```c#
public class MyFromToTransformer : ITransformer<MyFrom, MyTo, MyOperation>
{
    public MyTo Transform(MyFrom @from, MyOperation op)
    {
        switch (op)
        {
            case MyOperation.Create:
                return new MyTo
                {
                    Name = $"{@from.LastName}, {@from.FirstName}",
                    Age = CalculateAge(@from.DateOfBirth, DateTime.Now)
                };
                
            case MyOperation.Remove:
                return new MyTo {
                    Identifier = @from.Id
                };
                
           case MyOperation.Update:
           default:
                return new MyTo
                {
                    Identifier = @from.Id,
                    Name = $"{@from.LastName}, {@from.FirstName}",
                    Age = CalculateAge(@from.DateOfBirth, DateTime.Now)
                };
        }
    }
}
```

E por fim, para utilizar:
```c#
public class MyService
{
    private readonly ITransformationManager _transformer;
    
    public MyService(ITransformationManager transformer)
    {
        _transformer = transformer;
    }
    
    public void MyMethod(MyFrom myFrom)
    {
        MyTo myTo = _transformer.Transform(myFrom, MyOperation.Update);
    }
}
```

E é isso aí! Futuramente podemos melhorar os textos e exemplos, mas por enquanto é só.
