---
title: Tutorial - Atributos de Metatags
---

Atributos de Metatags
=====================

Com os atributos de metatag's você pode anotar qualquer objeto do seu código:
```c#
using E5R.Architecture.Core;
using Tag = E5R.Architecture.Core.MetaTagAttribute;

[MetaTag(Tag.DescriptionKey, "Descrição de minha classe")]
public class MinhaClasse
{
    [MetaTag(Tag.CustomIdKey, "001")]
    public int CampoInterno { get; set; }
    
    [MetaTag("Tipo", "Construtor")]
    public MinhaClasse() { }
}
```

Agora você pode usar os recursos de reflexão do _.NET_ junto as extensões _E5R_ para
recuperar  essas tags:
```c#
using E5R.Architecture.Core;
using E5R.Architecture.Core.Extensions;
using Tag = E5R.Architecture.Core.MetaTagAttribute;

var classType = typeof(MinhaClasse);

// Todas as tags de um tipo
var classTags = classType.GetMetaTags();

// Só os nomes das tags
var classTagNames = classType.GetTags();

// O valor de uma tag específica
var classDescription = classType.GetTag(Tag.DescriptionKey);

// O valor de uma tag específica de um campo
var fieldCustomId = classType.GetField("CampoInterno").GetTag(Tag.CustomIdKey);

// O valor de uma tag específica de um construtor
var tipoConstrutor = classType.GetConstructor(new Type[0]).GetTag("Tipo");
```

Mas um bom uso dessa funcionalidade está relacionada ao manuseio de _Enum's_.
Por exemplo, suponha que você tenha um _Enum_ pra representar uma situação qualquer, e que
essa situação é utilizada em algumas rotas de sua API:
```c#
public enum MinhaSituacao
{
    Aguardando,
    EmProcessamento,
    CanceladoManualmente,
    CanceladoPeloSistema,
    FinalizadoComSucesso,
    FinalizadoComFalha
}

public class ObterSituacaoViewModel
{
    public int CodigoProcesso { get; set; }
    public MinhaSituacao Situacao { get; set; }
}

public class AlterarSituacaoViewModel
{
    public int CodigoProcesso { get; set; }
    public MinhaSituacao NovaSituacao { get; set; }
}
```

Quando você serializar/desserializar o objeto entre o front-end, você terá por exemplo
um JSON assim:
```json
{
    "ObterSituacaoViewModel": {
        "codigoProcesso": 123, 
        "situacao": 3
    },
    
    "Alterar situação": {
        "codigoProcesso": 456,
        "novaSituacao": 5
    }
}
```

Isso não fica tão intuitivo para o usuário final que usa a API, então seria bem melhor
algo como:
```json
{
    "ObterSituacaoViewModel": {
        "codigoProcesso": 123, 
        "situacao": "CS"
    },
    
    "Alterar situação": {
        "codigoProcesso": 456,
        "novaSituacao": "FF"
    }
}
```

Ou ainda:
```json
{
    "Obter situação": {
        "codigoProcesso": 123, 
        "situacao": {
            "id": "CS",
            "description": "Cancelado pelo sistema"
        }
    },
    
    "Alterar situação": {
        "codigoProcesso": 456,
        "novaSituacao": {
            "id": "FF",
            "description": "Finalizado com Falha"
        }
    }
}
```

Com isso você pode imaginar uma melhor descrição de seus _enum's_.

Representação em `JSON`:
```json
{
    "MinhaSituacao": [
        { "id": 0, "description": "Aguardando" },
        { "id": 1, "description": "EmProcessamento" },
        { "id": 2, "description": "CanceladoManualmente" },
        { "id": 3, "description": "CanceladoPeloSistema" },
        { "id": 4, "description": "FinalizadoComSucesso" },
        { "id": 5, "description": "FinalizadoComFalha" }
    ]
}
```

Ou ainda:
```json
{
    "MinhaSituacao": [
        { "id": "AG", "description": "Aguardando" },
        { "id": "EP", "description": "Em processamento" },
        { "id": "CM", "description": "Cancelado manualmente" },
        { "id": "CS", "description": "Cancelado pelo sistema" },
        { "id": "FS", "description": "Finalizado com sucesso" },
        { "id": "FF", "description": "Finalizado com falha" }
    ]
}
```

Você consegue esse resultado se aproveitando do novo recurso de _metatag's_ junto a um punhado
de alguns utilitários.

> Vale ressaltar que seus _Enum's_ não serão convertidos automaticamente "de" e "para"
> essas visões e representações JSON. Mas você consegue esse resultado escrevendo filtros,
> middleware, e outros componentes da sua pilha de desenvolvimento (ex: ASP.NET), e esses
> por sua vez podem utilizar a funcionalidade de _metatag's_ e os utilitários apresentados
> para alcançar tal resultado.

Vejamos então como usar.

Anote seu enum com _metatag's_:
```c#
public enum MinhaSituacao
{
    [MetaTag(Tag.DescriptionKey, "Aguardando")]
    [MetaTag(Tag.CustomIdKey, "AG")]
    Aguardando,
    
    [MetaTag(Tag.DescriptionKey, "Em processamento")]
    [MetaTag(Tag.CustomIdKey, "EP")]
    EmProcessamento,
    
    [MetaTag(Tag.DescriptionKey, "Cancelado manualmente")]
    [MetaTag(Tag.CustomIdKey, "CM")]
    CanceladoManualmente,
    
    [MetaTag(Tag.DescriptionKey, "Cancelado pelo sistema")]
    [MetaTag(Tag.CustomIdKey, "CS")]
    CanceladoPeloSistema,
    
    [MetaTag(Tag.DescriptionKey, "Finalizado com sucesso")]
    [MetaTag(Tag.CustomIdKey, "FS")]
    FinalizadoComSucesso,
    
    [MetaTag(Tag.DescriptionKey, "Finalizado com falha")]
    [MetaTag(Tag.CustomIdKey, "FF")]
    FinalizadoComFalha
}
```

Quando for entregar nas suas ViewModel's, ao invés de usar o próprio _enum_, você
pode usar o modelo geral EnumModel.
```c#
using E5R.Architecture.Core.Models;

public class ObterSituacaoViewModel
{
    public int CodigoProcesso { get; set; }
    public EnumModel Situacao { get; set; }
}

public class AlterarSituacaoViewModel
{
    public int CodigoProcesso { get; set; }
    public EnumModel NovaSituacao { get; set; }
}
```

E quando for entregar seus objetos na API:
```c#
using E5R.Architecture.Core;
using E5R.Architecture.Core.Models;

public class MyController
{
    public ObterSituacaoViewModel Get()
    {
        return new ObterSituacaoViewModel
        {
            CodigoProcesso = 1,
            
            // Aqui o Enum é convertido implicitamente para um EnumModel
            Situacao = MinhaSituacao.EmProcessamento
        };
        
        // resulta no mesmo que:
        return new ObterSituacaoViewModel
        {
            CodigoProcesso = 1,
            Situacao = new EnumModel
            {
                Id = "EP",
                Description: "Em processamento"
            }
        };
    }
}
```

E quando receber seus objetos da API, supondo que foi-lhe enviado o `JSON`:
```json
{
    "codigoProcesso": 1,
    "novaSituacao": {
        "id": "CM"
    }
}
```

```c#
using E5R.Architecture.Core;
using E5R.Architecture.Core.Models;
using E5R.Architecture.Core.Utils;
using E5R.Architecture.Core.Extensions;

public class MyController
{
    public void Alterar(AlterarSituacaoViewModel model)
    {
        MinhaSituacao situacao = model.NovaSituacao.ToEnum<MinhaSituacao>();
        
        // Sua ação com enum MinhaSituacao...
    }
}
```

Além disso você também pode obter uma lista de _enum's_ já tipada:
```c#
using E5R.Architecture.Core.Utils;

MinhaSituacao[] situacoes = EnumUtil.GetValues<MinhaSituacao>();
```

Ou, se preferir uma lista de EnumModel do _Enum_ correspondente:
```c#
using E5R.Architecture.Core.Utils;
using E5R.Architecture.Core.Models;

EnumModel[] modelos = EnumUtil.GetModels<MinhaSituacao>();
```
