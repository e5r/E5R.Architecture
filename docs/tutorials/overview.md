---
title: Visão Geral
---

Arquitetura E5R
===============

A _Arquitetura E5R_ é basicamente a forma como eu [Erlimar](https://github.com/erlimar)
costumo organizar meus projetos [.NET](https://dot.net), e também reúne uma _"meia dúzia"_ de utilitários,
extensões, genéricos e outras classes úteis. Então podemos dizer que
**E5R.Architecture** é uma `especificação + alguns utilitários` que ajudam você a
implementar uma arquitetura de software [.NET](https://dot.net).

Basicamente, este projeto começou com uma série de utilitários e genéricos que uso para abstrair o
acesso ao banco de dados nas aplicações [.NET](https://dot.net) que tenho trabalhado, e mais especificamente
usando o [EntityFramework](https://github.com/dotnet/efcore), mas também incluía algumas outras _"coisinhas"_
para auxiliar na forma como implemento essas aplicações seguindo os princípios de [DDD](https://martinfowler.com/tags/domain%20driven%20design.html).

Além das idéias de organização em si (que iremos discutir nos próximos tutoriais),
estão disponíveis os seguintes componentes que podem ser obtidos via [NuGet](https://www.nuget.org/packages?q=e5r.architecture).

Componente  | &nbsp;
----------- | :----
E5R.Architecture.Business | [![NuGet](https://img.shields.io/nuget/v/E5R.Architecture.Business.png?style=flat-square&logo=nuget&color=blue)](https://www.nuget.org/packages/E5R.Architecture.Business)
E5R.Architecture.Core | [![NuGet](https://img.shields.io/nuget/v/E5R.Architecture.Core.png?style=flat-square&logo=nuget&color=blue)](https://www.nuget.org/packages/E5R.Architecture.Core)
E5R.Architecture.Data | [![NuGet](https://img.shields.io/nuget/v/E5R.Architecture.Data.png?style=flat-square&logo=nuget&color=blue)](https://www.nuget.org/packages/E5R.Architecture.Data)
E5R.Architecture.Data.Dapper | [![NuGet](https://img.shields.io/nuget/v/E5R.Architecture.Data.Dapper.png?style=flat-square&logo=nuget&color=blue)](https://www.nuget.org/packages/E5R.Architecture.Data.Dapper)
E5R.Architecture.Data.EntityFrameworkCore | [![NuGet](https://img.shields.io/nuget/v/E5R.Architecture.Data.EntityFrameworkCore.png?style=flat-square&logo=nuget&color=blue)](https://www.nuget.org/packages/E5R.Architecture.Data.EntityFrameworkCore)
E5R.Architecture.Infrastructure | [![NuGet](https://img.shields.io/nuget/v/E5R.Architecture.Infrastructure.png?style=flat-square&logo=nuget&color=blue)](https://www.nuget.org/packages/E5R.Architecture.Infrastructure)
E5R.Architecture.Infrastructure.Defaults | [![NuGet](https://img.shields.io/nuget/v/E5R.Architecture.Infrastructure.Defaults.png?style=flat-square&logo=nuget&color=blue)](https://www.nuget.org/packages/E5R.Architecture.Infrastructure.Defaults)
E5R.Architecture.Infrastructure.AspNetCore | [![NuGet](https://img.shields.io/nuget/v/E5R.Architecture.Infrastructure.AspNetCore.png?style=flat-square&logo=nuget&color=blue)](HTTPS://WWW.NUGET.ORG/PACKAGES/E5R.ARCHITECTURE.INFRASTRUCTURE.ASPNETCORE)

**E5R Architecture** é código aberto, o [código fonte está disponível no GitHub](https://github.com/e5r/e5r.architecture),
e toda contribuição é bem vinda.

E como tudo começou pensando nos utilitários e genéricos para abstrair a forma como
acessamos o banco de dados, minha dica é: comece pela leitura do tutorial
[Introdução a manipulação de dados](./data-intro.md).

