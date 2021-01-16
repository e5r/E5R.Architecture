---
title: Tutorial - Lazy loading - Carregamento preguiçoso
---

Lazy loading - Carregamento preguiçoso
======================================

Você pode injetar suas dependências e elas serão carregadas preguiçosamente,
se você usar o genérico `ILazy`. Basicamente o que muda é isso:
```c#
public class MyService
{
    // Ao invés de usar isso:
    public MyService(IMyRepository myRepository, IStorage<Person> personStorage)
    {
        // ...
    }
    
    // Passe a usar isso:
    public MyService(ILazy<IMyRepository> myRepository, ILazy<IStorage<Person>> personStorage)
    {
        // ...
    }
}
```

Com essa assinatura o serviço em si só será carregado quando solicitado, o que nos lembra
que também temos uma pequena mudança na hora de utilizar as dependências:
```c#
// Antes
public MyMethod()
{
    var myData = myRepository.MyGetData();
}

// Depois
public MyMethod()
{
    var myData = myRepository.Value.MyGetData();
}
```

O problema principal que queremos evitar é o fato de carregar dependências desnecessariamente,
e que é bem descrito por [Aleksei Ananev](https://dev.to/hypercodeplace) no artigo
[Lazy Dependency Injection for .NET](https://dev.to/hypercodeplace/lazy-dependency-injection-37en).

> TODO: Explicar melhor o benefício para quando há muitas dependências
> nos construtores
